
using JPT_TosaTest.Config.SoftwareManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisionLib;
using JPT_TosaTest.MotionCards;
using JPT_TosaTest.IOCards;
using JPT_TosaTest.Model;
using M12.Definitions;
using M12.Base;
using M12.Commands.Alignment;
using JPT_TosaTest.WorkFlow.CmdArgs;
using JPT_TosaTest.Config.ProcessParaManager;
using JPT_TosaTest.Classes.WatchDog;
using JPT_TosaTest.WorkFlow.WorkFlow;
using HalconModle;

namespace JPT_TosaTest.WorkFlow
{
    public class WF_Aligner : WorkFlowBase
    {
        public enum STEP : int
        {

            #region INIT  回机械原点
            INIT,
            #endregion

            #region TOUCH,    //touch
            TOUCH,      //移动X轴
            WaitTochXOk,    //等待TouchSensor到位
            MoveBack,       //后退几个微米，根据工艺来确定
            #endregion

            #region ROUGHSCAN, //粗扫
            ROUGHSCAN, //粗扫
            MoveToMaxPositionRoughScan,
            ShowResultInGUIRoughScan,
            #endregion

            #region FINESCAN,  //精扫
            FINESCAN,  //精扫
            MoveToMaxPositionFineScan,
            ShowResultInGUIFineScan,
            #endregion

            #region CALCLOSS, //计算差损
            CALCLOSS, //计算差损
            SwitchPowerSource,  //切换光路
            ReadPower,          //读取功率
            ShowResultLoss,     //计算该路差损
            #endregion

            #region DO_NOTHING
            DO_NOTHING,
            #endregion

            #region EXIT
            EXIT,
            #endregion
        }

        private Motion_IrixiEE0017 motion = null;
        private IO_IrixiEE0017 io = null;
        private const int AXIS_X = 0, AXIS_Y = 1, AXIS_Z = 2, AXIS_R = 4;
        STEP Step;

        WFPointModel PtCamBottomSnapPos;    //下相机拍照点
        WFPointModel PtCamTopSnapPos;       //上相机拍照点
        const int IN_STEP_PLC = 0, IN_STEP_FA = 1;
        const int OUT_STEP_PLC = 0, OUT_STEP_FA = 1;

        public override bool UserInit()
        {
            motion = MotionMgr.Instance.FindMotionCardByAxisIndex(1) as Motion_IrixiEE0017;
            io = IOCardMgr.Instance.FindIOCardByCardName("IO_IrixiEE0017[0]") as IO_IrixiEE0017;
            bool bRet = motion != null && io != null && LoadPoint();
            
            if (!bRet)
                ShowInfo($"初始化失败");
            io.OnIOStateChanged += Io_OnIOStateChanged;
            return bRet;
        }

        public WF_Aligner(WorkFlowConfig cfg) : base(cfg)
        {

        }
        protected override int WorkFlow()
        {
            double CommonAcc = 500;
            double CommonSpeed = 10;
            List<Point3D> BlindScanResult = null;
            try
            {
                ClearAllStep();
                PushStep(STEP.INIT);
                while (!cts.IsCancellationRequested)
                {

                    int n = PeekStep();
                    var b = Enum.IsDefined(typeof(STEP), n);
                    Thread.Sleep(10);
                    if (bPause || b==false)
                        continue;
                    Step = (STEP)n;
                    switch (Step)
                    {
                        case STEP.INIT: 
                            HomeAll();
                            PopStep();
                            break;

                        case STEP.TOUCH:
                            motion.SetCssThreshold(CSSCH.CH1, 1000, 1500);
                            motion.SetCssEnable(CSSCH.CH1,true);
                            motion.MoveAbs(AXIS_X, CommonAcc, CommonSpeed, 1000);
                            PopAndPushStep(STEP.WaitTochXOk);
                            break;
                        case STEP.WaitTochXOk:
                            if (motion.IsNormalStop(AXIS_X))
                            {
                                motion.MoveRel(AXIS_X, CommonAcc, CommonSpeed, 0.002);
                                PopAndPushStep(STEP.MoveBack);
                            }
                            break;
                        case STEP.MoveBack:
                            if (motion.IsNormalStop(AXIS_X))
                            {
                                ClearAllStep();
                            }
                            break;


                        case STEP.ROUGHSCAN:
                            var Args = new CmdAlignArgs()
                            {
                                HArgs = new BlindSearchArgsF()
                                {
                                    AxisNoBaseZero = AXIS_Y,
                                    Gap = 0.001,
                                    Interval = 0.001,
                                    Range = 0.05,
                                },
                                VArgs = new BlindSearchArgsF()
                                {
                                    AxisNoBaseZero = AXIS_Z,
                                    Gap = 0.001,
                                    Interval = 0.001,
                                    Range = 0.05,
                                },
                            };
                            BlindScanResult=DoBlindSearchAlignment(Args);
                            PopAndPushStep(STEP.MoveToMaxPositionRoughScan);
                            break;

                        case STEP.MoveToMaxPositionRoughScan:
                            if (BlindScanResult != null)
                            {
                                PopAndPushStep(STEP.ShowResultInGUIRoughScan);
                            }
                            break;
                        case STEP.ShowResultInGUIRoughScan: //界面显示

                            PopStep();
                            break;


                        case STEP.FINESCAN:   //线扫
                            motion.DoFastScan1D(AXIS_X, 0.01, 0.001, 10, ADCChannels.CH1, out List<Point2D> FastScanResult);
                            PopAndPushStep(STEP.ShowResultInGUIFineScan);
                            break;
                        case STEP.ShowResultInGUIFineScan:
                            PopStep();
                            break;
   


                        case STEP.CALCLOSS: //计算差损
                            PopAndPushStep(STEP.SwitchPowerSource);
                            break;
                        case STEP.SwitchPowerSource:
                            PopAndPushStep(STEP.ReadPower);
                            break;
                        case STEP.ReadPower:
                            PopAndPushStep(STEP.ShowResultLoss);
                            break;
                        case STEP.ShowResultLoss:
                            PopStep();
                            break;
                       

                        case STEP.DO_NOTHING:
                            PopStep();
                            break;
                        case STEP.EXIT:
                            PopStep();
                            return 0;
                        default:
                            break;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message);
                ShowError(ex.Message);
                return -1;
            }
        }

        #region Private Method
        /// <summary>
        /// 回原点
        /// </summary>
        private bool HomeAll()
        {
            bool bRet = false;
            nSubStep = 1;
            while (!cts.IsCancellationRequested)
            {
                switch (nSubStep)
                {
                    case 1:
                        ShowInfo("Z轴回原点");
                        motion.Home(AXIS_Z, 0, 500, 2, 5);
                        nSubStep = 2;
                        break;
                    case 2:
                        if (motion.IsHomeStop(AXIS_Z))
                        {
                            ShowInfo("X,Y轴回原点");
                            motion.Home(AXIS_Y, 0, 500, 2, 5);
                            motion.Home(AXIS_X, 0, 500, 2, 5);
                            nSubStep = 3;
                        }
                        break;
                    case 3:
                        if (motion.IsHomeStop(AXIS_X) && motion.IsHomeStop(AXIS_Y))
                        {
                            ShowInfo("R轴回原点");
                            motion.Home(AXIS_R, 0, 500, 2, 5);
                            nSubStep = 4;
                        }
                        break;
                    case 4:
                        if (motion.IsHomeStop(AXIS_R))
                        {
                            motion.MoveAbs(AXIS_R, 500, 5, 120);
                            nSubStep = 5;
                        }
                        break;
                    case 5:
                        if (motion.IsNormalStop(AXIS_R))
                        {
                            ShowInfo("回原点完成");
                            bRet = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            return bRet;
        }  
        
        /// <summary>
        /// 耦合
        /// </summary>
        private List<Point3D> DoBlindSearchAlignment(CmdAlignArgs CmdPara)
        {
            var HArgsF = CmdPara.HArgs;
            var VArgsF = CmdPara.VArgs;
            ShowInfo("开始BlindSearch......");
            motion.DoBlindSearch(HArgsF,VArgsF, ADCChannels.CH2, out List<Point3D> Value);
            ShowInfo("BlindSearch完成......");
            return Value;
        }

        protected bool LoadPoint()
        {
            PtCamBottomSnapPos = WorkFlowMgr.Instance.GetPoint("下相机拍照位置");
            PtCamTopSnapPos = WorkFlowMgr.Instance.GetPoint("上相机拍照位置");
            return PtCamBottomSnapPos!=null;
        }

        private void Io_OnIOStateChanged(IIO sender, EnumIOType IOType, ushort OldValue, ushort NewValue)
        {
            if (IOType == EnumIOType.INPUT)
            {
                bool oldPlcSwitchState = (OldValue >> IN_STEP_PLC) == 1;
                bool oldFASwitchState= (OldValue >> IN_STEP_FA) == 1;

                bool newPlcSwitchState= (NewValue >> IN_STEP_PLC) == 1;
                bool newFASwitchState = (NewValue >> IN_STEP_FA) == 1;

                if (newPlcSwitchState != oldPlcSwitchState)
                {
                    if (io.ReadIoOutBit(OUT_STEP_PLC, out bool v))
                    {
                        io.WriteIoOutBit(OUT_STEP_PLC, !v);
                    }
                }
                if (newFASwitchState != oldFASwitchState)
                {
                    if (io.ReadIoOutBit(OUT_STEP_FA, out bool v))
                    {
                        io.WriteIoOutBit(OUT_STEP_FA, !v);
                    }
                }
            }
        }
        #endregion

    }
}

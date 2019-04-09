
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
            Init,   //回机械原点

            AdjustAngle,    //计算并调整角度
           
            AdjustXY, //计算并调整距离

            AdjustXYRel,  //调整相对位置

            DoBlindSearchAlign, //耦合

            DO_NOTHING,
            EXIT,
        }

        private Motion_IrixiEE0017 motion = null;
        private IO_IrixiEE0017 io = null;
        private const int AXIS_X = 0, AXIS_Y = 1, AXIS_Z = 2, AXIS_R = 4;
        STEP Step;
        #region PointDefine
        WFPointModel PtCamBottomSnapPos;    //下相机拍照点
        WFPointModel PtCamTopSnapPos;       //上相机拍照点
        const int IN_STEP_PLC = 0, IN_STEP_FA = 1;
        const int OUT_STEP_PLC = 0, OUT_STEP_FA = 1;

        ShapeModle ModelFindTool = new ShapeModle();
        HalconVision Vision = HalconVision.Instance;

        readonly int CAM_UP = 0;
        readonly int CAM_DOWN = 1;
        readonly int CAM_SIDE = 2;

        const string PATH_MODEL_UP = @"VisionData/Model/ModelUp";
        const string FILE_CALIB_CAM_UP = @"VisionData/Calib/CamUp.tup";
        #endregion

        public override bool UserInit()
        {
            motion = MotionMgr.Instance.FindMotionCardByAxisIndex(1) as Motion_IrixiEE0017;
            io = IOCardMgr.Instance.FindIOCardByCardName("IO_IrixiEE0017[0]") as IO_IrixiEE0017;
            bool bRet = motion != null && io != null && LoadPoint();
            io.OnIOStateChanged += Io_OnIOStateChanged;

            if (!bRet)
                ShowInfo($"初始化失败");
            return bRet;
        }

       

        public WF_Aligner(WorkFlowConfig cfg) : base(cfg)
        {

        }
        protected override int WorkFlow()
        {
            try
            {
                ClearAllStep();
                PushStep(STEP.Init);
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
                        case STEP.Init: //初始化
                            HomeAll();
                            PopStep();
                            break;
                        case STEP.AdjustAngle:  //调整角度
                            AdjectAngle();
                            PopStep();
                            break;
                        case STEP.AdjustXY:   //调整距离
                            AdjustXY();
                            PopStep();
                            break;
                        case STEP.AdjustXYRel:    //调整相对位置
                            AdjustXYRel();
                            PopStep();
                            break;
   
                        case STEP.DoBlindSearchAlign:
                            DoBlindSearchAlignment(null);
                            PopStep();
                            break;
                        case STEP.EXIT:
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
        /// 调整角度
        /// </summary>
        private void AdjectAngle()
        {
            var dog = new Dog(30000);
            nSubStep = 1;
            while (!cts.IsCancellationRequested)
            {
                dog.CheckTimeOut("调整角度超时");
                switch (nSubStep)
                {
                    //移动到拍照位置
                    case 1:
                        motion.MoveAbs(AXIS_Z, 1000, 5, 0);
                        break;
                    case 2:
                        if (motion.IsNormalStop(AXIS_Z))
                        {
                            nSubStep = 3;
                        }
                        break;
                    case 3:
                        motion.MoveAbs(AXIS_X, 1000, 5, PtCamBottomSnapPos.X);
                        motion.MoveAbs(AXIS_Y, 1000, 5, PtCamBottomSnapPos.Y);
                        nSubStep = 4;
                        break;
                    case 4:
                        if (motion.IsNormalStop(AXIS_X) && motion.IsNormalStop(AXIS_Y))
                        {
                            motion.MoveAbs(AXIS_Y, 1000, 5, PtCamBottomSnapPos.Z);
                            nSubStep = 5;
                        }
                        break;
                    case 5:
                        //Snap
                        var Image=Vision.GrabImage(CAM_DOWN, true, false);
                        ModelFindTool.BackImage = Image;
                        ModelFindTool.LoadModle(PATH_MODEL_UP);
                        var Result=ModelFindTool.FindSimple();

                        nSubStep = 6;
                        break;
                    
                    case 6:
                        //旋转
                        return;
                   
                }
            }
        }

        private void AdjustXY()
        {
            var dog = new Dog(30000);
            nSubStep = 1;
            while (!cts.IsCancellationRequested)
            {
                dog.CheckTimeOut("调整角度超时");
                switch (nSubStep)
                {
                    //移动到拍照位置
                    case 1:
                        motion.MoveAbs(AXIS_Z, 1000, 5, 0);
                        break;
                    case 2:
                        if (motion.IsNormalStop(AXIS_Z))
                        {
                            nSubStep = 3;
                        }
                        break;
                    case 3:
                        motion.MoveAbs(AXIS_X, 1000, 5, PtCamBottomSnapPos.X);
                        motion.MoveAbs(AXIS_Y, 1000, 5, PtCamBottomSnapPos.Y);
                        nSubStep = 4;
                        break;
                    case 4:
                        if (motion.IsNormalStop(AXIS_X) && motion.IsNormalStop(AXIS_Y))
                        {
                            motion.MoveAbs(AXIS_Y, 1000, 5, PtCamBottomSnapPos.Z);
                            nSubStep = 5;
                        }
                        break;
                    case 5:
                        //Snap
                        var Image = Vision.GrabImage(CAM_DOWN, true, false);
                        ModelFindTool.BackImage = Image;
                        ModelFindTool.LoadModle(PATH_MODEL_UP);
                        var Result = ModelFindTool.FindSimple();
                        
                        nSubStep = 6;
                        break;
                    case 6:
                        //MoveXY

                        return;
                }
            }
        }

        //精确调整
        private void AdjustXYRel()
        {
            var dog = new Dog(30000);
            nSubStep = 1;
            while (!cts.IsCancellationRequested)
            {
                dog.CheckTimeOut("调整角度超时");
                switch (nSubStep)
                {
                    //移动到拍照位置
                    case 1:
                        motion.MoveAbs(AXIS_Z, 1000, 5, 0);
                        break;
                    case 2:
                        if (motion.IsNormalStop(AXIS_Z))
                        {
                            nSubStep = 3;
                        }
                        break;
                    case 3:
                        motion.MoveAbs(AXIS_X, 1000, 5, PtCamTopSnapPos.X);
                        motion.MoveAbs(AXIS_Y, 1000, 5, PtCamTopSnapPos.Y);
                        nSubStep = 4;
                        break;
                    case 4:
                        if (motion.IsNormalStop(AXIS_X) && motion.IsNormalStop(AXIS_Y))
                        {
                            motion.MoveAbs(AXIS_Y, 1000, 5, PtCamBottomSnapPos.Z);
                            nSubStep = 5;
                        }
                        break;
                    case 5:
                        //Snap
                        Vision.GrabImage(CAM_UP, true);
                        nSubStep = 6;
                        break;
                    case 6:
                        //Rotate
                        nSubStep = 7;
                        break;
                    case 7:
                        //Snap
                        nSubStep = 8;
                        break;
                    case 8:
                        //MoveXYRel
                        return;
                }
            }
        }

        /// <summary>
        /// 耦合
        /// </summary>
        private void DoBlindSearchAlignment(CmdAlignArgs CmdPara)
        {
            var HArgsF = CmdPara.HArgs;
            var VArgsF = CmdPara.VArgs;
            ShowInfo("开始耦合......");
            motion.DoBlindSearch(HArgsF,VArgsF, ADCChannels.CH2, out List<Point3D> Value);
            CmdPara.QResult=Value;
            CmdPara.FireFinishAlimentEvent();
            ShowInfo("耦合完成......");
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

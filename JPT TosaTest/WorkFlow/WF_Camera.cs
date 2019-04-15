
using JPT_TosaTest.Config.SoftwareManager;
using JPT_TosaTest.IOCards;
using JPT_TosaTest.MotionCards;
using JPT_TosaTest.MotionCards.IrixiCommand;
using VisionLib;
using JPT_TosaTest.WorkFlow.WorkFlow;
using M12.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JPT_TosaTest.WorkFlow
{
    public class WF_Camera : WorkFlowBase
    {
        public enum STEP : int
        {
            Init,
            DO_NOTHING,
            EXIT,
        }
        STEP Step;
        private const int CAM_TOP = 0, CAM_BACK = 1;
        public override bool UserInit()
        {
            return false;
        }
        public WF_Camera(WorkFlowConfig cfg) : base(cfg)
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
                    if (bPause || b == false)
                        continue;
                    Step = (STEP)n;
                    switch (Step)
                    {
                        case STEP.Init:
                            HalconVision.Instance.GrabImage(CAM_TOP,true,true);
                            HalconVision.Instance.GrabImage(CAM_BACK,true,true);
                            ShowInfo("连续采集中......");
                            break;
                        case STEP.EXIT:
                            return 0;
                        default:
                            break;
                    }
                }
                return 0;
            }
            catch(Exception ex)
            {
                ShowInfo(ex.Message);
                ShowError(ex.Message);
                return -1;
            }
        }

   
    }
}

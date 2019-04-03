using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using JPT_TosaTest.Config.SoftwareManager;
using JPT_TosaTest.Config.SystemCfgManager;
using JPT_TosaTest.WorkFlow.CmdArgs;

namespace JPT_TosaTest.WorkFlow.WorkFlow
{
    public enum EnumProductType
    {
        SUPPORT,
        PLC,
    }
    public enum EnumRTShowType
    {
        Support,
        Tia,
        None,
    }
    public delegate void StationInfoHandler(int Index, string StationName, string Msg);
    public class WorkFlowBase
    {
        public bool Enable;
        public string StationName;
        public int StationIndex;
        protected bool bPause = false;
        protected Queue<CmdArgsBase> CmdParaQueue = null;
        public event StationInfoHandler OnStationInfoChanged;
        protected WorkFlowConfig cfg = null;
        protected CancellationTokenSource cts =new CancellationTokenSource();
        protected Queue<int> nStepQueue=new Queue<int>();
        protected Task t = null;
        protected int nSubStep = 0;
        private object _lock = new object();
        public WorkFlowBase(WorkFlowConfig cfg)
        {
            CmdParaQueue = new Queue<CmdArgsBase>();
            this.cfg = cfg;
        }
        protected int PeekStep()
        {
            try
            {
                lock (_lock)
                {
                    if(nStepQueue.Count>0)
                        return nStepQueue.Peek();
                    return -1;
                }
            }
            catch
            {
                return -1;
            }

        }
        protected void PushStep<T>(T Step, CmdArgsBase para=null) where T:struct {
            lock (_lock)
            {
                nStepQueue.Enqueue(Step.GetHashCode());
                if(para!=null)
                    CmdParaQueue.Enqueue(para);
            }
        }
        protected void PopAndPushStep<T>(T Step)
        {
            lock (_lock)
            {
                nStepQueue.Dequeue();
                nStepQueue.Enqueue(Step.GetHashCode());
            }
        }
        protected void PushBatchStep<T>(T[] nSteps) where T:struct
        {
            foreach (var step in nSteps)
                nStepQueue.Enqueue(step.GetHashCode());
        }
        protected void PopStep()
        {
            lock (_lock)
            {
                nStepQueue.Dequeue();
            }
        }
        public void ClearAllStep()
        {
            lock (_lock)
            {
                nStepQueue.Clear();
            }
        }
        protected int GetCurStepCount()
        {
            lock (_lock)
            {
                return nStepQueue.Count;
            }
        }
        public virtual bool UserInit() { return true; }

        public void ShowInfo(string strInfo=null)    //int msg, int iPara, object lParam
        {
            if (strInfo == null || strInfo.Trim().ToString() == "")
                return;
            DateTime dt = DateTime.Now;
            OnStationInfoChanged?.Invoke(StationIndex, cfg.Name, string.Format("{0:D2}:{1:D2}:{2:D2}  {3:D2}", dt.Hour, dt.Minute, dt.Second, strInfo));
        }
        public bool Start()
        {
            bPause = false;
            if (t==null || t.Status == TaskStatus.Canceled || t.Status == TaskStatus.RanToCompletion)
            {
                cts = new CancellationTokenSource();
                t = new Task(() => ThreadFunc(this), cts.Token);
                t.Start();
            }
            return true;
        }
        public bool Stop()
        {
            cts.Cancel();
            nSubStep = 0;
            return true;
        }
        public bool Pause()
        {
            this.bPause = true;
            return true;
        }
        private static int ThreadFunc(object o) { return (o as WorkFlowBase).WorkFlow(); }
        protected virtual int WorkFlow() { return 0; }
        public void WaitComplete()
        {
            //if (t != null)
            //    t.Wait(5000);
        }
        protected SystemParaModel SysPara = null;
        protected void ShowError(string ErrorMsg)
        {
            Messenger.Default.Send<string>(ErrorMsg, "Error");
        }

        public void SetCmd<T>(T step,CmdArgsBase para=null) where T:struct
        {
            PushStep(step.GetHashCode(),para);
        }

        public int GetCurCmd()
        {
            return PeekStep();
        }

       
    }
}

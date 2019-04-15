using JPT_TosaTest.Config.HardwareManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JPT_TosaTest.Instrument.PowerMeter
{
    public class PowerMeterBase : InstrumentBase, INotifyPropertyChanged
    {
        public PowerMeterBase(InstrumentCfg InstrumentCfg, ICommunicationPortCfg CommunicationCfg) : base(InstrumentCfg, CommunicationCfg) { }
        #region Variables

        protected SerialPort serialport;
        protected CancellationTokenSource cts_fetching;
        protected Progress<EventArgs> autoFetchProgressChangedHandler;
        protected Task taskAutoFetch = null;
        int activeChannel;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Properties

        /// <summary>
        /// Get whether this instrument contains multiple channel
        /// </summary>
        public bool IsMultiChannel
        {
            protected set;
            get;
        }

        /// <summary>
        /// Get which channel is the active one that represents the return value of fetch() function
        /// </summary>
        public int ActivedChannel
        {
            protected set
            {
                if (activeChannel != value)
                {
                    activeChannel = value;
                    RaisePropertyChanged();
                }
            }
            get
            {
                return activeChannel;
            }
        }

        /// <summary>
        /// Get what is the unit of the active channel
        /// </summary>
        //public int ActiveUnit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion

        #region Methods

        public virtual string GetDescription()
        {
            return Read("*IDN?");
        }

        public virtual void Reset()
        {
            Send("*CLS");
            Thread.Sleep(50);

            Send("*RST"); // reset device to default setting
            Thread.Sleep(200);
        }

        public override bool Init()
        {
#if !FAKE_ME
            try
            {
                ComportCfg cfg = CommunicationCfg as ComportCfg;
                serialport.PortName = $"COM{cfg.Port}";
                serialport.BaudRate = cfg.BaudRate;
                serialport.DataBits = cfg.DataBits;
                switch (cfg.StopBits)
                {
                    case 0:
                        serialport.StopBits = StopBits.None;
                        break;
                    case 1:
                        serialport.StopBits = StopBits.One;
                        break;
                    case 2:
                        serialport.StopBits = StopBits.Two;
                        break;
                    default:
                        serialport.StopBits = StopBits.OnePointFive;
                        break;

                }
                switch (cfg.Parity)
                {
                    case "n":
                    case "N":
                        serialport.Parity = Parity.None;
                        break;
                    case "e":
                    case "E":
                        serialport.Parity = Parity.Even;
                        break;
                    case "o":
                    case "O":
                        serialport.Parity = Parity.Odd;
                        break;
                    default:
                        serialport.Parity = Parity.None;
                        break;
                }
                serialport.Open();
                Task.Delay(100);
                // run user init process function
                UserInitProc();
                this.IsInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    serialport.Close();
                }
                catch
                {

                }
                LastError = ex.Message;
                return false;
            }
#else
            UserInitProc();
            this.IsInitialized = true;


            return true;
#endif
        }

        public void StartAutoFetching()
        {
            // check whether the task had been started
            if (taskAutoFetch == null || taskAutoFetch.IsCompleted)
            {
                // token source to cancel the task
                cts_fetching = new CancellationTokenSource();
                autoFetchProgressChangedHandler = new Progress<EventArgs>(AutoFetchReport);

                var token = cts_fetching.Token;

                // start the loop task
                taskAutoFetch = Task.Run(() =>
                {
                    DoAutoFecth(token, autoFetchProgressChangedHandler);
                });

                // if error, throw it

                taskAutoFetch.ContinueWith(t => throw t.Exception, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        public void PauseAutoFetching()
        {
            if (taskAutoFetch != null && taskAutoFetch.IsCompleted == false)
            {
                // cancel the task of fetching loop
                cts_fetching.Cancel();
                TimeSpan ts = TimeSpan.FromMilliseconds(2000);
                if (!taskAutoFetch.Wait(ts))
                    throw new TimeoutException("unable to stop the fetching loop task");
            }
        }
        public virtual void ResumeAutoFetching()
        {
            if (taskAutoFetch != null)
                StartAutoFetching();
        }
        public virtual void StopAutoFetching()
        {
            if (taskAutoFetch != null)
            {

                if (taskAutoFetch.IsCompleted == false)
                {
                    PauseAutoFetching();
                }

                // destory the objects
                taskAutoFetch = null;
                cts_fetching = null;
            }
        }

        protected bool disposedValue = false;
        public override bool DeInit()
        {      
            try
            {
                //! Remember to run the stop function, otherwise the app may NOT exit currectly
                StopAutoFetching();
                // run user's dispose process
                UserDisposeProc();
                // close serial port and destory the object
                serialport.Close();
                return true;
            }
            catch
            {
                serialport = null;
                return false;
                }
        }

        #endregion

        #region Methods implemented by user

        protected virtual void UserInitProc()
        {
            throw new NotImplementedException();
        }

        public virtual double Fetch()
        {
            throw new NotImplementedException();
        }

        public virtual double Fetch(int Channel)
        {
            throw new NotImplementedException();
        }

        protected virtual void DoAutoFecth(CancellationToken token, IProgress<EventArgs> progress)
        {
            throw new NotImplementedException();
        }

        protected virtual void AutoFetchReport(EventArgs Args)
        {
            throw new NotImplementedException();
        }

        protected virtual void UserDisposeProc()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        protected virtual void Send(string Command)
        {
            try
            {
                lock (serialport)
                {
                    serialport.WriteLine(Command);

                    Thread.Sleep(10);

                    // check if error occured
                    serialport.WriteLine("*ERR?");
                    var ret = serialport.ReadLine();
                    var errornum = ret.Split(',')[0];

                    if (int.TryParse(errornum, out int err_count))
                    {
                        if (err_count != 0) // error occured
                        {
                            // read all errors occured
                            var err = ret.Split(',')[1];
                            throw new InvalidOperationException(err);
                        }
                    }
                    else
                    {
                        throw new InvalidCastException(string.Format("unable to convert error count {0} to number", err_count));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual string Read(string Command)
        {

            try
            {
                lock (serialport)
                {
                    serialport.WriteLine(Command);
                    return serialport.ReadLine().Replace("\r", "").Replace("\n", "");
                }
            }
            catch (TimeoutException)
            {
                // read all errors occured
                serialport.WriteLine("*ERR?");
                this.LastError = serialport.ReadLine();
                throw new InvalidOperationException(this.LastError);
            }
            catch (Exception ex)
            {
                this.LastError = ex.Message;
                throw ex;
            }
        }

        #endregion

        #region Private Method
        private void RaisePropertyChanged([CallerMemberName]string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}


using JPT_TosaTest.Config.HardwareManager;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPT_TosaTest.Instrument
{
    public abstract class InstrumentBase
    {
        protected InstrumentCfg InstrumentCfg = null;
        protected ICommunicationPortCfg CommunicationCfg = null;
        //Comport
        protected SerialPort comPort = null;
        public int Index = -1;
        protected object _lock = new object();
        public virtual bool Init() {
            throw new NotImplementedException();
        }
        public virtual bool DeInit() {
            throw new NotImplementedException();
        }
        public bool IsInitialized { get; protected set; }
        public string LastError { get; protected set; }
        public InstrumentBase(InstrumentCfg InstrumentCfg, ICommunicationPortCfg CommunicationCfg)
        {
            this.InstrumentCfg = InstrumentCfg;
            this.CommunicationCfg = CommunicationCfg;
        }

    }
}
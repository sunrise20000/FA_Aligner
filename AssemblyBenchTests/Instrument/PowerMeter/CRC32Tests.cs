using Microsoft.VisualStudio.TestTools.UnitTesting;
using JPT_TosaTest.Instrument.PowerMeter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPT_TosaTest.Config.HardwareManager;
using JPT_TosaTest.MotionCards;
using M12.Definitions;

namespace JPT_TosaTest.Instrument.PowerMeter.Tests
{
    [TestClass()]
    public class CRC32Tests
    {

        [TestMethod()]
        public void FetchTest()
        {
            var InsCfg=new Config.HardwareManager.InstrumentCfg()
            {
                ConnectMode = "Comport",
                Enabled = true,
                InstrumentName = "2832",
                PortName = "2832"

            };
            var ComCfg = new ComportCfg()
            {
                BaudRate = 9600,
                Parity = "n",
                Port = "COM11",
                StopBits = 1,
                TimeOut = 1000,
                DataBits = 8,
            };
            Newport2832C Ins2832 = new Newport2832C(InsCfg, ComCfg);
            Ins2832.Init();
            var DesStr= Ins2832.GetDescription();
            var V=Ins2832.Fetch();
            Ins2832.DeInit();
            Console.WriteLine(V);
        }

        [TestMethod()]
        public void ReadADCTest()
        {
            var M12 = M12Wrapper.CreateInstance("COM7",115200);
            M12.Open();
            var x=M12.ReadADC(ADCChannels.CH1);
            Console.WriteLine(x[0]);
            M12.Close();
        }
    }
}
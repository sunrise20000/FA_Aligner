using AxisParaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPT_TosaTest.MotionCards
{
    public class AxisStateChangeArgs
    {
        public int AxisNoBaseZero { get; set; }
        public AxisArgs axisState { get; set; }
    }
}

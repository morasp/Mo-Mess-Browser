using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo_Mess.Sensor
{
    public abstract class SensorHelper
    {
        protected abstract void init();
        protected double Value;
        public abstract double getValue();
        public abstract int getIntValue();




        
        
       
    }
}

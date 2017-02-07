using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo_Mess_Browser.Plugins.Contracts
{
    public interface ICalculatorPlugin
    {
        string Name { get; }
        bool run { get; set; }
        bool initComplete { get; set; }
        void init();
        void calc();
        void calc(double value);
        void onClick(object sender, EventArgs e);
        
    }
}

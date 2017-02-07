using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mo_Mess_Browser.Plugins.Contracts;
using System.Drawing;

namespace testplugin
{
    public class testPlugin : ICalculatorPlugin
    {
        public bool initComplete
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "testPlugin";
            }
        }

        public bool run
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void calc()
        {
            
        }

        public void calc(double value)
        {
            
        }

        public void init()
        {
           
        }

        public void onClick(object sender, EventArgs e)
        {
            System.Windows.Forms.Form f = new System.Windows.Forms.Form();
            f.Size = new Size(100, 100);
            f.Show();
            f.Text = "test";
            
        }
    }
}

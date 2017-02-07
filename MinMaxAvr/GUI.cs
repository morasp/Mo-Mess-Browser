using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Messs_Browser.Plugins.MinMaxAvr
{
    public partial class GUI : Form
    {
        private delegate void setTextCallback(Label l, string text);
        public delegate void ResetEventHandler(object sender, EventArgs e);
        public event ResetEventHandler Reset;
        public bool run = false;
        private double _min;
        public double min {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
                setLabel(lbl_min_v, _min.ToString("F2"));
            } 
                
        }
        private double _max;
        public double max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                setLabel(lbl_max_v,_max.ToString("F2"));
            }

        }
        private double _avr;
        public double avr
        {
            get
            {
                return _avr;
            }
            set
            {
                _avr = value;
                setLabel(lbl_avr_v, _avr.ToString("F2"));
            }

        }
        public GUI()
        {
            InitializeComponent();
            
        }

        private void cb_aktiviert_CheckedChanged(object sender, EventArgs e)
        {
            run = ((CheckBox)sender).Checked;
            Console.WriteLine(((CheckBox)sender).Checked);
            
        }
        private void setLabel(Label l, string text)
        {
            try
            {

                if (l.InvokeRequired)
                {
                    setTextCallback t = new setTextCallback(setLabel);
                    Invoke(t, new object[] { l, text });
                }
                else
                {
                    l.Text = text;
                }
            }
            catch(ObjectDisposedException odex )
            {
                Console.Write(odex.Message);
            };
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            _min = new double(); 
            _max = new double();
            _avr = 0;
            Reset(this, EventArgs.Empty);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Mo_Mess_Browser.Plugins.Contracts;


namespace Mo_Messs_Browser.Plugins.MinMaxAvr 
{
    public class Class1 : ICalculatorPlugin
    {
        GUI gui;
        ulong pointCount = 0;
        double PointSum = 0;

        public bool initComplete
        {
            get;
            set;  
        }

        public string Name
        {
            get
            {
                return "MoMess-Min-Max-Avr";
            }
        }

        public bool run
        {
            get; set;
        }

        public void calc()
        {
            
        }

        public void calc(double value)
        {
            try
            {
                //Console.WriteLine(gui.run);
                if (gui.run)
                {
                    gui.min = value < gui.min ? value : gui.min;
                    gui.max = value > gui.max ? value : gui.max;
                    PointSum += value;
                    pointCount++;
                    gui.avr = PointSum / pointCount;

                }
            }
            catch (NullReferenceException)
            {
                Console.Write("NullReferenceException");
            }
        }

        public void init()
        {
            gui = new GUI();
            gui.Reset += Gui_Reset;
        }

        private void Gui_Reset(object sender, EventArgs e)
        {
            pointCount = 0;
            PointSum = 0;
        }

        public void onClick(object sender, EventArgs e)
        {
            gui.Show();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo_Mess.Browser
{
    public class Panel2 : System.Windows.Forms.Panel
    {
        public SensorProvider sensor;
        public bool WindowShown = false;
        public ShowSensor form;

        public void showForm(ShowSensorHelper MDIParent)
        {

            form = new ShowSensor(sensor);
            form.MdiParent = MDIParent;
            form.MdiParent.Show();
            form.Show();


        }
    }
}


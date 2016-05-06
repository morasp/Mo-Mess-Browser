using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mo_Mess.Browser
{
    public partial class ShowSensorHelper : Form
    {
        private List<ShowSensor> internalForms = new List<ShowSensor>();
        public ShowSensorHelper()
        {
            InitializeComponent();
        }

        private void ShowSensorHelper_Load(object sender, EventArgs e)
        {

        }
        public void addForm(ShowSensor form)
        {
            int temp = internalForms.Count;
            internalForms.Add(form);
            internalForms[temp].MdiParent = this;
            internalForms[temp].Show();
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void vertikalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void voreinanderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }
    }
}

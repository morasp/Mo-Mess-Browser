using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace Mo_Mess.Browser
{
    public partial class Form1 : Form
    {
        bool open = true;
        UdpClient udpBroadcastClient = new UdpClient(11000);
        IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 11000);
        List<Panel> geraete = new List<Panel>();
        private delegate void addPanelCallback(Panel p);
        Thread t;
        ShowSensorHelper sensorFrame = new ShowSensorHelper();
        public Form1()
        {
            InitializeComponent();
            
            
            t = new Thread(new ThreadStart(broadcastClient));

            t.Start();
        }
        public void broadcastClient()
        {
            

            try
            {
                while (open)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = udpBroadcastClient.Receive(ref ipe);

                    Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                        ipe.ToString(),
                        Encoding.ASCII.GetString(bytes, 0, bytes.Length));

                    SensorProvider sensor = SensorProvider.SensorFromByteArray(bytes, ipe.Address.ToString());
                   
                    addPaneltoMain(MainPanelFromDevice(sensor));
                    addPaneltoSide(SidePanelFromDevice(sensor));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                udpBroadcastClient.Close();
            }
        }

        private Panel SidePanelFromDevice(SensorProvider s)
        {
            Panel2 P = new Panel2();
            P.sensor = s;
            P.Name = "panel-" + s.name;
            P.BackColor = Color.DarkGray;
            P.BorderStyle = BorderStyle.FixedSingle;
            
            P.Width = flp_side.Width;
            P.Height = 20;
            Label l = new Label();

            l.Text = s.name;
            l.ForeColor = Color.WhiteSmoke;
            //l.Location = new Point(0, (P.Height - l.Height) - 10);
            
            P.Controls.Add(l);
            
            P.Click += P_Click;
            P.form = new ShowSensor(P.sensor);
            return P;
        }

        private Panel MainPanelFromDevice(SensorProvider s)
        {
            Panel2 P = new Panel2();
            P.sensor = s;
            P.Name = "panel-" + s.name;
            P.BackColor = Color.DarkGray;
            P.BorderStyle = BorderStyle.Fixed3D;
            P.Margin = new Padding(20);
            P.Width = 100;
            P.Height = 100;
            PictureBox pix = new PictureBox();
            pix.BackgroundImage = Bitmap.FromFile(@"Image1.bmp");
            pix.SizeMode = PictureBoxSizeMode.Zoom;
            P.Controls.Add(pix);
            Label l = new Label();
            l.BackColor = Color.Transparent;
           
            l.Text = s.name;
            l.ForeColor = Color.WhiteSmoke;
            l.Location = new Point(0, (P.Height - l.Height)-10);
            P.Controls.Add(l);
            Label type = new Label();
            type.Text = s.Type.ToString();
            type.ForeColor = Color.WhiteSmoke;
            type.Location = new Point(0, (P.Height - type.Height) - 20);
            P.Controls.Add(type);
            P.Click += P_Click;
            P.MouseDown += P_mouseDown_Animation;
            P.MouseUp += P_MouseUp_Animation;
            P.form = new ShowSensor(P.sensor);
            return P;
            
        }

        private void P_MouseUp_Animation(object sender, MouseEventArgs e)
        {
            ((Panel)sender).BackColor = Color.DarkGray;
        }

        private void P_mouseDown_Animation(object sender, MouseEventArgs e)
        {
            ((Panel)sender).BackColor = Color.Red;
        }

        private void P_Click(object sender, EventArgs e)
        {
            
            Panel2 P = (Panel2)sender;
           // P.form.MdiParent = sensorFrame;
            if (P.form != null && P.form.Visible && sensorFrame.Visible)
            {
                sensorFrame.Focus();
                P.form.Focus();
            }
            else {

                
               
                P.showForm(sensorFrame);
            }
        }

        private void addPaneltoSide(Panel p)
        {
            if (this.flp_side.InvokeRequired)
            {
                addPanelCallback d = new addPanelCallback(addPaneltoSide);
                this.Invoke(d, new object[] { p });
            }
            else
            {
                this.flp_side.Controls.Add(p);
            }
            
        }
        private void addPaneltoMain(Panel p)
        {
            if (this.flp_main.InvokeRequired)
            {
               addPanelCallback d = new addPanelCallback(addPaneltoMain);
                this.Invoke(d, new object[] { p });
            }
            else
            {
                this.flp_main.Controls.Add(p);
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            open = false;
            udpBroadcastClient.Close();
            t.Join();
           Application.Exit();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }


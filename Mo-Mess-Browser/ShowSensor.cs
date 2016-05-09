using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace Mo_Mess
{
    public partial class ShowSensor : Form
    {
        SensorProvider sensor;

        Socket SensorListener;
        TcpClient Client = new TcpClient(new IPEndPoint(IPAddress.Any, 0));

        Thread reader;
        Mo_Mess_Browser.buffer<double> buffer = new Mo_Mess_Browser.buffer<double>(50);



        private delegate void addPointCallback(System.Windows.Forms.DataVisualization.Charting.Series s, double Value);


        #region Konstruktoren
        public ShowSensor(object s, ref object sender)
        {
            InitializeComponent();
            sensor = (SensorProvider)s;
            this.Text = "Sensor: " + sensor.name;

            SensorListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            reader = new Thread(new ThreadStart(read));
            comboBox1.DataSource = Enum.GetValues(typeof(SensorProvider.type));
            comboBox1.SelectedItem = sensor.Type;
            textBox1.Text = sensor.id.ToString();
            textBox2.Text = sensor.address.ToString();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            ((Browser.Panel2)sender).WindowShown = true;

        }
        public ShowSensor(object s)
        {
            InitializeComponent();
            sensor = (SensorProvider)s;
            this.Text = "Sensor: " + sensor.name;

            SensorListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            reader = new Thread(new ThreadStart(read));
            comboBox1.DataSource = Enum.GetValues(typeof(SensorProvider.type));
            comboBox1.SelectedItem = sensor.Type;
            textBox1.Text = sensor.id.ToString();
            textBox2.Text = sensor.address.ToString();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.ResetZoom;


        }
        #endregion

        private void ShowSensor_Load(object sender, EventArgs e)
        {

            OptimizeOfLocalFormsOnly(this);

            Thread t = new Thread(new ThreadStart(read));
            t.Start();
            Thread readb = new Thread(new ThreadStart(readbuffer));
            readb.Start();

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        public void read()
        {


            Client.Connect(new IPEndPoint(sensor.address, 9999));
            NetworkStream ns = Client.GetStream();
            StreamReader sr = new StreamReader(ns);
            String data = "";
            Console.WriteLine("Lese Daten");
            while (Client.Connected && !sr.EndOfStream)
            {
                try {
                    data = sr.ReadLine();
                    double Value = Double.Parse(data);
                    buffer.add(Value);
                } catch (IOException ioex)
                {
                    break;
                }
            }

        }

        public void AddPoint(System.Windows.Forms.DataVisualization.Charting.Series s, double Value)
        {
            if (this.chart1.InvokeRequired)
            {
               
                    addPointCallback d = new addPointCallback(AddPoint);
                try
                {
                    this.Invoke(d, new object[] { s, Value });
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            else
            {
                try
                {
                    s.Points.Add(Value);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void ShowSensor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Client.Connected)
                Client.Close();


        }

        public bool OptimizeOfLocalFormsOnly(System.Windows.Forms.Control chartControlForm)
        {
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                SetUpDoubleBuffer(chartControlForm);
                return true;
            }
            return false;

        }

        public static void SetUpDoubleBuffer(System.Windows.Forms.Control chartControlForm)
        {

            System.Reflection.PropertyInfo formProp =
            typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            formProp.SetValue(chartControlForm, true, null);
        }

        public void readbuffer()
        {
            while (true)
            {

                while (buffer.dataInBuffer)
                {
                    AddPoint(chart1.Series[0], buffer.read());
                    Console.WriteLine(buffer.Count);
                }
            }
        }

    }
}


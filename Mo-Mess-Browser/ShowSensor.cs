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
using System.Reflection;
using Mo_Mess_Browser.Plugins.Contracts;
using SimplePlugin;


namespace Mo_Mess
{
    public partial class ShowSensor : Form
    {
        SensorProvider sensor;

        Socket SensorListener;
        TcpClient Client = new TcpClient(new IPEndPoint(IPAddress.Any, 0));
        bool pluginsGeladen = false;
        Thread reader;
        Mo_Mess_Browser.buffer<double> buffer = new Mo_Mess_Browser.buffer<double>(50);
        Dictionary<string, ICalculatorPlugin> _Plugins = new Dictionary<string, ICalculatorPlugin>();
        List<double> allValues = new List<double>();
        long horizontalCount = 100;

        private delegate void addPointCallback(System.Windows.Forms.DataVisualization.Charting.Series s, double Value);
        private delegate void clearPointsCallback();
        Browser.Form1 mainForm;
        Thread readb;
        bool run = false;

        #region Konstruktoren
        public ShowSensor(object s, object sender, Browser.Panel2 panel)
        {
            InitializeComponent();
            sensor = (SensorProvider)s;
            mainForm = (Browser.Form1)sender;
            mainForm.Commited += MainForm_Commited;
            this.Text = "Sensor: " + sensor.name;

            SensorListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            reader = new Thread(new ThreadStart(read));
            comboBox1.DataSource = Enum.GetValues(typeof(SensorProvider.type));
            comboBox1.SelectedItem = sensor.Type;
            textBox1.Text = sensor.id.ToString();
            textBox2.Text = sensor.address.ToString();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            ((Browser.Panel2)panel).WindowShown = true;

        }

        private void MainForm_Commited(object sender, EventArgs e)
        {
            buffer = new Mo_Mess_Browser.buffer<double>(mainForm.bufferSize);
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
           

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 100;

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
                    allValues.Add(Value);
                    Console.WriteLine(allValues.Count);
                    buffer.add(Value);
                    if (pluginsGeladen)
                    {
                       foreach(KeyValuePair<string,ICalculatorPlugin> p in _Plugins)
                        {
                            p.Value.calc(Value);
                        }
                    }
                } catch (IOException ioex)
                {
                    break;
                }catch(ObjectDisposedException odex)
                {
                    sr.Close();
                    ns.Close();
                    Client.Close();
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
            while (true&& run)
            {

                while (buffer.dataInBuffer && run)
                {
                    try
                    {
                        if (cb_mode.Checked)
                        {
                            if (chart1.Series[0].Points.Count >= horizontalCount)
                            {
                                clearPoints();
                                allValues.Clear();

                                //chart1.Series[0].Points.Clear();
                            }
                        }
                        AddPoint(chart1.Series[0], buffer.read());
                    }
                    catch
                    {

                    }
                    

                }
            }
        }

        private void clearPoints()
        {
            if (this.chart1.InvokeRequired)
            {

                clearPointsCallback c = new clearPointsCallback(clearPoints);
                                    try
                {
                    this.Invoke(c);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                try
                {
                   
                    chart1.Series[0].Points.Clear();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void pluginsLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MoritzSpeidel\Mo_Mess_Browser\Plugins";
            Console.WriteLine(path);
            string[] dllFileNames = null;
            if (Directory.Exists(path))
            {
                ICollection<Assembly> assemblies;
                try { 
                dllFileNames = Directory.GetFiles(path, "*.dll");

               assemblies  = new List<Assembly>(dllFileNames.Length);

                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);


                }
            }catch(Exception ex)
                {
                    MessageBox.Show("Es konnten keine funktionsfähigen Plugins gefunden werden.", "Keine Plugins gefunden", MessageBoxButtons.OK);
                    return;
                }
                Type pluginType = typeof(ICalculatorPlugin);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();
                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                    pluginsGeladen = true;
                }

                ICollection<ICalculatorPlugin> plugins = new List<ICalculatorPlugin>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    ICalculatorPlugin plugin = (ICalculatorPlugin)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }
                
                ICollection<ICalculatorPlugin> plugins2 = PluginLoader.LoadPlugins("Plugins");
                foreach (var item in plugins)
                {
                    _Plugins.Add(item.Name, item);
                    pluginsToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(item.Name, new Bitmap(1, 1), onPluginClick));
                    item.init();
                }
            }
        }

        private void onPluginClick(object sender, EventArgs e)
        {
            //MessageBox.Show(((ToolStripMenuItem)sender).Text);
            Console.WriteLine(((ToolStripMenuItem)sender).Text);
            _Plugins[((ToolStripMenuItem)sender).Text].onClick(this,EventArgs.Empty);
        }

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ShowSensor_FormClosing2(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            e.Cancel = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            horizontalCount =(long) numericUpDown1.Value;
            chart1.ChartAreas[0].AxisX.Maximum = horizontalCount;
        }

        private void cb_mode_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_mode.Checked)
            {
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = horizontalCount;
            }else
            {
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                chart1.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                buffer.Clear();
                run = true;
                readb = new Thread(new ThreadStart(readbuffer));
                readb.Start();
            }
            else
            {
                run = false;
            }
        }

       
        private void readBufferSingle(long lenght)
        {
            long l = 0;
            while (l < lenght)
            {
                while (buffer.dataInBuffer && l < lenght)
                {
                    try
                    {
                        if (cb_mode.Checked)
                        {
                            if (chart1.Series[0].Points.Count >= horizontalCount)
                            {
                                clearPoints();
                                allValues.Clear();

                                //chart1.Series[0].Points.Clear();
                            }
                        }
                        AddPoint(chart1.Series[0], buffer.read());
                        l++;
                    }
                    catch
                    {

                    }


                }
            }
        }

        private void cb_single_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_single.Checked)
            {
                buffer.Clear();
                readBufferSingle(horizontalCount);
                cb_single.Checked = false;
            }
            else
            {

            }
        }

        private void cb_trigger_auto_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SerialTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = Parity.None;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;
            serialPort1.PortName = portComboBox.Text;
            serialPort1.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                portComboBox.Items.Add(port);
            }
            if (portComboBox.Items.Count > 0)
                portComboBox.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(textBox2.Text + "\n");
            }
        }

        delegate void SetTextCallback(string text);
        private void Response(string text)
        {
            if (textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Response);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + "\n");
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = serialPort1.ReadLine();
            Response(str);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }
    }
}

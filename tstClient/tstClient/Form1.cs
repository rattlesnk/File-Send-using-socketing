using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace tstClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // IPEndPoint remoteEP = new IPEndPoint(textBox1.Text, int.Parse(textBox2.Text));
           // IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
          //  IPAddress ipAddress = ipHostInfo.AddressList[0];
            lbl_Status.Text = "Connecting....";

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            // Create a TCP/IP  socket.
            Socket receiver = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            receiver.Connect(remoteEP);
            lbl_Status.Text = "Connected!";
            //receiver.Connect(remoteEP);
        }
    }
}

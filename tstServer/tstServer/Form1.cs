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
using System.Threading;
using System.Net;
using System.IO;

namespace tstServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static bool HasStartup = false;





        // Data buffer for incoming data.
        byte[] buffer;
        byte[] fileNameByte;
        byte[] fileNameLenByte;
        byte[] fileLenByte;

        // FileStream to write data
        FileStream fileStream;
        Int64 fileLen = 0;
        int NoOfPackets = 0;
        int readBytes = 0;
        int i;
 

        private void btnSelectFile_Click_1(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbxFile.Text = openFileDialog.FileName;
                //AsynchronousSocketListener.FileToSend = tbxFile.Text;
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (!HasStartup) //gota change this flag in listner class when client connects
            {
                try
                {
                    listnerClass.Port = textBox1.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                    return;
                }
                listnerClass.Server = this;
                Thread listener = new Thread(new ThreadStart(listnerClass.StartListening));
                listener.IsBackground = true;
                listener.Start();
                HasStartup = true;
            }
        }

        public void AddClient(IPEndPoint IpEndPoint)
        {
            lbxServer.BeginUpdate();
            lbxServer.Items.Add(IpEndPoint.ToString());
            lbxServer.EndUpdate();
        }

    
    }
}

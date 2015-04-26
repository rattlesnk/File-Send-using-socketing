using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace tstServer
{
    public static class listnerClass
    {
        //Vars
        private static int port;

        //Members
        private delegate void AddClientHandler(IPEndPoint IpEndPoint);

        //Properties
        public static Form1 Server { get; set; }
        public static string Port
        {
            set
            {
                try
                {
                    port = Convert.ToInt32(value);
                }
                catch (FormatException)
                {
                    throw new Exception(Properties.Resources.InvalidPortMsg);
                }
                catch (OverflowException ex)
                {
                    throw new Exception(ex.Message);
                }

                if (port < 0 || port > 65535)
                {
                    throw new Exception(Properties.Resources.InvalidPortMsg);
                }
            }
        }

      //  public static IPAddress IP { get; set; }





        public static void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                // Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.
                Socket handler = listener.Accept();
                IPEndPoint ipEndPoint = handler.RemoteEndPoint as IPEndPoint;
                if ((ipEndPoint) != null)
                {
                    
                    Server.BeginInvoke(new AddClientHandler(Server.AddClient), ipEndPoint);
                }
                //lbxServer.Items.Add(ipEndPoint);
                //Int32 filesNumber = binFilesNames.Count;
                //byte[] filesNumberByte = BitConverter.GetBytes(filesNumber);
                //handler.Send(filesNumberByte);
                //// --
                //foreach (string binName in binFilesNames)
                //{
                //    fileNameByte = Encoding.UTF8.GetBytes(binName);
                //    fileNameLenByte = BitConverter.GetBytes(fileNameByte.Length);
                //    handler.Send(fileNameLenByte);
                //    handler.Send(fileNameByte);
                //    // --
                //    fileStream = new FileStream(sourceFilePath + binName, FileMode.Open, FileAccess.Read);
                //    fileLen = fileStream.Length;
                //    fileLenByte = BitConverter.GetBytes(fileLen);
                //    handler.Send(fileLenByte);
                //    // --
                //    NoOfPackets = Convert.ToInt32(Math.Ceiling(
                //        Convert.ToDouble(fileLen) / Convert.ToDouble(BufferSize)));
                //    for (i = 0; i < NoOfPackets; i++)
                //    {
                //        if (fileLen > BufferSize)
                //        {
                //            buffer = new byte[BufferSize];
                //            // reeding data from file and writing it to the bytes "buffer"
                //            readBytes = fileStream.Read(buffer, 0, BufferSize);
                //            // send bytes from "buffer"
                //            handler.Send(buffer, readBytes, SocketFlags.None);
                //            fileLen -= BufferSize;
                //        }
                //        else
                //        {
                //            buffer = new byte[fileLen];
                //            // reeding data from file and writing it to the bytes "buffer"
                //            readBytes = fileStream.Read(buffer, 0, (int)fileLen);
                //            // send bytes from "buffer"
                //            handler.Send(buffer, readBytes, SocketFlags.None);
                //        }
                //    }
                //    fileStream.Close();
                //}
                // Release the socket.
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

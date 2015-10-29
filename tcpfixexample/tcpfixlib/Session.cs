using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TcpFixLib
{
    public class Session : ISession
    {
        protected String m_IP = "localhost";
        protected Int16 m_Port = 0;
        protected String m_TargetCompId = "";
        protected String m_SenderCompID = "";
        protected String m_FixVersion = "4.4";
        protected int m_HeartBeat = 60;

        protected int m_In_Seq_Num = 0;
        protected int m_Out_Seq_Num = 0;
        protected String m_Last_Ran_EOD = "";

        protected byte[] m_Secret = {};

        protected TcpClient m_Client;

        public Session(string ipaddress, Int16 port, string sendercompid, string targetcompid, string fixversion, int heartbeat)
        {
            m_IP = ipaddress;
            m_Port = port;
            m_SenderCompID = sendercompid;
            m_TargetCompId = targetcompid;
            m_FixVersion = fixversion;
            m_HeartBeat = heartbeat;
        }

        public void go(Byte[] secret)
        {
            TcpClient client = new TcpClient(m_IP, m_Port);
            NetworkStream stream = client.GetStream();
            //get communication stats and fill yourself
        }

        public String IP
        {
            get
            {
                return m_IP;
            }
            set
            {
                m_IP = value;
            }
        }

        public Int16 Port{
            get
            {
                return m_Port;
            }
            set
            {
                m_Port = value;
            }
        }
        public String TargetCompId
        {
            get
            {
                return m_TargetCompId;
            }
            set
            {
                m_TargetCompId = value;
            }
        }
        public String SenderCompID
        {
            get
            {
                return m_SenderCompID;
            }
            set
            {
                m_SenderCompID = value;
            }
        }
        public String FixVersion
        {
            get
            {
                return m_FixVersion;
            }
            set
            {
                m_FixVersion = value;
            }
        }
        public int HeartBeat
        {
            get
            {
                return m_HeartBeat;
            }
            set
            {
                m_HeartBeat = value;
            }
        }
        public int In_Seq_Num
        {
            get
            {
                return m_In_Seq_Num;
            }
        }
        public int Out_Seq_Num
        {
            get
            {
                return m_Out_Seq_Num;
            }
        }

        public void start(String ip, Int16 port, String username, String password)
        {

        }

        public void connect()
        {
        }

        protected void ReadClient()
        {
            while (true)
            {
                byte[] command = new byte[1];
                NetworkStream stream = m_Client.GetStream();
                stream.Read(command, 0, command.Length);
                switch (command[0])
                {
                    case (byte)'o':
                        Console.Write("Incoming event:\n");
                        ReadEvent(stream);
                        break;
                    case (byte)'r':
                        Console.Write("Incoming result:\n");
                        ReadResult(stream);
                        break;
                    case (byte)'e':
                        Console.Write("Incoming error:\n");
                        ReadError(stream);
                        break;
                    case (byte)'n':
                        Console.Write("Incoming notification:\n");
                        ReadNotification(stream);
                        break;
                }
                /*
                string response = ByteArrayHandler.readString(stream);
                TagValue[] parameter;
                switch (response)
                {
                    case "connectionClosed":
                        parameter = readTagValues();
                        break;
                    case "connectionEstablished":
                        parameter = readTagValues();
                        break;
                    case "acceptFixMsg":
                        parameter = readTagValues();
                        break;
                    default:
                        parameter = new TagValue[0];
                        break;
                }
                Console.Write(response);
                foreach (TagValue tv in parameter)
                {
                    tv.print();
                }
                Console.Write("\n");
                 */
            }
        }

        protected void ReadEvent(NetworkStream stream)
        {
        }

        protected void ReadResult(NetworkStream stream)
        {
        }

        protected void ReadError(NetworkStream stream)
        {
        }

        protected void ReadNotification(NetworkStream stream)
        {
        }

        public event AcceptFixMessageEventHandler AcceptFixMessage;
    }
}

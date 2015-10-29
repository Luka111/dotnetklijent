using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace TcpFixLib
{
    class SecretObtainer 
    {
        protected Session m_Session;
        protected Byte[] m_Descriptor;

        public SecretObtainer(Session session, Byte[] descriptor)
        {
            m_Session = session;
            m_Descriptor = descriptor;
            new Thread(new ThreadStart(getSecret)).Start();
        }
        private void getSecret()
        {
            TcpClient client = new TcpClient(m_Client.IP, m_Client.Port);
            NetworkStream stream = client.GetStream();
            stream.Write(m_Descriptor, 0, m_Descriptor.Length);
            Byte[] secret = new Byte[16];
            stream.Read(secret, 0, Secret.Length);
            m_Session.go(secret);
        }
    }


    public class Client
    {
        //protected List<ISession> m_Sessions = new List<ISession>();
        protected String m_IP;
        protected Int16 m_Port;
        protected String m_Username;
        protected String m_Password;
        protected Dictionary<Byte[], Session> m_Sessions = new Dictionary<Byte[],Session>();

        public String IP
        {
            get
            {
                return m_IP;
            }
        }

        public Int16 Port
        {
            get
            {
                return m_Port;
            }
        }


        public Client(String ip, Int16 port, String username, String password)
        {
            m_IP = ip;
            m_Port = port;
            m_Username = username;
            m_Password = password;
        }

        public Session Session(string ipaddress, Int16 port, string sendercompid, string targetcompid, string fixversion, int heartbeat)
        {
            Byte[] sessiondesc = SessionDesc(ipaddress, port, sendercompid, targetcompid, fixversion, heartbeat);
            Session s;
            if (m_Sessions.TryGetValue(sessiondesc, out s))
            {
                return s;
            }
            else
            {
                s = new Session(ipaddress, port, sendercompid, targetcompid, fixversion, heartbeat);
                m_Sessions.Add(sessiondesc, s);
                //start talking to the server in other thread and
                new SecretObtainer(this, sessiondesc);

                return s;
            }
        }

        protected Byte[] SessionDesc(string ipaddress, Int16 port, string sendercompid, string targetcompid, string fixversion, int heartbeat)
        {
            Byte[] usernamebytes = ByteArrayHandler.asciiBytes(m_Username);
            Byte[] passwordbytes = ByteArrayHandler.asciiBytes(m_Password);
            Byte[] ipaddressbytes = ByteArrayHandler.asciiBytes(ipaddress);
            Byte[] portbytes = ByteArrayHandler.asciiBytes(port);
            Byte[] sendercompidbytes = ByteArrayHandler.asciiBytes(sendercompid);
            Byte[] targetcompidbytes = ByteArrayHandler.asciiBytes(targetcompid);
            Byte[] fixversionbytes = ByteArrayHandler.asciiBytes(fixversion);
            Byte[] heartbeatbytes = ByteArrayHandler.asciiBytes(heartbeat);
            Byte[][] bytess = { usernamebytes, passwordbytes, ipaddressbytes, portbytes, sendercompidbytes, targetcompidbytes, fixversionbytes, heartbeatbytes };
            return ByteArrayHandler.concatBytes(bytess);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TcpFixLib
{
    class StreamReader
    {
        public static String ReadString (TcpClient client) {
            String response = "";
            Byte[] inbyte = new byte[1];
            NetworkStream stream = client.GetStream();
            while (true)
            {
                stream.Read(inbyte, 0, inbyte.Length);
                if (inbyte[0] != 0)
                {
                    response = String.Concat(response, Encoding.ASCII.GetString(inbyte));
                }
                else
                {
                    break;
                }
            }
            return response;
        }

        public static int ReadInt(TcpClient client)
        {
            String intstring = ReadString(client);
            if (intstring.Length < 1)
            {
                return 0;
            }
            return int.Parse(intstring);
        }

        public static FixMessage readTagValues(TcpClient client)
        {
            FixMessage ret = new FixMessage();
            int tag;
            string value;
            while (true)
            {
                tag = ReadInt(client);
                if (tag < 1)
                {
                    break;
                }
                value = ReadString(client);
                ret.Add(new TagValue(tag, value));
            }
            return ret;
        }
    }
}

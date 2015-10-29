using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TcpFixLib
{
    struct ByteArrayHandler
    {
        private static byte[] ZeroByte = { 0 };
        static public Byte[] concatBytes(byte[][] bytes)
        {
            byte[] ret = new byte[bytes.Sum(a => a.Length)+bytes.Length-1];
            int offset = 0;
            byte [] last = bytes.Last();
            foreach (byte[] _bytes in bytes)
            {
                Buffer.BlockCopy(_bytes, 0, ret, offset, _bytes.Length);
                offset += _bytes.Length;
                if (_bytes != last)
                {
                    Buffer.BlockCopy(ZeroByte, 0, ret, offset, ZeroByte.Length);
                    offset += ZeroByte.Length;
                }
            }
            return ret;
        }
        static public Byte[] asciiBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        static public Byte[] asciiBytes(Int16 shrt)
        {
            return asciiBytes(shrt.ToString());
        }
        static public Byte[] asciiBytes(int intnum)
        {
            return asciiBytes(intnum.ToString());
        }

        static public string readString(NetworkStream stream)
        {
            String response = "";
            Byte[] inbyte = new byte[1];
            while (true)
            {
                stream.Read(inbyte, 0, inbyte.Length);
                if (inbyte[0] != 0)
                {
                    response = String.Concat(response, System.Text.Encoding.ASCII.GetString(inbyte));
                }
                else
                {
                    break;
                }
            }
            return response;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TcpFixLib;

namespace tcpfixexample
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client("blabla.com", 8080, "inbla", "123");
            Session s = c.Session("127.0.0.1", 1456, "nodebla", "electrobla", "4.4", 30);
        }
    }
}

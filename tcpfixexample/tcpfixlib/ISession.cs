using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpFixLib
{
    public delegate void AcceptFixMessageEventHandler(FixMessage msg);

    public interface ISession : IRemote
    {
        String TargetCompId
        {
            get;
            set;
        }
        String SenderCompID
        {
            get;
            set;
        }
        String FixVersion
        {
            get;
            set;
        }
        int HeartBeat
        {
            get;
            set;
        }
        int In_Seq_Num
        {
            get;
        }
        int Out_Seq_Num
        {
            get;
        }
        void start(String ip, Int16 port, String username, String password);
        void connect();

        event AcceptFixMessageEventHandler AcceptFixMessage;

    }
}

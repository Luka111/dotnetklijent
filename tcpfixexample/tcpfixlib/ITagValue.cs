using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpFixLib
{
    public interface ITagValue
    {
        int Tag
        {
            get;
            set;
        }
        String Value
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpFixLib
{
    class TagValue : ITagValue
    {
        protected int m_Tag = 0;
        protected String m_Value = "";

        public TagValue(int tag, String value)
        {
            m_Tag = tag;
            m_Value = value;
        }

        public int Tag
        {
            get
            {
                return m_Tag;
            }
            set
            {
                m_Tag = value;
            }
        }

        public String Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }
    }
}

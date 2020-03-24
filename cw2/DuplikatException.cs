using System;
using System.Collections.Generic;
using System.Text;

namespace cw2
{
    public class DuplikatException : Exception
    {
        public DuplikatException() : base()
        {

        }

        public DuplikatException(string message) : base(message)
        {

        }
    }
}

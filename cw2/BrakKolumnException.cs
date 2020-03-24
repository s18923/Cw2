using System;
using System.Collections.Generic;
using System.Text;

namespace cw2
{
    public class BrakKolumnException : Exception
    {
        public BrakKolumnException() : base()
        {

        }

        public BrakKolumnException(string message) : base(message)
        {

        }
    }
}

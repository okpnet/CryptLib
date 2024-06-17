using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptLib
{
    public class CryptLibException : Exception
    {
        public CryptLibException()
        {
        }
        public CryptLibException(string message):base(message)
        {
        }
        public CryptLibException(string message,Exception innerexception) : base(message,innerexception)
        {
        }
    }
}

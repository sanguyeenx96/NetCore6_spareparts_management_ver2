using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Exceptions
{
    public class SparepartsException : Exception
    {
        public SparepartsException()
        {

        }
        public SparepartsException(string message) : base(message)
        {

        }

    }  
}

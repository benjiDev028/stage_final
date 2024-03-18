using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBusinessLogic.Exceptions
{
    public class ImpossibleException : Exception
    {
        public ImpossibleException(string message) : base(message) { }
    }
    
}

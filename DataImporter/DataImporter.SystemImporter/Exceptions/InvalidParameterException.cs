using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string message)
            : base(message) { }
    }
}

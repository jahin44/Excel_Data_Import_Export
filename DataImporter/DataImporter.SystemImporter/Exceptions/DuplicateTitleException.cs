using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Exceptions
{
    public class DuplicateTitleException : Exception
    {
        public DuplicateTitleException(string message)
            : base(message) { }
    }
}

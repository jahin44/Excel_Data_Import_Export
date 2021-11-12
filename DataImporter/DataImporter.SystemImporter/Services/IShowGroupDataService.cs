using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    public interface IShowGroupDataService
    {
      
        (List<string> Header, List<string> Data) GroupData(int id);
       
    }
}

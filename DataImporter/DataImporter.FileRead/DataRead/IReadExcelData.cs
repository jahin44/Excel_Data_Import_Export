using DataImporter.SystemImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.FileRead.DataRead
{
    public interface IReadExcelData
    {
        void ReadData(FileStatus fileStatus);
    }
}

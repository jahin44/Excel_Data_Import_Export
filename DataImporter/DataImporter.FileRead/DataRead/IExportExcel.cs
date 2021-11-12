using DataImporter.SystemImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.FileRead.DataRead
{
    public interface IExportExcel
    {
        public DataTable ExportTable(int Id, DataTable dataTable);
    }
}

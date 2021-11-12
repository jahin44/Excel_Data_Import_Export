using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Entities
{
    public class ExcelFieldData : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int ExcelDataId { get; set; }
        public ExcelData ExcelData { get; set; }

    }
}

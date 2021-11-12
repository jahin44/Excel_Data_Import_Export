using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Entities
{
    public class ExcelData : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public List<ExcelFieldData> ExcelFieldDatas { get; set; }
    }
}

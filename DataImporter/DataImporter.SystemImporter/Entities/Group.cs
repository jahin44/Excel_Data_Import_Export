using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<ExcelData> ExcelDatas { get; set; }
        public List<FileStatus> FileStatuses { get; set; }
        public List<ExportHistory> ExportHistories { get; set; }



    }
}

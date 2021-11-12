using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.BusinessObjects
{
    public class ExportHistory
    {
        public int Id { get; set; }
        public DateTime ExportDate { get; set; }
        public string Email { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
    }
}

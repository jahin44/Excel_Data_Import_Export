using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.BusinessObjects
{
    public class FileStatus
    {
        public int Id { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public DateTime UplodeTime { get; set; }
        public string Status { get; set; }
    }
}

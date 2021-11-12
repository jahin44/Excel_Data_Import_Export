using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.FileRead.DataRead
{
    public interface IPreviewData
    {
        public string TableExcelData(IFormFile file, string newPath);

    }
}

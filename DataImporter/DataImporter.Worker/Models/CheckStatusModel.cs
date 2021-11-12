using DataImporter.Common.Utilities;
using DataImporter.FileRead.DataRead;
using DataImporter.SystemImporter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker.Models
{
    public class CheckStatusModel
    {
        private IFileStatusService _fileStatusService;
        private IReadExcelData _readExcelData;
        public CheckStatusModel(IFileStatusService fileStatusService, IReadExcelData readExcelData)
        {
            _fileStatusService = fileStatusService;
            _readExcelData = readExcelData;
        }

        public void CheckCall()
        {
            var FileStatusFUllTable = _fileStatusService.GetAllFileStatuss();
         
            foreach (var eachFile in FileStatusFUllTable)
            {
                if (eachFile.Status == "processing")
                {
                    _readExcelData.ReadData(eachFile);
                }

            }
        }         
    }
}

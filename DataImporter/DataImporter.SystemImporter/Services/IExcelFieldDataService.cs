using DataImporter.SystemImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    public interface IExcelFieldDataService
    {
        IList<ExcelFieldData> GetAllExcelFieldDatas();
        void CreateExcelFieldData(ExcelFieldData excelData);
        ExcelFieldData GetExcelFieldData(int id);
        void UpdateExcelFieldData(ExcelFieldData excelData);
        void DeleteExcelFieldData(int id);
    }
}

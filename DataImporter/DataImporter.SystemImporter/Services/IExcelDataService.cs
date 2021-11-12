using DataImporter.SystemImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    public interface IExcelDataService
    {
        IList<ExcelData> GetAllExcelDatas();
        void CreateExcelData(ExcelData excelData);
        //(IList<ExcelData> records, int total, int totalDisplay) GetExcelDatas(int pageIndex, int pageSize,
        //    string searchText, string sortText);
        ExcelData GetExcelData(int id);
        void UpdateExcelData(ExcelData excelData);
        void DeleteExcelData(int id);
    }
}

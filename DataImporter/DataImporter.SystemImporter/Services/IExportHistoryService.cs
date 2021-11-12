using DataImporter.SystemImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    public interface IExportHistoryService
    {
        IList<ExportHistory> GetAllExportHistorys();
        void CreateExportHistory(ExportHistory exportHistory);
        (IList<ExportHistory> records, int total, int totalDisplay) GetExportHistorys(int pageIndex, int pageSize,
            string searchText, string sortText);
        ExportHistory GetExportHistory(int id);
        void UpdateExportHistory(ExportHistory exportHistory);
        void DeleteExportHistory(int id);
    }
}

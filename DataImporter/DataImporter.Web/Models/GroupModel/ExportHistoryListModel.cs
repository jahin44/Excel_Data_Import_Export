using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.GroupModel
{
    public class ExportHistoryListModel
    {
        private IExportHistoryService _exportHistoryService;
        private IHttpContextAccessor _httpContextAccessor;

        public ExportHistoryListModel()
        {
            _exportHistoryService = Startup.AutofacContainer.Resolve<IExportHistoryService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }

        public ExportHistoryListModel(IExportHistoryService exportHistoryService, IHttpContextAccessor httpContextAccessor)
        {
            _exportHistoryService = exportHistoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal object GetExportHistorys(DataTablesAjaxRequestModel tableModel)
        {
            var data = _exportHistoryService.GetExportHistorys(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "GroupName", "ExportData", "Email" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName,
                                record.ExportDate.ToString(),
                                record.Email,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _exportHistoryService.DeleteExportHistory(id);
        }
    }
}

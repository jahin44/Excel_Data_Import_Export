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
    public class ContactsListModel
    {
        private IFileStatusService _fileStatusService;
        private IHttpContextAccessor _httpContextAccessor;

        public ContactsListModel()
        {
            _fileStatusService = Startup.AutofacContainer.Resolve<IFileStatusService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }

        public ContactsListModel(IFileStatusService fileStatusService, IHttpContextAccessor httpContextAccessor)
        {
            _fileStatusService = fileStatusService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal object GetFileStatuss(DataTablesAjaxRequestModel tableModel)
        {
            var data = _fileStatusService.GetFileStatuss(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "GroupName", "FileName", "UplodeTime", "Status" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName,
                                record.FileName,
                                record.UplodeTime.ToString(),
                                record.Status,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _fileStatusService.DeleteFileStatus(id);
        }
    }
}

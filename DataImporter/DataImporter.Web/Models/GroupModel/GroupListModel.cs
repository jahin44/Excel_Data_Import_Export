using Autofac;
using DataImporter.SystemImporter.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataImporter.Web;
using DataImporter.Common.Utilities;

namespace DataImporter.Web.Models.GroupModel
{
    public class GroupListModel
    {
        private IGroupService _groupService;
        private IHttpContextAccessor _httpContextAccessor;

        public GroupListModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }

        public GroupListModel(IGroupService groupService, IHttpContextAccessor httpContextAccessor)
        {
            _groupService = groupService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal object GetGroups(DataTablesAjaxRequestModel tableModel)
        {
            var data = _groupService.GetGroups(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "GroupName", "CreateDate" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName,
                                record.CreateDate.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _groupService.DeleteGroup(id);
        }
    }
}

using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.GroupModel
{
    public class ShowGroupTableModel
    {
        public int Id { get; set; }
 
        public string GroupName { get; set; }
        public List<string> HeaderName { get; set; }
        public List<string> TableData { get; set; }


        private readonly IGroupService _groupService;
        private readonly IShowGroupDataService _showGroupDataService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTimeUtility;


        public ShowGroupTableModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _showGroupDataService = Startup.AutofacContainer.Resolve<IShowGroupDataService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDateTimeUtility>();

        }

        public void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);
            Id = group.Id;
            GroupName = group?.GroupName;
            var data = _showGroupDataService.GroupData(id);
            HeaderName = data.Header;
            TableData = data.Data;
          
        }

         
    }
}

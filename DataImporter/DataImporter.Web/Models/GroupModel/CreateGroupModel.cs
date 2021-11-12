using Autofac;
using AutoMapper;
using DataImporter.SystemImporter.Services;
using DataImporter.SystemImporter.BusinessObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataImporter.Common.Utilities;

namespace DataImporter.Web.Models.GroupModel
{
    public class CreateGroupModel
    {
        [Required, MaxLength(200, ErrorMessage = "Group Name should be less than 200 charcaters")]
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }

        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTimeUtility;


        public CreateGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDateTimeUtility>();
        }

        public CreateGroupModel(IGroupService groupService)
        {
            _groupService = groupService;
        }

        internal void CreateGroup()
        {
            var group = _mapper.Map<Group>(this);
            group.CreateDate = _dateTimeUtility.Now;

            _groupService.CreateGroup(group);
        }

    }
}

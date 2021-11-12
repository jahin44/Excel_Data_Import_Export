using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.GroupModel
{
    public class EditGroupModel
    {
        public int Id { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Group Name should be less than 200 charcaters")]
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }

        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTimeUtility;


        public EditGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDateTimeUtility>();

        }
        public EditGroupModel(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        
        }

            public void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);
            _mapper.Map(group, this);
        }

        internal void Update()
        {
            var group = _mapper.Map<Group>(this);
            group.CreateDate = _dateTimeUtility.Now;
            _groupService.UpdateGroup(group);
        }

    }
}

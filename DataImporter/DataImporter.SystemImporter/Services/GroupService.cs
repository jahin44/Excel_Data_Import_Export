using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Exceptions;
using DataImporter.SystemImporter.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    public class GroupService : IGroupService
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;

        public GroupService(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public IList<Group> GetAllGroups()
        {
            var groupEntities = _systemImporterUnitOfWork.Groups.GetAll();
            var groups = new List<Group>();

            foreach (var entity in groupEntities)
            {
                var group = _mapper.Map<Group>(entity);
                groups.Add(group);
            }

            return groups;
        }

        public void CreateGroup(Group group)
        {
            if (group == null)
                throw new InvalidParameterException("Group was not provided");

            if (IsTitleAlreadyUsed(group.GroupName))
                throw new DuplicateTitleException("Group title already exists");

            _systemImporterUnitOfWork.Groups.Add(
                _mapper.Map<Entities.Group>(group)
            );

            _systemImporterUnitOfWork.Save();
        }


        private bool IsTitleAlreadyUsed(string groupName) =>
            _systemImporterUnitOfWork.Groups.GetCount(x => x.GroupName == groupName) > 0;

        private bool IsTitleAlreadyUsed(string groupName, int id) =>
            _systemImporterUnitOfWork.Groups.GetCount(x => x.GroupName == groupName && x.Id != id) > 0;

         
        public (IList<Group> records, int total, int totalDisplay) GetGroups(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var groupData = _systemImporterUnitOfWork.Groups.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.GroupName.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from groups in groupData.data
                              select _mapper.Map<Group>(groups)).ToList();

            return (resultData, groupData.total, groupData.totalDisplay);
        }

        public Group GetGroup(int id)
        {
            var group = _systemImporterUnitOfWork.Groups.GetById(id);

            if (group == null) return null;

            return _mapper.Map<Group>(group);
        }

        public void UpdateGroup(Group group)
        {
            if (group == null)
                throw new InvalidParameterException("Group was not provided");

            if (IsTitleAlreadyUsed(group.GroupName))
                throw new DuplicateTitleException("Group title already exists");

            var groupEntity = _systemImporterUnitOfWork.Groups.GetById(group.Id);

            if (groupEntity != null)
            {
                _mapper.Map(group, groupEntity);
                _systemImporterUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find group");
        }

        public void DeleteGroup(int id)
        {
            _systemImporterUnitOfWork.Groups.Remove(id);
            _systemImporterUnitOfWork.Save();
        }
    }
}

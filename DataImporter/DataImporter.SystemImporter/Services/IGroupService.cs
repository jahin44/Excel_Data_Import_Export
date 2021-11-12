using DataImporter.SystemImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    public interface IGroupService
    {
        IList<Group> GetAllGroups();
        void CreateGroup(Group group);
        (IList<Group>records, int total, int totalDisplay) GetGroups(int pageIndex, int pageSize, 
            string searchText, string sortText);
        Group GetGroup(int id);
        void UpdateGroup(Group group);
        void DeleteGroup(int id);
    }
}

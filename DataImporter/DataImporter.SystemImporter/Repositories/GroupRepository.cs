using DataImporter.Data;
using DataImporter.SystemImporter.Contexts;
using DataImporter.SystemImporter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Repositories
{
    public class GroupRepository : Repository<Group, int>,
        IGroupRepository
    {
        public GroupRepository(ISystemImporterDbContext context)
            : base((DbContext)context)
        {
        }
    }
}

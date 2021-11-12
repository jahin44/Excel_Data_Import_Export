using DataImporter.Data;
using DataImporter.SystemImporter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Repositories
{
    public interface IFileStatusRepository : IRepository<FileStatus, int>
    {
    }
}

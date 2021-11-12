using DataImporter.SystemImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    public interface IFileStatusService
    {
        IList<FileStatus> GetAllFileStatuss();
        void CreateFileStatus(FileStatus fileStatus);
        (IList<FileStatus> records, int total, int totalDisplay) GetFileStatuss(int pageIndex, int pageSize,
            string searchText, string sortText);
        FileStatus GetFileStatus(int id);
        void UpdateFileStatus(FileStatus fileStatus);
        void DeleteFileStatus(int id);
    }
}

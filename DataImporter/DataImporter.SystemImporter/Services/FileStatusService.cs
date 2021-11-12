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
    public class FileStatusService : IFileStatusService
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;

        public FileStatusService(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public IList<FileStatus> GetAllFileStatuss()
        {
            var fileStatusEntities = _systemImporterUnitOfWork.FileStatuss.GetAll();
            var fileStatuss = new List<FileStatus>();

            foreach (var entity in fileStatusEntities)
            {
                var fileStatus = _mapper.Map<FileStatus>(entity);
                fileStatuss.Add(fileStatus);
            }

            return fileStatuss;
        }

        public void CreateFileStatus(FileStatus fileStatus)
        {
            if (fileStatus == null)
                throw new InvalidParameterException("Fild was not provided");

            if (IsTitleAlreadyUsed(fileStatus.FileName))
                throw new DuplicateTitleException("This File already exists");

            _systemImporterUnitOfWork.FileStatuss.Add(
                _mapper.Map<Entities.FileStatus>(fileStatus)
            );

            _systemImporterUnitOfWork.Save();
        }


        private bool IsTitleAlreadyUsed(string fileName) =>
            _systemImporterUnitOfWork.FileStatuss.GetCount(x => x.FileName == fileName) > 0;

        private bool IsTitleAlreadyUsed(string fileName, int id) =>
            _systemImporterUnitOfWork.FileStatuss.GetCount(x => x.FileName == fileName && x.Id != id) > 0;


        public (IList<FileStatus> records, int total, int totalDisplay) GetFileStatuss(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var fileStatusData = _systemImporterUnitOfWork.FileStatuss.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.GroupName.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from fileStatuss in fileStatusData.data
                              select _mapper.Map<FileStatus>(fileStatuss)).ToList();

            return (resultData, fileStatusData.total, fileStatusData.totalDisplay);
        }

        public FileStatus GetFileStatus(int id)
        {
            var fileStatus = _systemImporterUnitOfWork.FileStatuss.GetById(id);

            if (fileStatus == null) return null;

            return _mapper.Map<FileStatus>(fileStatus);
        }

        public void UpdateFileStatus(FileStatus fileStatus)
        {
            if (fileStatus == null)
                throw new InvalidOperationException("FileStatus is missing");

            var fileStatusEntity = _systemImporterUnitOfWork.FileStatuss.GetById(fileStatus.Id);

            if (fileStatusEntity != null)
            {
                _mapper.Map(fileStatus, fileStatusEntity);
                _systemImporterUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find fileStatus");
        }

        public void DeleteFileStatus(int id)
        {
            _systemImporterUnitOfWork.FileStatuss.Remove(id);
            _systemImporterUnitOfWork.Save();
        }

    }
}

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
    public class ExportHistoryService : IExportHistoryService
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;

        public ExportHistoryService(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public IList<ExportHistory> GetAllExportHistorys()
        {
            var exportHistoryEntities = _systemImporterUnitOfWork.ExportHistorys.GetAll();
            var exportHistorys = new List<ExportHistory>();

            foreach (var entity in exportHistoryEntities)
            {
                var exportHistory = _mapper.Map<ExportHistory>(entity);
                exportHistorys.Add(exportHistory);
            }

            return exportHistorys;
        }

        public void CreateExportHistory(ExportHistory exportHistory)
        {
            if (exportHistory == null)
                throw new InvalidParameterException("Fild was not provided");

            _systemImporterUnitOfWork.ExportHistorys.Add(
                _mapper.Map<Entities.ExportHistory>(exportHistory)
            );

            _systemImporterUnitOfWork.Save();
        }


        //private bool IsTitleAlreadyUsed(string fileName) =>
        //    _systemImporterUnitOfWork.ExportHistorys.GetCount(x => x.FileName == fileName) > 0;

        //private bool IsTitleAlreadyUsed(string fileName, int id) =>
        //    _systemImporterUnitOfWork.ExportHistorys.GetCount(x => x.FileName == fileName && x.Id != id) > 0;


        public (IList<ExportHistory> records, int total, int totalDisplay) GetExportHistorys(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var exportHistoryData = _systemImporterUnitOfWork.ExportHistorys.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.GroupName.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from exportHistorys in exportHistoryData.data
                              select _mapper.Map<ExportHistory>(exportHistorys)).ToList();

            return (resultData, exportHistoryData.total, exportHistoryData.totalDisplay);
        }

        public ExportHistory GetExportHistory(int id)
        {
            var exportHistory = _systemImporterUnitOfWork.ExportHistorys.GetById(id);

            if (exportHistory == null) return null;

            return _mapper.Map<ExportHistory>(exportHistory);
        }

        public void UpdateExportHistory(ExportHistory exportHistory)
        {
            if (exportHistory == null)
                throw new InvalidOperationException("ExportHistory is missing");

            var exportHistoryEntity = _systemImporterUnitOfWork.ExportHistorys.GetById(exportHistory.Id);

            if (exportHistoryEntity != null)
            {
                _mapper.Map(exportHistory, exportHistoryEntity);
                _systemImporterUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find exportHistory");
        }

        public void DeleteExportHistory(int id)
        {
            _systemImporterUnitOfWork.ExportHistorys.Remove(id);
            _systemImporterUnitOfWork.Save();
        }

    }
}

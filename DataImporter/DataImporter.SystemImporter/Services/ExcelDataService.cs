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
    public class ExcelDataService : IExcelDataService
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;

        public ExcelDataService(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public IList<ExcelData> GetAllExcelDatas()
        {
            var excelDataEntities = _systemImporterUnitOfWork.ExcelDatas.GetAll();
            var excelDatas = new List<ExcelData>();

            foreach (var entity in excelDataEntities)
            {
                var excelData = _mapper.Map<ExcelData>(entity);
                excelDatas.Add(excelData);
            }

            return excelDatas;
        }

        public void CreateExcelData(ExcelData excelData)
        {
            if (excelData == null)
                throw new InvalidParameterException("ExcelData was not provided");

            _systemImporterUnitOfWork.ExcelDatas.Add(
                _mapper.Map<Entities.ExcelData>(excelData)
            );

            _systemImporterUnitOfWork.Save();
        }

        public ExcelData GetExcelData(int id)
        {
            var excelData = _systemImporterUnitOfWork.ExcelDatas.GetById(id);

            if (excelData == null) return null;

            return _mapper.Map<ExcelData>(excelData);
        }

        public void UpdateExcelData(ExcelData excelData)
        {
            if (excelData == null)
                throw new InvalidOperationException("ExcelData is missing");        

            var excelDataEntity = _systemImporterUnitOfWork.ExcelDatas.GetById(excelData.Id);

            if (excelDataEntity != null)
            {
                _mapper.Map(excelData, excelDataEntity);
                _systemImporterUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find excelData");
        }

        public void DeleteExcelData(int id)
        {
            _systemImporterUnitOfWork.ExcelDatas.Remove(id);
            _systemImporterUnitOfWork.Save();
        }
    }
}

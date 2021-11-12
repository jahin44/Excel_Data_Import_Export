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
    public class ExcelFieldDataService : IExcelFieldDataService
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;

        public ExcelFieldDataService(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public IList<ExcelFieldData> GetAllExcelFieldDatas()
        {
            var excelFieldDataEntities = _systemImporterUnitOfWork.ExcelFieldDatas.GetAll();
            var excelFieldDatas = new List<ExcelFieldData>();

            foreach (var entity in excelFieldDataEntities)
            {
                var excelFieldData = _mapper.Map<ExcelFieldData>(entity);
                excelFieldDatas.Add(excelFieldData);
            }

            return excelFieldDatas;
        }

        public void CreateExcelFieldData(ExcelFieldData excelFieldData)
        {
            if (excelFieldData == null)
                throw new InvalidParameterException("ExcelFieldData was not provided");

            _systemImporterUnitOfWork.ExcelFieldDatas.Add(
                _mapper.Map<Entities.ExcelFieldData>(excelFieldData)
            );

            _systemImporterUnitOfWork.Save();
        }



        //public (IList<ExcelFieldData> records, int total, int totalDisplay) GetExcelFieldDatas(int pageIndex, int pageSize,
        //    string searchText, string sortText)
        //{
        //    var excelFieldDataData = _systemImporterUnitOfWork.ExcelFieldDatas.GetDynamic(
        //        string.IsNullOrWhiteSpace(searchText) ? null : x => x.ExcelFieldDataName.Contains(searchText),
        //        sortText, string.Empty, pageIndex, pageSize);

        //    var resultData = (from excelFieldDatas in excelFieldDataData.data
        //                      select _mapper.Map<ExcelFieldData>(excelFieldDatas)).ToList();

        //    return (resultData, excelFieldDataData.total, excelFieldDataData.totalDisplay);
        //}

        public ExcelFieldData GetExcelFieldData(int id)
        {
            var excelFieldData = _systemImporterUnitOfWork.ExcelFieldDatas.GetById(id);

            if (excelFieldData == null) return null;

            return _mapper.Map<ExcelFieldData>(excelFieldData);
        }

        public void UpdateExcelFieldData(ExcelFieldData excelFieldData)
        {
            if (excelFieldData == null)
                throw new InvalidOperationException("ExcelFieldData is missing");

            var excelFieldDataEntity = _systemImporterUnitOfWork.ExcelFieldDatas.GetById(excelFieldData.Id);

            if (excelFieldDataEntity != null)
            {
                _mapper.Map(excelFieldData, excelFieldDataEntity);
                _systemImporterUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find excelFieldData");
        }

        public void DeleteExcelFieldData(int id)
        {
            _systemImporterUnitOfWork.ExcelFieldDatas.Remove(id);
            _systemImporterUnitOfWork.Save();
        }
    }
}

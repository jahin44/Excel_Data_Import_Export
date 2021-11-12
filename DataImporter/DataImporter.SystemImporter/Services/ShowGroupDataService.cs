using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Services
{
    class ShowGroupDataService : IShowGroupDataService
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;
        private readonly IExcelDataService _excelDataService;
        private readonly IExcelFieldDataService _excelFieldDataService;

        public ShowGroupDataService(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IDateTimeUtility dateTimeUtility,
            IExcelDataService excelDataService,
            IExcelFieldDataService excelFieldDataService,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _excelDataService = excelDataService;
            _excelFieldDataService = excelFieldDataService;
            _mapper = mapper;
        }

        public ( List<string> Header, List<string> Data) GroupData(int id)
        {
            var AllExcelData = _excelDataService.GetAllExcelDatas();
            var AllExcelFieldDatas = _excelFieldDataService.GetAllExcelFieldDatas();
            var ExcelDatasWithGroupId = new List<ExcelData>();
            var ExcelFieldDatasWithExcelDataID = new List<ExcelFieldData>();
            var Header = new List<string>();
            var Value = new List<string>();

            foreach (var Row in AllExcelData)
            {
                if (Row.GroupId == id)
                {
                    ExcelDatasWithGroupId.Add(Row);
                }
            }

            var ExcelDataFirstValue = ExcelDatasWithGroupId.First();

            foreach (var Data in AllExcelFieldDatas)
            {
                if (Data.ExcelDataId == ExcelDataFirstValue.Id)
                {
                    Header.Add(Data.Name);
                }
            }
            
            foreach (var Row in ExcelDatasWithGroupId)
            {
                    foreach (var Data in AllExcelFieldDatas)
                    {
                        if (Row.Id == Data.ExcelDataId)
                        {
                            Value.Add(Data.Value);
                        }
                    }
                
            }
            


            return ( Header , Value);
        }
    }
}

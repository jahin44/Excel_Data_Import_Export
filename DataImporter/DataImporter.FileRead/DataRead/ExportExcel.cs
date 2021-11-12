using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.Services;
using DataImporter.SystemImporter.UnitOfWorks;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Table;
using DataImporter.SystemImporter.BusinessObjects;
using System.Data;

namespace DataImporter.FileRead.DataRead
{
    public class ExportExcel : IExportExcel
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;
        private readonly IFileStatusService _fileStatusService;
        private readonly IExcelFieldDataService _excelFieldDataService;
        private readonly IExcelDataService _excelDataService;
        private readonly IGroupService _groupService;


        public ExportExcel(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IFileStatusService fileStatusService,
            IExcelDataService excelDataService,
            IGroupService groupService,
            IExcelFieldDataService excelFieldDataService,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _fileStatusService = fileStatusService;
            _excelDataService = excelDataService;
            _groupService = groupService;
            _excelFieldDataService = excelFieldDataService;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public DataTable ExportTable(int Id, DataTable dataTable)
        {
            var group = _groupService.GetGroup(Id);
            var ALLExcelData = _excelDataService.GetAllExcelDatas();
            var AllExcelFieldDatas = _excelFieldDataService.GetAllExcelFieldDatas();
            var ExcelDatasWithGroupId = new List<ExcelData>();
            var ExcelFieldDatasWithExcelDataID = new List<ExcelFieldData>();

            foreach (var entity in ALLExcelData)
            {
                if (entity.GroupId == Id)
                {
                    ExcelDatasWithGroupId.Add(entity);                                    
                }
            }

            var ExcelDataFirstValue = ExcelDatasWithGroupId.First();
            foreach (var excelFieldData in AllExcelFieldDatas)
            {
                if (excelFieldData.ExcelDataId == ExcelDataFirstValue.Id)
                {
                    ExcelFieldDatasWithExcelDataID.Add(excelFieldData);                 
                }
            }
            
            foreach (var excelFieldDataWithExcelDataId in ExcelFieldDatasWithExcelDataID)
            {
                dataTable.Columns.Add(excelFieldDataWithExcelDataId.Name, typeof(string));      
            }

            foreach (var eachRow in ExcelDatasWithGroupId)
            {
                int i = 0;
                DataRow row = dataTable.NewRow();
                foreach (var excelFieldDatas in AllExcelFieldDatas)
                {
                    if (eachRow.Id == excelFieldDatas.ExcelDataId)
                    {

                        row[i] = excelFieldDatas.Value;
                        i++;

                    }
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}

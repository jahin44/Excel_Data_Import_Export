using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Services;
using DataImporter.SystemImporter.UnitOfWorks;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.FileRead.DataRead
{
    public class CheckGroupColumn : ICheckGroupColumn
    {

        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;
        private readonly IFileStatusService _fileStatusService;
        private readonly IExcelFieldDataService _excelFieldDataService;
        private readonly IExcelDataService _excelDataService;
        private readonly IGroupService _groupService;


        public CheckGroupColumn(ISystemImporterUnitOfWork systemImporterUnitOfWork,
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

        public bool IsSameColumn(FileStatus fileStatus)
        {

            FileInfo existingFile = new FileInfo(fileStatus.FileUrl);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<string> ExcelFileColumnName = new List<string>();
            var AllExcelData = _excelDataService.GetAllExcelDatas();
            var AllExcelFieldData = _excelFieldDataService.GetAllExcelFieldDatas();
            bool ColumnMatch;

            List<ExcelData> ExcelDataWithGroupId = new List<ExcelData>();

            foreach (var excelData in AllExcelData)
            {
                if (excelData.GroupId == fileStatus.GroupId)
                {
                    ExcelDataWithGroupId.Add(excelData);
                }
            }

            if (ExcelDataWithGroupId.Count() == 0)
            {
                return ColumnMatch = true;
            }

            List<ExcelFieldData> ExcelFieldDataWithExcelDataId = new List<ExcelFieldData>();
            var ExcelDataWithGroupIdFirstValue = ExcelDataWithGroupId.First();
            List<string> ColumnName = new List<string>();

            foreach (var excelFildData in AllExcelFieldData)
            {
                if (ExcelDataWithGroupIdFirstValue.Id == excelFildData.ExcelDataId)
                {
                    ColumnName.Add(excelFildData.Name);
                }
            }
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                                                                                     //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;                       //get Column Count

                for (int i = 1; i <= colCount; i++)
                {
                   ExcelFileColumnName.Add(worksheet.Cells[1, i].Value?.ToString().Trim());
                                                            
                }

            }

            
            ColumnMatch = ExcelFileColumnName.SequenceEqual(ColumnName);

            return ColumnMatch;

        }



    }
}

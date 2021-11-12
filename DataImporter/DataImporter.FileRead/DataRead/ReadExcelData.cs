using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Services;
using DataImporter.SystemImporter.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.FileRead.DataRead
{
    public class ReadExcelData : IReadExcelData
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;
        private readonly IFileStatusService _fileStatusService;
        private readonly IExcelFieldDataService _excelFieldDataService;
        private readonly IExcelDataService _excelDataService;
        private readonly IGroupService _groupService;


        public ReadExcelData(ISystemImporterUnitOfWork systemImporterUnitOfWork,
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


        public void ReadData(FileStatus fileStatus)
        {

            FileInfo existingFile = new FileInfo(fileStatus.FileUrl);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                                                                             //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;                //get Column Count
                int rowCount = worksheet.Dimension.End.Row;                   //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    CreateExcelData(fileStatus);
                    var excelDataId = GetLastId();
                    for (int col = 1; col <= colCount; col++)
                    {
                        CreateExcelFieldData(worksheet.Cells[1, col].Value?.ToString().Trim(),
                                             worksheet.Cells[row, col].Value?.ToString().Trim(),
                                             excelDataId);
                    }
                }

            }
            UpdataFileStatus(fileStatus);
            File.Delete(fileStatus.FileUrl);

        }
        internal void CreateExcelData(FileStatus fileStatus)
        {
            var excelData = new ExcelData();
            excelData.InsertDate = fileStatus.UplodeTime;
            excelData.GroupId = fileStatus.GroupId;

            _excelDataService.CreateExcelData(excelData);
        }

        internal void UpdataFileStatus(FileStatus fileStatus)
        {
            var filestatusUpdate = new FileStatus();
            filestatusUpdate.FileName = fileStatus.FileName;
            filestatusUpdate.FileUrl = fileStatus.FileUrl;
            filestatusUpdate.GroupId = fileStatus.GroupId;
            filestatusUpdate.Id = fileStatus.Id;
            filestatusUpdate.GroupName = GetGroupName(fileStatus.GroupId);
            filestatusUpdate.UplodeTime = fileStatus.UplodeTime;
            filestatusUpdate.Status = "Active";
            _fileStatusService.UpdateFileStatus(filestatusUpdate);
        }
        internal string GetGroupName(int Id)
        {
            var group = _groupService.GetGroup(Id);

            return group.GroupName;
        }

        internal void CreateExcelFieldData(string name, string value, int excelDataId)
        {
            var excelFieldData = new ExcelFieldData();
            excelFieldData.Name = name;
            excelFieldData.Value = value;
            excelFieldData.ExcelDataId = excelDataId;

            _excelFieldDataService.CreateExcelFieldData(excelFieldData);
        }

        internal int GetLastId()
        {
            var GetAllExcelData = _excelDataService.GetAllExcelDatas();
            List<int> allExcelDataId = new List<int>();
            foreach (var eachRow in GetAllExcelData)
            {
                allExcelDataId.Add(eachRow.Id);

            }

            return allExcelDataId.Max();
        }


    }
}
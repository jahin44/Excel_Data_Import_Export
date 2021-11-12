using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.FileRead.DataRead;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.GroupModel
{
    public class ExportGroupModel
    {
        public int Id { get; set; }
        
        public string GroupName { get; set; }
        [Required]
        public string Email { get; set; }


        private readonly IGroupService _groupService;
        private readonly IExportExcel _exportExcel;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IEmailService _emailService;
        private readonly IExportHistoryService _exportHistoryService;


        public ExportGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _exportExcel = Startup.AutofacContainer.Resolve<IExportExcel>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDateTimeUtility>();
            _emailService = Startup.AutofacContainer.Resolve<IEmailService>();
            _exportHistoryService = Startup.AutofacContainer.Resolve<IExportHistoryService>();


        }
        public void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);
            Id = group.Id;
            GroupName = group?.GroupName;
        }

        internal DataTable ExportAndEmail( int Id , DataTable dataTable)
        {

           return _exportExcel.ExportTable(Id , dataTable);
             
        }
        internal void FileEmail(byte[] bytes, string fileName,string email)
        {

            _emailService.SendFileEmail(email, "Excel File", "Export File", bytes,fileName);
            CreateExportHistory();

        }
        internal void CreateExportHistory()
        {
            var exportHistory = new ExportHistory();
            exportHistory.GroupId = Id;
            exportHistory.GroupName = GroupName;
            exportHistory.ExportDate = _dateTimeUtility.Now;
            exportHistory.Email = Email;

            _exportHistoryService.CreateExportHistory(exportHistory);

        }

    }
}

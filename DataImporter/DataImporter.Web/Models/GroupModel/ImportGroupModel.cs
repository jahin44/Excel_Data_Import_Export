using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.FileRead.DataRead;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DataImporter.SystemImporter.Exceptions;

namespace DataImporter.Web.Models.GroupModel
{
    public class ImportGroupModel
    {
        [Required, Range(1, 50000)]
        public int Id { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Group Name should be less than 200 charcaters")]
        public string GroupName { get; set; }
        public string File { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
        
        public string FileUrl { get; set; }
        public DateTime InsertDate { get; set; }


        private readonly IFileStatusService _fileStatusService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly ICheckGroupColumn _checkGroupColumn;
        private readonly IPreviewData _previewData;

        public ImportGroupModel()
        {
            _fileStatusService = Startup.AutofacContainer.Resolve<IFileStatusService>();
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDateTimeUtility>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _checkGroupColumn = Startup.AutofacContainer.Resolve<ICheckGroupColumn>();
            _previewData = Startup.AutofacContainer.Resolve<IPreviewData>();

        }
        public ImportGroupModel(IFileStatusService fileStatusService)
        {
            _fileStatusService = fileStatusService;
        }

        public void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);
            Id = (int)(group?.Id);
            GroupName = group?.GroupName;
        }
        private string MakeUrl(string fileName)
        {
            StringBuilder makeUrl = new StringBuilder();
            makeUrl.Append(FileUrl);
            makeUrl.Append("\\");
            makeUrl.Append(fileName);
            return makeUrl.ToString();
        }
        internal void CreateFileStatus()
        {            
            var fileStatus = new FileStatus();
            fileStatus.GroupId = Id ;
            fileStatus.GroupName = GroupName;
            fileStatus.UplodeTime = _dateTimeUtility.Now;
            fileStatus.Status = "processing";
            fileStatus.FileName = FormFile.FileName;
            fileStatus.FileUrl = MakeUrl(fileStatus.FileName);

            if (_checkGroupColumn.IsSameColumn(fileStatus)== true)
            {
                _fileStatusService.CreateFileStatus(fileStatus);
            }
            else
            {
                throw new InvalidParameterException("Data Not Same");
            }

        }

        public string TableHtmlData(IFormFile _formFile,string _fileLocation)
        {
            var Tabledata= _previewData.TableExcelData(_formFile,_fileLocation);
            return Tabledata;
        }

    }
}

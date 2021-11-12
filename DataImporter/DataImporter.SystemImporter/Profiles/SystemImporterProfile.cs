using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = DataImporter.SystemImporter.Entities;
using BO = DataImporter.SystemImporter.BusinessObjects;

namespace DataImporter.SystemImporter.Profiles
{
    public class SystemImporterProfile : Profile
    {
        public SystemImporterProfile()
        {
            CreateMap<EO.Group, BO.Group>().ReverseMap();
            CreateMap<EO.ExcelData, BO.ExcelData>().ReverseMap();
            CreateMap<EO.ExcelFieldData, BO.ExcelFieldData>().ReverseMap();
            CreateMap<EO.FileStatus, BO.FileStatus>().ReverseMap();
            CreateMap<EO.ExportHistory, BO.ExportHistory>().ReverseMap();

        }
    }
}

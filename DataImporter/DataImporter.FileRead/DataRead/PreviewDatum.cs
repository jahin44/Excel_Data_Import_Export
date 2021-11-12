using AutoMapper;
using DataImporter.Common.Utilities;
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
    public class PreviewDatum : IPreviewData
    {
        private readonly ISystemImporterUnitOfWork _systemImporterUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IMapper _mapper;

        public PreviewDatum(ISystemImporterUnitOfWork systemImporterUnitOfWork,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper)
        {
            _systemImporterUnitOfWork = systemImporterUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
            _mapper = mapper;
        }

        public string TableExcelData(IFormFile file, string newPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            StringBuilder sb = new StringBuilder();

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                string FileExtension = Path.GetExtension(file.FileName).ToLower();
                string fullPath = Path.Combine(newPath, file.FileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    FileInfo existingFile = new FileInfo(fullPath);

                    using (ExcelPackage package = new ExcelPackage(existingFile))
                    {                                                                                             
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int colCount = worksheet.Dimension.End.Column;                        
                        int rowCount = worksheet.Dimension.End.Row;
                        sb.Append("<table class='table table-bordered'><tr>");
                        
                        for (int i = 1; i <= colCount; i++)
                        {
                            sb.Append("<th>");
                            sb.Append(worksheet.Cells[1, i].Value?.ToString().Trim());
                            sb.Append("</th>");
                        }
                        sb.Append("</tr>");
                                                 
                        for (int i = 2; i <= rowCount; i++)
                        {
                            sb.AppendLine("<tr>");

                            for (int j = 1; j <= colCount; j++)
                            {
                                sb.Append("<td>");
                                sb.Append(worksheet.Cells[i, j].Value?.ToString().Trim()).ToString();
                                sb.Append("</td>");

                            }
                            sb.AppendLine("</tr>");
                            
                            if (i > 9)
                            {
                                break;
                            }
                        }
                        sb.Append("</table>");
                    }                     
                }
            }

            return sb.ToString();
        }

    }
}

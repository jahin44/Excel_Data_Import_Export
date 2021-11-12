using DataImporter.Data;
using DataImporter.SystemImporter.Contexts;
using DataImporter.SystemImporter.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.UnitOfWorks
{
    public class SystemImporterUnitOfWork : UnitOfWork, ISystemImporterUnitOfWork
    {
        public IGroupRepository Groups { get; private set; }

        public IExcelDataRepository ExcelDatas { get; private set; }
        public IExcelFieldDataRepository ExcelFieldDatas { get; private set; }
        public IFileStatusRepository FileStatuss { get; private set; }
        public IExportHistoryRepository ExportHistorys { get; private set; }


        public SystemImporterUnitOfWork(ISystemImporterDbContext context,
            IGroupRepository groups,
            IExcelDataRepository excelDatas,
            IExcelFieldDataRepository excelFieldDatas,
            IFileStatusRepository fileStatuss,
            IExportHistoryRepository exportHistorys

            ) : base((DbContext)context)
        {
            Groups = groups;
            ExcelDatas = excelDatas;
            ExcelFieldDatas = excelFieldDatas;
            FileStatuss = fileStatuss;
            ExportHistorys = exportHistorys;
        }
    }
}

using DataImporter.Data;
using DataImporter.SystemImporter.Repositories;

namespace DataImporter.SystemImporter.UnitOfWorks
{
    public interface ISystemImporterUnitOfWork : IUnitOfWork
    {
        IGroupRepository  Groups { get; }
        IExcelDataRepository ExcelDatas { get; }
        IExcelFieldDataRepository ExcelFieldDatas { get; }
        IFileStatusRepository FileStatuss { get; }
        IExportHistoryRepository ExportHistorys { get; }

    }
}
using Autofac;
using DataImporter.SystemImporter.Contexts;
using DataImporter.SystemImporter.Repositories;
using DataImporter.SystemImporter.Services;
using DataImporter.SystemImporter.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter
{
    public class SystemImporterModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public SystemImporterModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SystemImporterDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<SystemImporterDbContext>().As<ISystemImporterDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            
            builder.RegisterType<GroupRepository>().As<IGroupRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelDataRepository>().As<IExcelDataRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelFieldDataRepository>().As<IExcelFieldDataRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileStatusRepository>().As<IFileStatusRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExportHistoryRepository>().As<IExportHistoryRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<SystemImporterUnitOfWork>().As<ISystemImporterUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelDataService>().As<IExcelDataService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelFieldDataService>().As<IExcelFieldDataService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileStatusService>().As<IFileStatusService>()
               .InstancePerLifetimeScope();
            builder.RegisterType<ExportHistoryService>().As<IExportHistoryService>()
               .InstancePerLifetimeScope();
            builder.RegisterType<ShowGroupDataService>().As<IShowGroupDataService>()
              .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

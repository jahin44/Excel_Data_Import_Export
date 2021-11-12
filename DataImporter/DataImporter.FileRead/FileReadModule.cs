using Autofac;
using DataImporter.FileRead.DataRead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.FileRead
{
    public class FileReadModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           
            builder.RegisterType<ReadExcelData>().As<IReadExcelData>()
               .InstancePerLifetimeScope();
            builder.RegisterType<ExportExcel>().As<IExportExcel>()
             .InstancePerLifetimeScope();
            builder.RegisterType<CheckGroupColumn>().As<ICheckGroupColumn>()
             .InstancePerLifetimeScope();
            builder.RegisterType<PreviewDatum>().As<IPreviewData>()
            .InstancePerLifetimeScope();

            base.Load(builder);
        }

    }
}

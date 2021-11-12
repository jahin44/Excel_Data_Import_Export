using Autofac;
using DataImporter.Web.Models.GroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GroupListModel>().AsSelf();
            builder.RegisterType<EditGroupModel>().AsSelf();
            builder.RegisterType<CreateGroupModel>().AsSelf();
            builder.RegisterType<ShowGroupTableModel>().AsSelf();
            builder.RegisterType<ExportGroupModel>().AsSelf();



            base.Load(builder);
        }
    }
}

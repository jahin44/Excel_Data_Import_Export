using DataImporter.SystemImporter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.SystemImporter.Contexts
{
    public class SystemImporterDbContext :  DbContext, ISystemImporterDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public SystemImporterDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // one to many relationship
            modelBuilder.Entity<Group>()
                .HasKey(g => g.Id);
            modelBuilder.Entity<ExcelData>()
                .HasKey(ed => ed.Id);
            modelBuilder.Entity<ExcelFieldData>()
                .HasKey(efd => efd.Id);
            modelBuilder.Entity<FileStatus>()
                .HasKey(fs => fs.Id);
            modelBuilder.Entity<ExportHistory>()
               .HasKey(eh => eh.Id);

            modelBuilder.Entity<FileStatus>()
               .HasOne(ed => ed.Group)
               .WithMany(g => g.FileStatuses)
               .HasForeignKey(ed => ed.GroupId);

            modelBuilder.Entity<ExportHistory>()
              .HasOne(ed => ed.Group)
              .WithMany(g => g.ExportHistories)
              .HasForeignKey(eh => eh.GroupId);

            modelBuilder.Entity<ExcelData>()
                .HasOne(ed => ed.Group)
                .WithMany(g => g.ExcelDatas)
                .HasForeignKey(ed => ed.GroupId);

            modelBuilder.Entity<ExcelFieldData>()
                .HasOne(efd => efd.ExcelData)
                .WithMany(ed => ed.ExcelFieldDatas)
                .HasForeignKey(efd => efd.ExcelDataId);

             

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<ExcelData> ExcelDatas { get; set; }
        public DbSet<ExcelFieldData> ExcelFieldDatas { get; set; }
        public DbSet<FileStatus> FileStatusess { get; set; }
        public DbSet<ExportHistory> ExportHistories { get; set; }


    }
}

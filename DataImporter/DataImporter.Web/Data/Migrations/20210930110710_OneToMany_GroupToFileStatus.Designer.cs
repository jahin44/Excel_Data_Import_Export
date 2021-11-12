﻿// <auto-generated />
using System;
using DataImporter.SystemImporter.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataImporter.Web.Migrations.SystemImporterDb
{
    [DbContext(typeof(SystemImporterDbContext))]
    [Migration("20210930110710_OneToMany_GroupToFileStatus")]
    partial class OneToMany_GroupToFileStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.ExcelData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("ExcelDatas");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.ExcelFieldData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExcelDataId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcelDataId");

                    b.ToTable("ExcelFieldDatas");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.FileStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UplodeTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("FileStatusess");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.ExcelData", b =>
                {
                    b.HasOne("DataImporter.SystemImporter.Entities.Group", "Group")
                        .WithMany("ExcelDatas")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.ExcelFieldData", b =>
                {
                    b.HasOne("DataImporter.SystemImporter.Entities.ExcelData", "ExcelData")
                        .WithMany("ExcelFieldDatas")
                        .HasForeignKey("ExcelDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelData");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.FileStatus", b =>
                {
                    b.HasOne("DataImporter.SystemImporter.Entities.Group", "Group")
                        .WithMany("FileStatuses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.ExcelData", b =>
                {
                    b.Navigation("ExcelFieldDatas");
                });

            modelBuilder.Entity("DataImporter.SystemImporter.Entities.Group", b =>
                {
                    b.Navigation("ExcelDatas");

                    b.Navigation("FileStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}

using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.SystemImporter.Repositories;
using DataImporter.SystemImporter.Services;
using DataImporter.SystemImporter.UnitOfWorks;
using Moq;
using NUnit.Framework;
using EO = DataImporter.SystemImporter.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Exceptions;

namespace DataImporter.SystemImporter.Tests
{
    [ExcludeFromCodeCoverage]
    public class ExcelDataServiceTests
    {
        private AutoMock _mock;
        private Mock<ISystemImporterUnitOfWork> _systemImporterUnitOfWork;
        private Mock<IExcelDataRepository> _excelDataRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IExcelDataService _excelDataService;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            _systemImporterUnitOfWork = _mock.Mock<ISystemImporterUnitOfWork>();
            _excelDataRepositoryMock = _mock.Mock<IExcelDataRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _excelDataService = _mock.Create<ExcelDataService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _systemImporterUnitOfWork.Reset();
            _excelDataRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [Test]
        public void GetAllExcelDatas_GetExcelDataList_ReturnExcelDataList()
        {
            List<EO.ExcelData> excelDatas = new List<EO.ExcelData> {
            new EO.ExcelData { Id = 5, GroupId = 2 },
            new EO.ExcelData { Id = 6, GroupId = 2},
            new EO.ExcelData { Id = 7, GroupId = 2}
            };

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            _excelDataRepositoryMock.Setup(x => x.GetAll()).Returns(excelDatas).Verifiable();

            _excelDataService.GetAllExcelDatas();

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _excelDataRepositoryMock.VerifyAll()

           );

        }

        [Test]
        public void CreateExcelData_ExcelDataDoesNotExists_ThrowsException()
        {
            ExcelData excelData = null;

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            Should.Throw<InvalidParameterException>(
                () => _excelDataService.CreateExcelData(excelData)
            );
        }

        [Test]
        public void CreateExcelData_ExcelDataExists_AddExcelData()
        {
            var excelData = new ExcelData { Id = 2, GroupId = 2 };

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            _systemImporterUnitOfWork.Setup(x => x.Save()).Verifiable();

            _excelDataService.CreateExcelData(excelData);

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _excelDataRepositoryMock.VerifyAll()

           );

        }
        [Test]
        public void GetExcelData_ExcelDataDoesNotExists_ReturnNull()
        {
            const int Id = 5;

            EO.ExcelData excelDataEntity = null;

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            _excelDataRepositoryMock.Setup(x => x.GetById(Id))
                .Returns(excelDataEntity).Verifiable();

            var RetunExcelData = _excelDataService. GetExcelData(Id);

            this.ShouldSatisfyAllConditions(
               () => _excelDataRepositoryMock.VerifyAll(),
               () => RetunExcelData.ShouldBe(null)      
           );

        }
      
        [Test]
        public void GetExcelData_ExcelDataExists_ReturnExcelData()
        {
            const int Id = 5;

            EO.ExcelData excelDataEntity = new EO.ExcelData { Id = 5, GroupId = 3, InsertDate = DateTime.Now };

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            _excelDataRepositoryMock.Setup(x => x.GetById(Id))
                .Returns(excelDataEntity).Verifiable();

            var RetunExcelData = _excelDataService.GetExcelData(Id);              //Ther is Some Problem Mapper not Work Properly

            this.ShouldSatisfyAllConditions(
               () => _excelDataRepositoryMock.VerifyAll() 
           );

        }


        [Test]
        public void UpdateExcelData_ExcelDataDoesNotExists_ThrowsException()
        {
            ExcelData excelData = null;

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            Should.Throw<InvalidOperationException>(
                () => _excelDataService.UpdateExcelData(excelData)
            );
        }

        [Test]
        public void UpdateExcelData_ExcelDataExists_UpdateAdd()
        {
            var excelData = new ExcelData { Id = 2, GroupId = 2 };

            var excelDataEntity = new EO.ExcelData { Id = excelData.Id, GroupId = excelData.GroupId };

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            _systemImporterUnitOfWork.Setup(x => x.Save()).Verifiable();

            _excelDataRepositoryMock.Setup(x => x.GetById(excelData.Id))
                .Returns(excelDataEntity);

            _excelDataService.UpdateExcelData(excelData);

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _excelDataRepositoryMock.VerifyAll(),
               () => excelDataEntity.GroupId.ShouldBe(excelData.GroupId),
               () => excelDataEntity.Id.ShouldBe(excelData.Id)


           );

        }

        [Test]
        public void DeleteExcelData_ExcelDataExists_Remove()
        {
            const int Id = 2;

            _systemImporterUnitOfWork.Setup(x => x.ExcelDatas)
                .Returns(_excelDataRepositoryMock.Object);

            _excelDataRepositoryMock.Setup(x => x.Remove(Id)).Verifiable();

            _systemImporterUnitOfWork.Setup(x => x.Save()).Verifiable();

            _excelDataService.DeleteExcelData(Id);

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _excelDataRepositoryMock.VerifyAll()

           );

        }
    }
}

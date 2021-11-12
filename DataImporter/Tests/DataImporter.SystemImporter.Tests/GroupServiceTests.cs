using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.SystemImporter.BusinessObjects;
using EO = DataImporter.SystemImporter.Entities;
using DataImporter.SystemImporter.Repositories;
using DataImporter.SystemImporter.Services;
using DataImporter.SystemImporter.UnitOfWorks;
using Moq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using Shouldly;
using System;
using DataImporter.SystemImporter.Exceptions;
using System.Collections.Generic;

namespace DataImporter.SystemImporter.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupServiceTests 
    {
        private AutoMock _mock;
        private Mock<ISystemImporterUnitOfWork> _systemImporterUnitOfWork;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IGroupService _groupService;

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
            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _groupService = _mock.Create<GroupService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _systemImporterUnitOfWork.Reset();
            _groupRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [Test]
        public void GetAllGroups_GetGrooupList_ReturnGroupList()
        {
            List<EO.Group> groups = new List<EO.Group> { 
            new EO.Group { Id = 5, GroupName = "Phone Number" },
            new EO.Group { Id = 6, GroupName = "Student" },
            new EO.Group { Id = 7, GroupName = "Office Data" }
            };

            _systemImporterUnitOfWork.Setup(x => x.Groups)
                .Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.GetAll()).Returns(groups).Verifiable();

            _groupService.GetAllGroups();

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _groupRepositoryMock.VerifyAll()

           );

        }

        [Test]
        public void CreateGroup_GroupDoesNotExists_ThrowsException()
        {
            Group group = null;

            _systemImporterUnitOfWork.Setup(x => x.Groups)
                .Returns(_groupRepositoryMock.Object);

            Should.Throw<InvalidParameterException>(
                () => _groupService.CreateGroup(group)
            );
        }

        [Test]
        public void CreateGroup_GroupExists_AddGroup()
        {
            var group = new Group { Id = 2, GroupName = "Number" };

            _systemImporterUnitOfWork.Setup(x => x.Groups)
                .Returns(_groupRepositoryMock.Object);

            _systemImporterUnitOfWork.Setup(x => x.Save()).Verifiable();

            _groupService.CreateGroup(group);

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _groupRepositoryMock.VerifyAll()
                
           );

        }
        [Test]
        public void UpdateGroup_GroupDoesNotExists_ThrowsException()
        {
            Group group = null;

            _systemImporterUnitOfWork.Setup(x => x.Groups)
                .Returns(_groupRepositoryMock.Object);

            Should.Throw<InvalidParameterException>(
                () => _groupService.UpdateGroup(group)
            );
        }

        [Test]
        public void UpdateGroup_GroupExists_UpdateAdd()
        {
            var group = new Group { Id = 2, GroupName = "Number" };

            var groupEntity = new EO.Group { Id = group.Id, GroupName = group.GroupName };

            _systemImporterUnitOfWork.Setup(x => x.Groups)
                .Returns(_groupRepositoryMock.Object);

            _systemImporterUnitOfWork.Setup(x => x.Save()).Verifiable();

            _groupRepositoryMock.Setup(x => x.GetById(group.Id))
                .Returns(groupEntity);

            _groupService.UpdateGroup(group);

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _groupRepositoryMock.VerifyAll(),
               () => groupEntity.GroupName.ShouldBe(group.GroupName),
               () => groupEntity.Id.ShouldBe(group.Id)


           );

        }

        [Test]
        public void DeleteGroup_GroupExists_Remove()
        {
            const int Id = 2;

            _systemImporterUnitOfWork.Setup(x => x.Groups)
                .Returns(_groupRepositoryMock.Object);

            _groupRepositoryMock.Setup(x => x.Remove(Id)).Verifiable();

            _systemImporterUnitOfWork.Setup(x => x.Save()).Verifiable();

            _groupService.DeleteGroup(Id);

            this.ShouldSatisfyAllConditions(
               () => _systemImporterUnitOfWork.VerifyAll(),
               () => _groupRepositoryMock.VerifyAll()
                
           );

        }


    }
}
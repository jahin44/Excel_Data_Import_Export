using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.SystemImporter.BusinessObjects;
using DataImporter.SystemImporter.Services;
using DataImporter.Web.Models.GroupModel;
using Moq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace DataImporter.Web.Tests
{
    [ExcludeFromCodeCoverage]
    public class EditGroupModelTests
    {
        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private Mock<IGroupService> _groupServiceMock;
        private EditGroupModel _model;

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
            _groupServiceMock = _mock.Mock<IGroupService>();
            _mapperMock = _mock.Mock<IMapper>();
            _model = _mock.Create<EditGroupModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _groupServiceMock?.Reset();
            _mapperMock?.Reset();
        }

        [Test]
        public void LoadModelData_CourseExists_LoadsProperties()
        {
            // Arrange
            const int id = 5;

            var group = new Group
            {
                GroupName = "Asp.net",
                Id = 5
            };

            _groupServiceMock.Setup(x => x.GetGroup(id)).Returns(group).Verifiable();

            _mapperMock.Setup(x => x.Map(
                group, It.IsAny<EditGroupModel>()
            )).Verifiable();
 
            // Act
            _model.LoadModelData(id);
            
            // Assert
            _groupServiceMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}
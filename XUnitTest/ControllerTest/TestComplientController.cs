using BusinessLogicLayer.Repository.Contracts;
using DataAccessLayer.Models;
using GlobalEntity.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PrasentationLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnitTest.MockData;

namespace XUnitTest.ControllerTest
{
    public class TestComplientController
    {
        private readonly Mock<IComplaintBusinessLayercs> _mock;
        private readonly ComplaintController _controller;
        public TestComplientController()
        {
            _mock=new Mock<IComplaintBusinessLayercs>();
            _controller=new ComplaintController(_mock.Object);
        }
        [Fact]
        public async Task GetAll_ShouldReturn200StatusCode()
        {
            //Arrange
            _mock.Setup(x=>x.GetAllComplients()).Returns(DummyData.GetAllRecords());
            //Act
            var result= (OkObjectResult) await _controller.GetAllRecords();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsNotType<BadRequest>(result);
        }
        [Fact]
        public async Task GetAllComplients_ShoudReturn204StatusCode()
        {
            //Arrange
            _mock.Setup(x => x.GetAllComplients()).Returns(DummyData.NoContext());
            //Act
            var result = (OkObjectResult) await _controller.GetAllRecords();
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsNotType<BadRequest>(result);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetByEmployeeId_ShouldReturn200StatusCode()
        {
            string id = "1001";
            //Arrange
            _mock.Setup(x => x.GetByComplientId(id)).Returns(DummyData.GetComplaintById(id));
            //Act
            var result = (OkObjectResult)await _controller.GetComplientByID(id);
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
            Assert.IsNotType<BadRequest>(result);
        }
        [Fact]
        public async Task GetEmployeeById_ShouldNull()
        {
            string id ="";
            _mock.Setup(x => x.GetByComplientId(id)).Returns(DummyData.GetComplaintById(id));
            //Act
            var result = (OkObjectResult)await _controller.GetComplientByID(id);
            //Assert
            Assert.IsType<OkObjectResult>(result);
           // Assert.Null(result);
            Assert.IsNotType<BadRequest>(result);

        }
        [Fact]
        public async Task GetComplientListByEmployeeId_ShouldReturn200Status()
        {
            string id = "MLI1135";
            //Act
            _mock.Setup(x => x.RequestedByEmployee(id)).Returns(DummyData.GetByEmployeeId(id));
            //Arrange
            var result=(OkObjectResult) await _controller.GetComplientList(id);
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetComplientListById_ShouldReturnNullValue()
        {
            string id = "";
            //Act
            _mock.Setup(x => x.RequestedByEmployee(id)).Returns(DummyData.GetByEmployeeId(id));
            //Arrange
            var result = (OkObjectResult)await _controller.GetComplientList(id);
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
        }
        //[Fact]
        //public async Task AddComplaint_ShouldReturn200Status()
        //{
        //    ComplientBox comp = new ComplientBox();
        //    //Act
        //    _mock.Setup(x=>x.AddComplient(comp)).Returns(DummyData.AddComplaint(comp));
        //    //Arrange
        //   // var result = (OkObjectResult)await _controller.AddComplient(comp);
        //    //Assert
        //    //Assert.IsType<OkObjectResult>(result);
        //}
    }
}

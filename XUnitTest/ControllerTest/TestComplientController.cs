using BusinessLogicLayer.Repository.Contracts;
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
        private readonly Mock<IComplientBusinessLayercs> _mock;
        private readonly ComplientController _controller;
        public TestComplientController()
        {
            _mock=new Mock<IComplientBusinessLayercs>();
            _controller=new ComplientController(_mock.Object);
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
            //Assert.IsNotType<OkObjectResult>(result);
            Assert.Null(result);
            
        }
    }
}

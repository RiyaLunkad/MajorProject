
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CrimeMgmnt.Controllers;
using Xunit;
using CrimeMgmnt.Models;
using CrimeMgmnt.Controllers;
using FluentAssertions;
using Xunit.Abstractions;


namespace Crime.xUnitTestProject
{
    public partial class CyberCellsApiTests
    {
        [Fact]
        public void DeleteCyberCell_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(CyberCellsApiTests.DeleteCyberCell_NotFoundResult);
            var logger = Mock.Of<ILogger<CyberCellsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var Controller = new CyberCellsController(dbContext, logger);
            int findControlRoomId = 900;

            // ACT
            IActionResult actionResultDelete = Controller.DeleteCyberCell(findControlRoomId).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultDelete as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCyberCell_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(CyberCellsApiTests.DeleteCyberCell_BadRequestResult);
            var logger = Mock.Of<ILogger<CyberCellsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new CyberCellsController(dbContext, logger);
            int? findControlRoomId = null;

            // ACT
            IActionResult actionResultDelete = controller.DeleteCyberCell(findControlRoomId).Result;

            // ASSERT - check if the IActionResult is BadRequest 
            Assert.IsType<BadRequestResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultDelete as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCyberCell_OkResult()
        {
            // ARRANGE
            var dbName = nameof(CyberCellsApiTests.DeleteCyberCell_OkResult);
            var logger = Mock.Of<ILogger<CyberCellsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new CyberCellsController(dbContext, logger);
            int findControlRoomId = 1;

            // ACT
            IActionResult actionResultDelete = controller.DeleteCyberCell(findControlRoomId).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultDelete);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultDelete as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
    }
}
    

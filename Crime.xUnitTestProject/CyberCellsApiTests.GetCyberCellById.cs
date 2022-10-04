using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CrimeMgmnt.Controllers;
using CrimeMgmnt.Models;
using System.Linq;


namespace Crime.xUnitTestProject
{

    public partial class CyberCellsApiTests
    {

        [Fact]
        public void GetCyberCellById_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(CyberCellsApiTests.GetCyberCellById_NotFoundResult);
            var logger = Mock.Of<ILogger<CyberCellsController>>();

            // using (var db = DbContextMocker.GetApplicationDbContext(dbName))
            // {
            // }           // db.Dispose(); implicitly called when you exit the USING Block

            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new CyberCellsController(dbContext, logger);
            int findControlRoomId = 900;

            // ACT
            IActionResult actionResultGet = apiController.GetCyberCell(findControlRoomId).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultGet as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
        [Fact]
        public void GetCyberCellById_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(CyberCellsApiTests.GetCyberCellById_BadRequestResult);
            var logger = Mock.Of<ILogger<CyberCellsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new CyberCellsController(dbContext, logger);
            int? findControlRoomId = null;

            // ACT
            IActionResult actionResultGet = controller.GetCyberCell(findControlRoomId).Result;

            // ASSERT - check if the IActionResult is BadRequest
            Assert.IsType<BadRequestResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultGet as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

    }
}
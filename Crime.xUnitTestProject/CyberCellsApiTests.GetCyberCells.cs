using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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
        public void GetCyberCells_OkResult()
        {
            // 1. ARRANGE
            var dbName = nameof(CyberCellsApiTests.GetCyberCells_OkResult);
            var logger = Mock.Of<ILogger<CyberCellsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new CyberCellsController(dbContext, logger);

            // 2. ACT
            IActionResult actionResultGet = controller.GetCyberCells().Result;

            // 3. ASSERT
            // ---- Check if the IActionResult is OK (HTTP 200)
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ---- If the Status Cose is HTTP 200 "OK"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }


    }
}

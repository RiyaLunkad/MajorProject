using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using Moq;
using CrimeMgmnt.Controllers;
using CrimeMgmnt.Models;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace Crime.xUnitTestProject
{
    public partial class CyberCellsApiTests
    {
        [Fact]

        public async void UpdateCyberCell_OkResult01()
        {
            // ARRANGE
            var dbName = nameof(CyberCellsApiTests.UpdateCyberCell_OkResult01);
            var logger = Mock.Of<ILogger<CyberCellsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new CyberCellsController(dbContext, logger);
            int editControlRoomId = 2;

            CyberCell originalCyberCell, changedCyberCell;
            changedCyberCell = new CyberCell
            {
                ControlRoomId = editControlRoomId,

            };
            // ACT #1: Get the Record to Edit.

            // (a) Get the Category to edit (to ensure that the row exists before editing it)
            IActionResult actionResultGet = await apiController.GetCyberCell(editControlRoomId);

            // (b) Check if HTTP 200 "Ok" 
            OkObjectResult OkResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            // (c) Extract the Category Object from the OkObjectResult
            originalCyberCell = OkResult.Value.Should().BeAssignableTo<CyberCell>().Subject;

            // (d) Check if the data to be edited was received from the API
            Assert.NotNull(originalCyberCell);

            _testOutputHelper.WriteLine("Retrieved the Data from the API.");


            try
            {
                // ACT #2: Try to update the data, using a completely new object
                //         NOTE: This will throw the InvalidOperationException!
                var actionResultPutAttempt1 = await apiController.PutCyberCell(editControlRoomId, changedCyberCell);

                // ASSERT - if the update was successfull.
                Assert.IsType<OkResult>(actionResultPutAttempt1);

                _testOutputHelper.WriteLine("Updated the changes back to the API - using a new object.");
            }
            catch (System.InvalidOperationException exp)
            {
                // The PUT operation should throw this exception,
                // because it is an object that does not carry change tracking information.

                _testOutputHelper.WriteLine("Failed to update the change back to the API - using a new object");
                _testOutputHelper.WriteLine($"-- Exception Type: {exp.GetType()}");
                _testOutputHelper.WriteLine($"-- Exception Message: {exp.Message}");
                _testOutputHelper.WriteLine($"-- Exception Source: {exp.Source}");
                _testOutputHelper.WriteLine($"-- Exception TargetSite: {exp.TargetSite}");
            }
        }
        [Fact]
        public async void UpdateCyberCell_OkResult02()
        {
            // ARRANGE
            var dbName = nameof(CyberCellsApiTests.UpdateCyberCell_OkResult02);
            var logger = Mock.Of<ILogger<CyberCellsController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new CyberCellsController(dbContext, logger);
            int editControlRoomId = 2;
            CyberCell originalcyberCell;
            string changedStatus = "--- CHANGED Category Name";

            // ACT #1: Get the Record to Edit.

            // (a) Get the Category to edit (to ensure that the row exists before editing it)
            IActionResult actionResultGet = await apiController.GetCyberCell(editControlRoomId);

            // (b) Check if HTTP 200 "Ok" 
            OkObjectResult OkResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            // (c) Extract the Category Object from the OkObjectResult
            originalcyberCell = OkResult.Value.Should().BeAssignableTo<CyberCell>().Subject;

            // (d) Check if the data to be edited was received from the API
            Assert.NotNull(originalcyberCell);

            _testOutputHelper.WriteLine("Retrieved the Data from the API.");

            // ACT #2: Now, try to update the data
            // SOLUTION: The following lines would work!
            //           Reason, we are modifying the data originally received.
            //           And then, calling the PUT operation.
            //           So, the API is able to find the ChangeTracking data associated to the object.

            // (a) Change the data of the object that was received from the API.
            originalcyberCell.Status = changedStatus;

            // (b) Call the HTTP PUT API to update the changes (with the updated object)
            var actionResultPutAttempt2 = await apiController.PutCyberCell(editControlRoomId, originalcyberCell);

            // ASSERT - if the update was successfull.
            Assert.IsType<NoContentResult>(actionResultPutAttempt2);

            _testOutputHelper.WriteLine("Updated the changes back to the API - using the object received");
        }
    }
}
    
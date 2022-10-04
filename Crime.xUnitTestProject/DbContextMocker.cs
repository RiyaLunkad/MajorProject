using CrimeMgmnt.Data;
using CrimeMgmnt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crime.xUnitTestProject
{
    //we need to make it singleton as static so that we dont use multiple instances of single class.
    public static class DbContextMocker
    {
        /// <summary>
        /// NOTE:  Since all tests run parallelly,
        // ensure that each test method gets its own locally scoped InMemory database
        /// </summary>

        public static ApplicationDbContext GetApplicationDbContext(string databasename)
        {
            // Create a fresh service provider for the InMemory Database instance.
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            // Create a new options instance,
            // telling the context to use InMemory database and the new service provider.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databasename)
                            .UseInternalServiceProvider(serviceProvider)
                            .Options;

            // Create the instance of the DbContext (would be an InMemory database)
            // NOTE: It will use the Scema as defined in the Data and Models folders
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the inmemory database
            dbContext.SeedData();

            return dbContext;
        }

        internal static readonly CyberCell[] TestData_ControllRoom
            = {
                new CyberCell
                {
                    UserId = 1,
                    Status = "Resolved"
                },
                new CyberCell
                {
                   UserId = 2,
                   Status = "On Hold"
                },
            };

        /// <summary>
        ///     An extension Method for the DbContext object.
        /// </summary>
        /// <param name="context"></param>
        private static void SeedData(this ApplicationDbContext context)
        {
            context.cyberCells.AddRange(TestData_ControllRoom);

            context.SaveChanges();
        }

    }
}

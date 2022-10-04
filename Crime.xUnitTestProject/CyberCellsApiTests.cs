using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace Crime.xUnitTestProject
{
    public partial class CyberCellsApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CyberCellsApiTests(
            ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}

using System;
using Xunit;
using Xunit.Abstractions;

namespace Crime.xUnitTestProject
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(
            ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        //Fact will see of you can do several oprations on your controller
        [Fact]
        public void Test1()
        {
            // ARRANGE
            int expectedResult = 4;
            int actualResult;
            int a = 1, b = 3;

            // ACT
            actualResult = a + b;

            _testOutputHelper.WriteLine($"input values are {a} and {b}");
            _testOutputHelper.WriteLine($"expectedResult = {expectedResult}, ActualResult = {actualResult}");

            // ASSERT
            Assert.Equal(expectedResult, actualResult);
        }

    }
}

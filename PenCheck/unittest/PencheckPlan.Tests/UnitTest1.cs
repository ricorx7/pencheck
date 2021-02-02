using PenCheck.Models;
using System;
using Xunit;

namespace PencheckPlan.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Plan plan1 = new Plan();
            plan1.Name = "Bob";
            Assert.Equal("Bob", plan1.Name);
        }
    }
}

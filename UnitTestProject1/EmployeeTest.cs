using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HumanResourcesModel;

namespace UnitTestProject1
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void TestEmployee()
        {
            Employee target = new Employee("Artur", "Ilin", "developer", new Department { Name = "R&D" }, EmployeeRank.Junior);
            Assert.AreEqual("Artur", target.FirstName);
            Assert.AreEqual("Ilin", target.SecondName);
            Assert.AreEqual("developer", target.Role);
            Assert.AreEqual(EmployeeRank.Junior, target.Rank);
        }

    }
}

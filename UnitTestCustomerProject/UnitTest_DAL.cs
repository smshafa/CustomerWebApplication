using System;
using System.Linq;
using Customer.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCustomerProject
{
    [TestClass]
    public class UnitTest_DAL
    {
        [TestMethod]
        public void TestEF()
        {
            //Arrange
            CustomerContext customerContext = new CustomerContext();
            //Act
            int count = customerContext.Provinces.Count();
            //Assert
            Assert.AreEqual(count, 2);

        }
    }
}



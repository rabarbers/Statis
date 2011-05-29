using StatisServiceHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StatisServiceTests
{
    
    
    /// <summary>
    ///This is a test class for HandleDb4oTest and is intended
    ///to contain all HandleDb4oTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HandleDb4oTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddAnalyst
        ///</summary>
        [TestMethod()]
        public void AddAnalystTest()
        {
            string currentUserName = "janka";
            
            string analystUserName = "analītiķis1";
            
            bool actual;
            actual = HandleDb4o.AddAnalyst(currentUserName, analystUserName);
            Assert.AreEqual(true, actual);

            //string analystTest = HandleDb4o.GetUserAnalysts("janka");
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}

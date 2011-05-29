//if WaitService symbol defined, at test class initiation server will work for 2 minutes
//if necessary, generate proxy
//#define WaitService

using System.IO;
using System.ServiceModel;
using System.Linq;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatisServiceContracts;
using StatisServiceHost;
using StatisServiceTests.StatisService;

namespace StatisServiceTests
{
    /// <summary>This is the combined UnitTests file</summary>
    [TestClass]
    public class UnitTests
    {
        //the time we allow for OneWay method calling (milliseconds)
        private const int timeToWait = 100;
        private const string TestDbFile = "TestDb.yap";

        private static ServiceHost _serviceHost;
        
        public UnitTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            _serviceHost = new ServiceHost(typeof(QuestionnaireService));
            _serviceHost.Open();
#if WaitService
            Thread.Sleep(TimeSpan.FromMinutes(2));
#endif
        }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _serviceHost.Close();
        }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void MyTestInitialize()
        {
            if (File.Exists(TestDbFile))
            {
                File.Delete(TestDbFile);
            }

            HandleDb4o.LoadTestData(TestDbFile);
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>Testing whether the database is correctly configured for cascade 
        /// deleting, so all questions are deleted along with the 
        /// questionnaire</summary>

        [TestMethod]
        public void TestQuestionnaireDeletionFromDb()
        {
            Assert.Inconclusive();
        }

        /// <summary>Tests if service returns the same annect data 
        /// as those found in the database</summary>
        [TestMethod]
        public void TestStatisServiceMethodGetQuestionnaire()
        {
            //opening database for data querying for comparison
            var config = Db4oEmbedded.NewConfiguration();
            config.Common.ObjectClass(typeof(Questionnaire)).CascadeOnDelete(true);
            var db = Db4oEmbedded.OpenFile(config, TestDbFile);

            const string testQuestionnaireName = "Q1";

            //retrieving questionnaire from service
            var questionnaire =
                (from Questionnaire q in db
                 where q.Name == testQuestionnaireName
                 select q).FirstOrDefault();

            Assert.IsNotNull(questionnaire, "DB does not contain requested questionnaire.");
            Assert.AreEqual(2, questionnaire.Questions.Count, "Service returns wrong number of Questions in questionnaire.");
            db.Close();
            
            //using service to retrieve test questionnaire from it
            using(var proxy = new QuestionnaireAdministrativeServiceClient())
            {
                proxy.Open();
                var serviceQuestionnaire = proxy.GetQuestionnaire(testQuestionnaireName);

                //commpare db and service returned questionnaires
                Assert.IsNotNull(serviceQuestionnaire, "Service does not return any data.");
                Assert.AreEqual(questionnaire.Name, serviceQuestionnaire.Name, "Service and db contain questionnaires with different names.");
                Assert.AreEqual(questionnaire.Description, serviceQuestionnaire.Description, "Service and db contain questionnaires with different descriptions.");
                Assert.AreEqual(questionnaire.Questions.Count, serviceQuestionnaire.Questions.Count, "Service and db questionnaires contain different number of questions.");
            }
        }

        /// <summary>Tiek pārbaudīta anketu saglabāšanas servisa metode</summary>
        [TestMethod]
        public void TestStatisServiceMethodStoreQuestionnaire()
        {
            var config = Db4oEmbedded.NewConfiguration();
            config.Common.ObjectClass(typeof(Questionnaire)).CascadeOnDelete(true);
            var db = Db4oEmbedded.OpenFile(config, TestDbFile);

            int noOfObjectsBefore;
            var questionnaireExample = new Questionnaire();
            noOfObjectsBefore = db.QueryByExample(questionnaireExample).Count;
            
            var questionnaire = new Questionnaire("Q99", "Clean test database");
            db.Store(questionnaire);

            int noOfObjectsAfter = db.QueryByExample(questionnaireExample).Count;

            Assert.AreNotEqual(noOfObjectsBefore, noOfObjectsAfter, "No new database stored");
            Assert.IsTrue(db.QueryByExample(questionnaireExample).Contains(questionnaire), "Test database not stored");

            /*using(var proxy = new QuestionnaireAdministrativeServiceClient())
            {
                proxy.Open();
                proxy.StoreQuestionnaire(questionnaire);
                
                //commpare db and service returned questionnaires
                Assert.IsNotNull(serviceQuestionnaire, "Service does not return any data.");
                Assert.AreEqual(questionnaire.Name, serviceQuestionnaire.Name, "Service and db contain questionnaires with different names.");
                Assert.AreEqual(questionnaire.Description, serviceQuestionnaire.Description, "Service and db contains questionnaires with different descriptions.");
                Assert.AreEqual(questionnaire.Questions.Count, serviceQuestionnaire.Questions.Count, "Service and db questionnaires contains different number of questions.");
            }*/
        }

        /// <summary>Tiek pārbaudīta anektu dzēšanas metodes loģika</summary>
        [TestMethod]
        public void TestStatisServiceMethodDeleteQuestionnaire()
        {
            var config = Db4oEmbedded.NewConfiguration();
            config.Common.ObjectClass(typeof(Questionnaire)).CascadeOnDelete(true);
            var db = Db4oEmbedded.OpenFile(config, TestDbFile);

            int noOfObjectsBefore;
            var questionnaireExample = new Questionnaire();
            noOfObjectsBefore = db.QueryByExample(questionnaireExample).Count;

            var questionnaire = new Questionnaire("Q99", "Clean test database");
            IObjectSet set = db.QueryByExample(questionnaire);

            var questionnaireVerify = new Questionnaire();
            questionnaireVerify = (Questionnaire)set[0];
            db.Delete(questionnaireVerify);

            int noOfObjectsAfter = db.QueryByExample(questionnaireExample).Count;

            Assert.AreNotEqual(noOfObjectsBefore, noOfObjectsAfter, "Test database not deleted");
            Assert.IsFalse(db.QueryByExample(questionnaireExample).Contains(questionnaire), "Test database not deleted");
        }

        /// <summary>Tiek pārbaudīta REST servisa metode, kas publicē ClientAccessPolicy.xml failu un ļauj Silverlight klientam konsumēt WCF servisu</summary>
        [TestMethod]
        public void TestStatisCrossDomainService()
        {
            Assert.Inconclusive();
        }


        
    }
}

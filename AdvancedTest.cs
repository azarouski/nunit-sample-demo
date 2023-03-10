using System.Collections.Generic;
using System.Net;
using NLog;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using ZebrunnerAgent.Attributes;
using ZebrunnerAgent.Registrar;

namespace NunitAgentSample
{
    [TestFixture]
    [Maintainer("vader")]
    [ZebrunnerClass, ZebrunnerTest]
    public class AdvancedTest
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private RestClient _client;

        [OneTimeSetUp]
        public void SetUp()
        {
            Logger.Info("Start SetUp");
            _client = new RestClient("https://jsonplaceholder.typicode.com");
            _client.UseNewtonsoftJson();
            Logger.Info("Finish SetUp");
        }

        [Test]
        [Maintainer("skywalker")]
        public void Test1()
        {
            Label.AttachToTestRun("TestRunLabel1", "Nunit");
            Label.AttachToTestRun("TestRunLabel2", "Zebrunner");

            Artifact.AttachToTestRun("logo", "./artifacts/zeb.png");
            Artifact.AttachReferenceToTestRun("Zebrunner", "https://zebrunner.com/");
            Artifact.AttachReferenceToTestRun("Nunit", "https://nunit.org/");
            Artifact.AttachReferenceToTestRun("Nunit Zebrunner agent",
                "https://zebrunner.com/documentation/reporting/nunit/");

            Logger.Info("Start 'Test1'");

            Label.AttachToTest("TestLabel", "Zebrunner");
            Artifact.AttachToTest("txt", "./artifacts/zeb.txt");
            Artifact.AttachReferenceToTest("TestArtifactReference", "https://zebrunner.com/");

            var request = new RestRequest("/users");
            var response = _client.Get<List<User>>(request);
            Logger.Info($"Status code (expected: '{HttpStatusCode.OK}', actual:'{response.StatusCode}')");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response status code not as expected.");
            Logger.Info("Finish 'Test1'");
        }
    }
}
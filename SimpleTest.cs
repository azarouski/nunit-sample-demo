using System.Collections.Generic;
using System.Net;
using NLog;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using ZebrunnerAgent.Attributes;

namespace NunitAgentSample
{
    [TestFixture]
    [ZebrunnerClass, ZebrunnerTest]
    public class SimpleTest
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
        public void Test1()
        {
            Logger.Info("Start 'Test1'");
            var request = new RestRequest("/users");
            var response = _client.Get<List<User>>(request);
            Logger.Info($"Status code (expected: '{HttpStatusCode.OK}', actual:'{response.StatusCode}')");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response status code not as expected.");
            Logger.Info("Finish 'Test1'");
        }

        [Test]
        public void Test2()
        {
            Logger.Info("Start 'Test2'");
            var request = new RestRequest("/users");
            var response = _client.Get<List<User>>(request);
            Logger.Info($"Quantity of users (expected: '{10}', actual:'{response.Data.Count}')");
            Assert.AreEqual(10, response.Data.Count, "Users quantity not as expected.");
            Logger.Info("Finish 'Test2'");
        }

        [Test]
        // This test should fail
        public void Test3()
        {
            Logger.Info("Start 'Test3'");
            var request = new RestRequest("/users");
            var response = _client.Get<List<User>>(request);
            Logger.Info($"User name (expected: 'Zebrunner Test', actual:'{response.Data[0].Name}')");
            Assert.AreEqual("Zebrunner Test", response.Data[0].Name, "User name not equal to expected.");
            Logger.Info("Finish 'Test3'");
        }
    }

    public class User
    {
        public long Id;
        public string Name;
        public string Username;
        public string Email;
        public string Phone;
        public string Website;
    }
}
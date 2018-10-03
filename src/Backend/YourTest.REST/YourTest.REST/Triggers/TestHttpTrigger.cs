
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using YourTest.REST.Service;
using YourTest.REST.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using YourTest.REST.Models;
using YourTest.REST.Data;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using YourTest.REST.FileSystem;


namespace YourTest.REST.Triggers
{
    public static class TestHttpTrigger
    {
        public static ITestManager TestManager { get; set; } = CreateTestManager();


        [FunctionName(nameof(GetAllTests))]
        public static async Task<IEnumerable<Test>> GetAllTests(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test")]
            HttpRequestMessage req
            )
        {
            return await TestManager.GetAllAsync();
        }

        [FunctionName(nameof(GetTestById))]
        public static async Task<Test> GetTestById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test/{id:int}")]
            HttpRequestMessage req
            , int id
            )
        {
            return await TestManager.GetByIdAync(id);
        }

        [FunctionName(nameof(ProcessTest))]
        public static async Task<TestSummary> ProcessTest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "test/{id:int}")]
            HttpRequestMessage req
            , Int32 id
            )
        {

            var answers = await req.Content.ReadAsAsync<QuestionAnswer[]>().ConfigureAwait(false);

            var testSummary = TestManager.Verify(id, answers);

            return testSummary;
        }


        private static ITestManager CreateTestManager()
        {
            IRepository<Test> repo = new InMemoryRepository<Test>();
            IDataProvider<Test> dataProvider = new ComposedDataProvider<Test>(
                 new FileDataProvider(Constants.FileDir)
                 );
            dataProvider.Seed(repo);
            return new TestManager(repo);
        }
    }
}

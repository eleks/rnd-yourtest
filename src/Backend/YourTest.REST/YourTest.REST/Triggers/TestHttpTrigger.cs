
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

namespace YourTest.REST.Triggers
{
    public static class TestHttpTrigger
    {
        public static ITestManager TestManager { get; set; } = CreateTestManager();


        [FunctionName(nameof(GetAllTests))]
        public static async Task<IEnumerable<Test>> GetAllTests(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "test")]
            HttpRequest req
            )
        {
            return await TestManager.GetAllAsync();
        }

        [FunctionName(nameof(GetTestById))]
        public static async Task<Test> GetTestById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "test/{id:int}")]
            HttpRequest req
            , int id
            )
        {
            return await TestManager.GetByIdAync(id);
        }

        [FunctionName(nameof(ProcessTest))]
        public static async Task<IActionResult> ProcessTest(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "test/{id:int}")]
            HttpRequestMessage req
            , Int32 id
            )
        {

            var testToProcess = await req.Content.ReadAsAsync<QuestionAnswer[]>().ConfigureAwait(false);

            var testSummery = TestManager.Verify(id, testToProcess);

            return new OkObjectResult(testSummery);
        }


        private static ITestManager CreateTestManager()
        {
            IRepository<Test> repo = new InMemoryRepository<Test>();
            IDataProvider<Test> dataProvider = new DemoDataProvider();
            dataProvider.Seed(repo);
            return new TestManager(repo);
        }
    }
}

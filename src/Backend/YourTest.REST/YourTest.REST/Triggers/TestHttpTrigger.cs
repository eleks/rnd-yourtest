
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

namespace YourTest.REST.Triggers
{
    public static class TestHttpTrigger
    {
        public static ITestManager TestManager { get; set; } = new TestManager(new Repository<Models.Test>());


        [FunctionName(nameof(GetAllTests))]
        public static async Task<IEnumerable<Test>> GetAllTests(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "test")]
            HttpRequest req
            , TraceWriter log
            )
        {
            return await TestManager.GetAllAsync();
        }

        [FunctionName(nameof(GetTestById))]
        public static async Task<Test> GetTestById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "test/{id}")]
            HttpRequest req
            , TraceWriter log
            , int id
            )
        {
            return await TestManager.GetByIdAync(id);
        }

        [FunctionName(nameof(ProcessTest))]
        public static IActionResult ProcessTest(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "test/{id}")]
            HttpRequest req
            , TraceWriter log
            , string id
            )
        {

            throw new NotImplementedException();
        }

    }
}

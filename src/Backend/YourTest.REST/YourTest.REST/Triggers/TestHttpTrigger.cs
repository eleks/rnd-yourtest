
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;

namespace YourTest.REST.Triggers
{
    public static class TestHttpTrigger
    {
        [FunctionName(nameof(GetAllTests))]
        public static IActionResult GetAllTests(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "test")]
            HttpRequest req
            , TraceWriter log
            )
        {
            log.Info("C# HTTP trigger function processed a request.");

            throw new NotImplementedException();
        }

        [FunctionName(nameof(GetTestById))]
        public static IActionResult GetTestById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "test/{id}")]
            HttpRequest req
            , TraceWriter log
            , string id
            )
        {
            log.Info("C# HTTP trigger function processed a request.");

            throw new NotImplementedException();
        }

        [FunctionName(nameof(ProcessTest))]
        public static IActionResult ProcessTest(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "test/{id}")]
            HttpRequest req
            , TraceWriter log
            , string id
            )
        {
            log.Info("C# HTTP trigger function processed a request.");

            throw new NotImplementedException();
        }

    }
}

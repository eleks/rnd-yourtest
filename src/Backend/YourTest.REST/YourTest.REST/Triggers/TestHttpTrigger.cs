
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
        public static async Task<IActionResult> GetAllTests(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test")]
            HttpRequestMessage req
            )
        {
            return new JsonResult(await TestManager.GetAllAsync());
        }

        [FunctionName(nameof(GetTestById))]
        public static async Task<IActionResult> GetTestById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test/{id:int}")]
            HttpRequestMessage req
            , int id
            )
        {
            return new JsonResult(await TestManager.GetByIdAync(id));
        }

        [FunctionName(nameof(ProcessTest))]
        public static async Task<IActionResult> ProcessTest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "test/{id:int}")]
            HttpRequestMessage req
            , Int32 id
            )
        {

            var answers = await req.Content.ReadAsAsync<QuestionAnswer[]>().ConfigureAwait(false);

            var testSummery = TestManager.Verify(id, answers);

            return new JsonResult(testSummery);
        }

        [FunctionName(nameof(SetDataFile))]
        public static async Task<HttpResponseMessage> SetDataFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "test/datafile")]
            HttpRequestMessage req
            )
        {
            var bytes = await req.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            var filePath = $"{Constants.FileDir}\\{Constants.DataFile}";

            using (var testdataFile = FunctionsFile.Open(filePath, FileMode.OpenOrCreate))
            {
                testdataFile.Write(bytes, 0, bytes.Length);

                testdataFile.Flush();

                testdataFile.Close();

            }

            // todo add more aligant way to seed test data with
            TestManager = CreateTestManager();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [FunctionName(nameof(GetDataFile))]
        public static HttpResponseMessage GetDataFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test/datafile")]
            HttpRequestMessage req
            )
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var filePath = Path.Combine(Constants.FileDir, Constants.DataFile);
            response.Content = new ByteArrayContent(FunctionsFile.ReadAllBytes(filePath));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            return response;
        }



        private static ITestManager CreateTestManager()
        {
            IRepository<Test> repo = new InMemoryRepository<Test>();
            IDataProvider<Test> dataProvider = new ComposedDataProvider<Test>(
                 new StubDataProvider()
                 , new FileDataProvider(Constants.FileDir)
                 );
            dataProvider.Seed(repo);
            return new TestManager(repo);
        }
    }
}

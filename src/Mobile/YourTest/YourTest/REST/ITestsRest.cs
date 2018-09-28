using System;
using System.Threading.Tasks;
using Refit;
using YourTest.Models;
using System.Collections.Generic;
namespace YourTest.REST
{
    public interface ITestsRest
    {
        [Get("/api/test")]
        Task<Test[]> GetAllAsync();
        [Get("/api/test/{id}")]
        Task<Test> GetByIdAsync(Int32 id);
        [Post("/api/test/{id}")]
        Task<TestSummary> ProcessTestAsync(Int32 id, [Body]IEnumerable<QuestionAnswer> answers);
    }
}

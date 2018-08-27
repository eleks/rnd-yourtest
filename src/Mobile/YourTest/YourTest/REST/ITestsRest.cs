using System;
using System.Threading.Tasks;
using Refit;
using YourTest.Models;
namespace YourTest.REST
{
    public interface ITestsRest
    {
        [Get("/api/test")]
        Task<Test[]> GetAllAsync();
        [Get("/api/test/{id}")]
        Task<Test> GetBbyIdAsync(Int32 id);
        [Get("/api/test/{id}")]
        Task<TestSummery> ProcessTestAsync(Int32 id, [Body]QuestionAnswer[] answers);
    }
}

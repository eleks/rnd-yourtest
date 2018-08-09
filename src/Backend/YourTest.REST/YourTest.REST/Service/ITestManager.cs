using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using YourTest.REST.Models;
namespace YourTest.REST.Service
{
    public interface ITestManager
    {
        Task<IEnumerable<Test>> GetAllAsync();
        Task<Test> GetByIdAync(Int32 id);
        TestSummery Verify(int testId, IEnumerable<QuestionAnswer> answers);
    }
}

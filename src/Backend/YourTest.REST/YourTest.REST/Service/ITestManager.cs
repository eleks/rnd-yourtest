using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using YourTest.REST.Models;
namespace YourTest.REST.Service
{
    public interface ITestManager
    {
        Task<IEnumerable<Test>> GetAll();
        Task<Test> GetById(Int32 id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourTest.REST.Models;
using YourTest.REST.Repository;
using System.Linq;

namespace YourTest.REST.Service
{
    public class TestManager : ITestManager
    {
        public TestManager(IRepository<Test> repository) => _repo = repository;

        public async Task<IEnumerable<Test>> GetAllAsync() => await Task.Run<IEnumerable<Test>>(() => _repo.Query().ToArray());

        public async Task<Test> GetByIdAync(int id) => await Task.Run(() => _repo.Query().FirstOrDefault(t => t.Id == id));

        private readonly IRepository<Test> _repo;
    }
}

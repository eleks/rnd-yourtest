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

        public async Task<IEnumerable<Test>> GetAllAsync() => await Task.Run<IEnumerable<Test>>(() => _repo.Query().Select(PreperItem).ToArray());

        public async Task<Test> GetByIdAync(int id) => await Task.Run(() => PreperItem(_repo.Query().FirstOrDefault(t => t.Id == id)));

        private static Test PreperItem(Test item)
        {
            if (item == null)
            {
                return null;
            }

            var res = item;
            foreach (var q in res.Questions)
            {
                q.Answare = null;
            }
            return res;
        }

        private readonly IRepository<Test> _repo;
    }
}

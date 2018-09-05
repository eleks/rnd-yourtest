using System;
using System.IO;
using Newtonsoft.Json;
using YourTest.REST.Models;
using YourTest.REST.Repository;
using YourTest.REST.FileSystem;

namespace YourTest.REST.Data
{
    public class FileDataProvider : IDataProvider<Test>
    {
        private readonly string _rootDir;
        private readonly string _filePath;

        public FileDataProvider(String rootDir)
        {
            _rootDir = rootDir;
            _filePath = Path.Combine(_rootDir, Constants.DataFile);
        }

        public void Seed(IRepository<Test> repository)
        {
            var tests = JsonConvert.DeserializeObject<Test[]>(FunctionsFile.ReadAllText(_filePath));

            foreach (var t in tests)
            {
                repository.Insert(t);
            }
        }
    }
}

using System;
using System.IO;
using Newtonsoft.Json;
using YourTest.REST.Models;
using YourTest.REST.Repository;

namespace YourTest.REST.Data
{
    public class FileDataProvider : IDataProvider<Test>
    {
        private readonly string _rootDir;
        private readonly string _filePath;

        public FileDataProvider(String rootDir)
        {
            _rootDir = rootDir;
            _filePath = $"{_rootDir}\\{Constants.DataFile}";
        }

        public void Seed(IRepository<Test> repository)
        {
            var tests = JsonConvert.DeserializeObject<Test[]>(File.ReadAllText(_filePath));

            foreach (var t in tests)
            {
                repository.Insert(t);
            }
        }

        //private static string GetScriptPath() => Path.Combine(GetEnvironmentVariable("HOME"), @"site\wwwroot");

        //private static string GetEnvironmentVariable(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}

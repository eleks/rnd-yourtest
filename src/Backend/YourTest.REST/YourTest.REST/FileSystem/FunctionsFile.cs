using System;
using System.IO;

namespace YourTest.REST.FileSystem
{
    public static class FunctionsFile
    {
        public static String ReadAllText(String filePath) => File.ReadAllText(Path.Combine(GetDeploymentPath(), filePath));
        public static Byte[] ReadAllBytes(String filePath) => File.ReadAllBytes(Path.Combine(GetDeploymentPath(), filePath));
        public static FileStream Open(String filePath, FileMode fileMode) => File.Open(Path.Combine(GetDeploymentPath(), filePath), fileMode);


        public static Boolean IsAzureEnvironment => !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));

        private static string GetDeploymentPath() => IsAzureEnvironment
            ? Path.Combine(GetEnvironmentVariable("HOME"), "site", "wwwroot")
            : "";

        private static string GetEnvironmentVariable(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);



    }
}

using System;
namespace YourTest.REST.Extensions
{
    public static class DoubleExtension
    {
        public static Double ToPercentage(this Double number) => Math.Floor(number * 100) / 100;
    }
}

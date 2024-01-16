using BenchmarkDotNet.Running;
using System.Runtime.InteropServices;
public class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<BenchmarkPerformance>();
    }
}
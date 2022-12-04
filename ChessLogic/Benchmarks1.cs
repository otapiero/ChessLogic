using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System;

namespace ChessLogic
{
    [MemoryDiagnoser]
    public class Benchmarks1
    {

        [Benchmark]
        public void Scenario1()
        {
            int x;
            List<int> list = new List<int>();   
            for(int i = 0; i < 10000; i++) 
            {
                list.Add(i);
            }
            foreach(int i in list)
            {
               x= i + i;
            }
        }

        [Benchmark]
        public void Scenario2()
        {
            int x;
            List<int> list = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(i);
            }
            var u = list.ToArray().AsSpan();

            foreach(int i in u)
            {
                x=i + i;
            }
        }
    }
}

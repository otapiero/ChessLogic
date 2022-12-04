// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ChessLogic;
using System.Security.Cryptography;
using System.Text;
using ChessLogicLibrary;

namespace ChessConsole;

public class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<Benchmarks1>();

    }

}


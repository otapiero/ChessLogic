 using ChessLogic;
using System.Security.Cryptography;
using System.Text;
using ChessLogicLibrary;
using BenchmarkDotNet.Running;

namespace ChessConsole;

public class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var game = new ChessLogicLibrary.Game();
        var b = game.BoardToString();
        PrintBoardString(b);
        for (int i = 0; i < 10; i++)
        {
            game.AIMove();
            b = game.BoardToString();
            Console.Clear();
            PrintBoardString(b);
        }
     


        Console.WriteLine("Hello World!");









        void PrintBoardString(string[,] boardString)
        {
            for (int i = 0; i < boardString.GetLength(0); i++)
            {
                for (int j = 0; j < boardString.GetLength(1); j++)
                {
                    Console.Write(boardString[i, j]+"  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }






        }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_08
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");

            GameConsole console = new GameConsole(content);

            Console.WriteLine("Puzzle 1 : " + Puzzle1(console));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(console));
        }

        static int Puzzle1(GameConsole console)
        {
            console.Run();
            return console.Acc;
        }

        static int Puzzle2(GameConsole console)
        {
            while (console.Status != -1)
            {
                console.Reset();
                console.SwapNextInstruction();
                console.Run();
            }

            return console.Acc;
        }
    }
}

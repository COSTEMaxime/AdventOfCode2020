using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_16
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = System.IO.File.ReadAllText(@"input.txt");
            string[] parsedContent = content.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            var solver = new Solver(parsedContent);

            Console.WriteLine("Puzzle 1 : " + solver.Puzzle1());
            Console.WriteLine("Puzzle 2 : " + solver.Puzzle2());
        }
    }
}

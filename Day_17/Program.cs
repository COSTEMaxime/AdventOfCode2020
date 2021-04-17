using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_17
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");

            ConwayCube cube = new ConwayCube(content);

            Console.WriteLine("Puzzle 1 : " + cube.Puzzle1());
            Console.WriteLine("Puzzle 2 : " + cube.Puzzle2());
        }
    }
}

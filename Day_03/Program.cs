using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_03
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");

            Console.WriteLine("Puzzle 1 : " + Puzzle1(content, 3, 1));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(content));
        }

        static long Puzzle1(string[] input, int slopeRight, int slopeDown)
        {
            int inputSize = input[0].Length;
            int treeCount = 0;
            int xpos = 0;
            int ypos = 0;

            while (ypos < input.Length - 1)
            {
                xpos += slopeRight;
                ypos += slopeDown;

                if (input[ypos][xpos % inputSize] == '#')
                {
                    treeCount++;
                }
            }

            return treeCount;
        }

        static long Puzzle2(string[] input)
        {
            return Puzzle1(input, 1, 1) * Puzzle1(input, 3, 1) * Puzzle1(input, 5, 1) * Puzzle1(input, 7, 1) * Puzzle1(input, 1, 2);
        }
    }
}

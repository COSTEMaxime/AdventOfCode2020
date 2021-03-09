using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_05
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"input.txt");

            int[] seatIDs = GetSeatIDs(lines);

            Console.WriteLine("Puzzle 1 : " + Puzzle1(seatIDs));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(seatIDs));
        }

        static int[] GetSeatIDs(string[] input)
        {
            int[] seatIDs = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                seatIDs[i] = GetSeatID(input[i]);
            }

            return seatIDs;
        }

        static int GetSeatID(string line)
        {
            byte row = 0;
            for (byte i = 0; i < line.Length - 3; i++)
            {
                row |= (byte)((line[i] == 'F') ? 0 << line.Length - 3 - i - 1: 1 << line.Length - 3 - i - 1);
            }

            byte column = 0;
            for (byte i = 7; i < line.Length; i++)
            {
                column |= (byte)((line[i] == 'L') ? 0 << line.Length - i - 1: 1 << line.Length - i - 1);
            }

            return row * 8 + column;
        }

        static int Puzzle1(int[] values)
        {
            return values.Max();
        }

        static int Puzzle2(int[] values)
        {
            Array.Sort(values);
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] - values[i - 1] != 1)
                {
                    return values[i] - 1;
                }
            }

            throw new Exception("Not found");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_09
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");
            long[] values = new long[content.Length];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = long.Parse(content[i]);
            }

            int puzzle1Answer = Puzzle1(values);
            Console.WriteLine("Puzzle 1 : " + values[puzzle1Answer]);
            Console.WriteLine("Puzzle 2 : " + Puzzle2(values, puzzle1Answer));
        }

        static int Puzzle1(long[] values)
        {
            int currentNumberIndex = 25;

            while (IsNumberValid(values, currentNumberIndex++)) { }

            return --currentNumberIndex;
        }

        static bool IsNumberValid(long[] values, int index)
        {
            for (int lowerIndex = index - 25; lowerIndex < index; lowerIndex++)
            {
                for (int upperIndex = index - 1; upperIndex > index - 25; upperIndex--)
                {
                    if (values[lowerIndex] + values[upperIndex] == values[index] && values[lowerIndex] != values[upperIndex])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static long Puzzle2(long[] values, int answerIndex)
        {
            int lowerIndex = answerIndex - 2;
            int upperIndex = answerIndex - 1;
            long total;

            while (true)
            {
                total = 0;
                for (int i = lowerIndex; i <= upperIndex; i++)
                {
                    total += values[i];
                }

                if (total == values[answerIndex]) {
                    long min = values[upperIndex];
                    long max = values[upperIndex];
                    for (int j = lowerIndex; j < upperIndex; j++)
                    {
                        if (values[j] < min) { min = values[j]; }
                        if (values[j] > max) { max = values[j]; }
                    }
                    return min + max;
                }

                if (total < values[answerIndex])
                {
                    lowerIndex--;
                }
                else
                {
                    lowerIndex--;
                    upperIndex--;
                }

            }

            return 0;
        }
    }
}

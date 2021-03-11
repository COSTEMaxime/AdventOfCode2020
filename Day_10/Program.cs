using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");
            int[] values = new int[content.Length];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = int.Parse(content[i]);
            }

            Array.Sort(values);
            Console.WriteLine("Puzzle 1 : " + Puzzle1(values));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(values));
        }

        static int Puzzle1(int[] values)
        {
            int oneDifferenceCount = 0;
            int threeDifferenceCount = 0;
            int previousValue = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] - previousValue == 1) { oneDifferenceCount++; }
                if (values[i] - previousValue == 3) { threeDifferenceCount++; }
                previousValue = values[i];
            }

            // +1 : built in adapter
            return oneDifferenceCount * (threeDifferenceCount + 1);
        }

        static long Puzzle2(int[] values)
        {
            // research
            // 19208 = 2 * 2 * 2 * 7 * 7 * 7 * 7

            // if length = 5 => 7
            // if length = 4 => 4
            // if length = 3 => 2

            // With the algorithm and the example :
            // 5 5 4 3 5 5
            // 7*7*5*4*7*7 
            // = 19208

            // method :
            // split into groups when the difference with the other value is 3
            // let n = group length - 1
            // calculate group arrangements based on formula : 2^n ( if n >= 3) - 2^n-3
            int currentGroupLength = 0;
            long total = 1;
            int previousValue = 0;

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine(values[i]);
                if (values[i] - previousValue == 1) { currentGroupLength++; }
                if (values[i] - previousValue == 3 || i == values.Length - 1) {
                    // if n < 2, there's no 'middle value' to remove
                    if (currentGroupLength >= 2)
                    {
                        currentGroupLength -= 1;
                        int groupArrangements = (int)Math.Pow(2, currentGroupLength);
                        if (currentGroupLength >= 3)
                        {
                            groupArrangements -= (int)Math.Pow(2, currentGroupLength - 3);
                        }

                        total *= groupArrangements;
                    }

                    currentGroupLength = 0;
                }

                previousValue = values[i];
            }

            return total;
        }
    }
}
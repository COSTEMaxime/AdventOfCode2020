using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");
            int[] parsedContent = new int[content.Length];
            for (int i = 0; i < content.Length; i++)
            {
                parsedContent[i] = int.Parse(content[i]);
            }

            Console.WriteLine("Puzzle 1 : " + Puzzle1(parsedContent));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(parsedContent));
        }

        static int Puzzle1(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020)
                    {
                        return input[i] * input[j];
                    }
                }
            }

            throw new Exception();
        }

        static int Puzzle2(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    for (int k = 0; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            return input[i] * input[j] * input[k];
                        }
                    }
                }
            }

            throw new Exception();
        }
    }
}

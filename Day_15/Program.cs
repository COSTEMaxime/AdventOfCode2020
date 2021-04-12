using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_15
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedContent = new int[] { 16, 11, 15, 0, 1, 7 };
            Console.WriteLine("Puzzle 1 : " + Puzzle1(parsedContent, 2020));
            Console.WriteLine("Puzzle 2 : " + Puzzle1(parsedContent, 30000000));
        }

        static int Puzzle1(int[] input, int maxTurns)
        {
            IDictionary<int, int> cache = new Dictionary<int, int>();
            int currentTurn = 1;
            for (; currentTurn < input.Length; currentTurn++)
            {
                cache[input[currentTurn - 1]] = currentTurn;
            }

            int lastNumber = input[input.Length - 1];
            bool firstTime = true;

            currentTurn++;

            for (; currentTurn <= maxTurns; currentTurn++)
            {
                firstTime = !cache.ContainsKey(lastNumber);
                if (firstTime)
                {
                    cache.Add(lastNumber, currentTurn - 1);
                    lastNumber = 0;
                }
                else
                {
                    int tmp = lastNumber;
                    lastNumber = currentTurn - cache[lastNumber] - 1;
                    cache[tmp] = currentTurn - 1;
                }
            }

            return lastNumber;
        }
    }
}

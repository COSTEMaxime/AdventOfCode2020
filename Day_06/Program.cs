using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_06
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = System.IO.File.ReadAllText(@"input.txt");
            string[] parsedContent = content.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            Console.WriteLine("Puzzle 1 : " + Puzzle1(parsedContent));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(parsedContent));
        }

        static int Puzzle1(string[] input)
        {
            int yesAnswersCount = 0;

            foreach (var line in input)
            {
                yesAnswersCount += GetYesAnswersCount(line);
            }

            return yesAnswersCount;
        }

        static int GetYesAnswersCount(string line)
        {
            var tokens = line.Split(new[] { "\r\n" }, StringSplitOptions.None);
            IDictionary<char, int> map = new Dictionary<char, int>();

            foreach (var token in tokens)
            {
                foreach (var letter in token)
                {
                    if (!map.ContainsKey(letter))
                    {
                        map.Add(letter, 1);
                    }
                }
            }

            return map.Count;
        }

        static int Puzzle2(string[] input)
        {
            int yesAnswersCount = 0;

            foreach (var line in input)
            {
                yesAnswersCount += GetEveryoneYesAnswersCount(line);
            }

            return yesAnswersCount;
        }

        static int GetEveryoneYesAnswersCount(string line)
        {
            var tokens = line.Split(new[] { "\r\n" }, StringSplitOptions.None);
            IDictionary<char, int> map = new Dictionary<char, int>();

            foreach (var token in tokens)
            {
                foreach (var letter in token)
                {
                    if (!map.ContainsKey(letter))
                    {
                        map.Add(letter, 1);
                    }
                    else
                    {
                        map[letter]++;
                    }
                }
            }

            int count = 0;
            foreach (var keyValue in map)
            {
                if (keyValue.Value == tokens.Length)
                {
                    count++;
                }
            }
            return count;
        }
    }
}

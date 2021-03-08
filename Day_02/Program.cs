using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");
            PasswordPolicy[] parsedContent = new PasswordPolicy[content.Length];
            for (int i = 0; i < content.Length; i++)
            {
                parsedContent[i] = new PasswordPolicy(content[i]);
            }

            Console.WriteLine("Puzzle 1 : " + Puzzle1(parsedContent));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(parsedContent));
        }

        public struct PasswordPolicy
        {
            public PasswordPolicy(string line)
            {
                var match = PasswordPolicy.pattern.Match(line);
                LowerBound = int.Parse(match.Groups[1].Value);
                HigherBound = int.Parse(match.Groups[2].Value);
                Letter = char.Parse(match.Groups[3].Value);
                Password = match.Groups[4].Value;
            }

            private static Regex pattern = new Regex(@"(\d+)-(\d+) ([a-z]): (\w+)");

            public string Password { get; }
            public int LowerBound { get; }
            public int HigherBound { get; }
            public char Letter { get; }
        }

        static int Puzzle1(PasswordPolicy[] policies)
        {
            int total = 0;
            foreach (var policy in policies)
            {
                if (IsPasswordValidPuzzle1(policy))
                {
                    total++;
                }
            }

            return total;
        }

        static bool IsPasswordValidPuzzle1(PasswordPolicy policy)
        {
            int charCount = 0;
            foreach (char c in policy.Password)
            {
                if (c == policy.Letter)
                {
                    charCount++;
                }
            }

            return charCount >= policy.LowerBound && charCount <= policy.HigherBound;
        }

        static int Puzzle2(PasswordPolicy[] policies)
        {
            int total = 0;
            foreach (var policy in policies)
            {
                if (IsPasswordValidPuzzle2(policy))
                {
                    total++;
                }
            }

            return total;
        }

        static bool IsPasswordValidPuzzle2(PasswordPolicy policy)
        {
            return (policy.LowerBound <= policy.Password.Length && policy.Password[policy.LowerBound - 1] == policy.Letter)
                != (policy.HigherBound <= policy.Password.Length && policy.Password[policy.HigherBound - 1] == policy.Letter);
        }
    }
}

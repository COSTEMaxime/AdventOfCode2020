using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_04
{
    class Program
    {
        static string[] fields = { "byr", "iyr", "eyr", "hgt", "hcl", "pid", "ecl" };
        static IDictionary<string, Func<string, bool>> validators = new Dictionary<string, Func<string, bool>>() {
            { "byr", BirthValidator },
            { "iyr", IssueYearValidator },
            { "eyr", ExpirationValidator },
            { "hgt", HeightValidator },
            { "hcl", HairColorValidator },
            { "ecl", EyeColorValidator},
            { "pid", PassportValidator},
            { "cid", (x) => true }
        };


        static void Main(string[] args)
        {
            string content = System.IO.File.ReadAllText(@"input.txt");
            string[] parsedContent = content.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            Console.WriteLine("Puzzle 1 : " + Puzzle1(parsedContent));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(parsedContent));
        }

        static int Puzzle1(string[] input)
        {
            int validPassportCount = 0;

            foreach (var line in input)
            {
                if (IsPasswordValidPuzzle1(line))
                {
                    validPassportCount++;
                }
            }

            return validPassportCount;
        }

        static bool IsPasswordValidPuzzle1(string input)
        {
            string[] tokens = input.Replace("\r\n", " ").Split(' ');
            string[] parsedTokens = new string[tokens.Length];

            if (tokens.Length < 7)
            {
                return false;
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                parsedTokens[i] = tokens[i].Remove(3, tokens[i].Length - 3);
            }

            foreach (var field in fields)
            {
                if (!parsedTokens.Any(field.Contains))
                {
                    return false;
                }
            }

            return true;
        }

        static int Puzzle2(string[] input)
        {
            int validPassportCount = 0;

            foreach (var line in input)
            {
                if (IsPasswordValidPuzzle2(line))
                {
                    validPassportCount++;
                }
            }

            return validPassportCount;
        }

        static bool IsPasswordValidPuzzle2(string input)
        {
            string[] tokens = input.Replace("\r\n", " ").Split(' ');
            string[] parsedTokens = new string[tokens.Length];

            if (tokens.Length < 7)
            {
                return false;
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                parsedTokens[i] = tokens[i].Remove(3, tokens[i].Length - 3);
            }

            foreach (var field in fields)
            {
                if (!parsedTokens.Any(field.Contains))
                {
                    return false;
                }
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                if (!validators[parsedTokens[i]](tokens[i].Split(':')[1]))
                {
                    return false;
                }
            }

            return true;
        }

        static bool BirthValidator(string value)
        {
            try
            {
               int parsed = int.Parse(value);
                return parsed <= 2002 && parsed >= 1920;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static bool IssueYearValidator(string value)
        {
            try
            {
                int parsed = int.Parse(value);
                return parsed <= 2020 && parsed >= 2010;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static bool ExpirationValidator(string value)
        {
            try
            {
                int parsed = int.Parse(value);
                return parsed <= 2030 && parsed >= 2020;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static bool HeightValidator(string value)
        {
            Regex pattern = new Regex(@"^(\d+)(\w+)$");
            var match = pattern.Match(value);
            if (match.Success)
            {
                int parsed = int.Parse(match.Groups[1].Value);
                if (match.Groups[2].Value == "cm")
                {
                    return parsed <= 193 && parsed >= 150;
                }
                else if (match.Groups[2].Value == "in")
                {
                    return parsed <= 76 && parsed >= 59;
                }
            }

            return false;
        }

        static bool HairColorValidator(string value)
        {
            Regex pattern = new Regex(@"^#([a-fA-F0-9]{6})$");
            var match = pattern.Match(value);
            return match.Success;
        }

        static bool EyeColorValidator(string value)
        {
            string[] values = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return values.Any(value.Contains);
        }

        static bool PassportValidator(string value)
        {
            if (value.Length != 9)
            {
                return false;
            }

            try
            {
                int parsed = int.Parse(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_14
{
    class Program
    {
        static readonly int MASK_SIZE = 36;

        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");

            Console.WriteLine("Puzzle 1 : " + Puzzle1(content));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(content));
        }

        static long Puzzle1(string[] input)
        {
            IDictionary<int, char[]> memory = new Dictionary<int, char[]>();
            string mask = "";

            foreach(var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(' ')[2];
                }
                else
                {
                    Regex r = new Regex(@"^mem\[(\d+)\] = (\d+)$");
                    var matches = r.Match(line);

                    int key = int.Parse(matches.Groups[1].Value);
                    char[] newMemory = getMemoryValue1(matches.Groups[2].Value, mask);

                    if  (memory.ContainsKey(key)) { memory[key] = newMemory; }
                    else { memory.Add(key, newMemory); }
                }
            }

            long count = 0;
            foreach (var kvp in memory)
            {
                count += Convert.ToInt64(new string(kvp.Value), 2);
            }

            return count;
        }

        static char[] getMemoryValue1(string rawValue, string mask)
        {
            int value = int.Parse(rawValue);

            string binary = Convert.ToString(value, 2);

            char[] newMemory = new char[MASK_SIZE];
            for (int i = 0; i < MASK_SIZE; i++)
            {
                if (mask[i] != 'X')
                {
                    newMemory[i] = mask[i];
                }
                else if (i < MASK_SIZE - binary.Length)
                {
                    newMemory[i] = '0';
                }
                else
                {
                    newMemory[i] = binary[i - (MASK_SIZE - binary.Length)];
                }
            }

            return newMemory;
        }

        static long Puzzle2(string[] input)
        {
            IDictionary<string, int> memory = new Dictionary<string, int>();
            string mask = "";

            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(' ')[2];
                }
                else
                {
                    Regex r = new Regex(@"^mem\[(\d+)\] = (\d+)$");
                    var matches = r.Match(line);

                    string key = new string(getMemoryValue2(matches.Groups[1].Value, mask));
                    int value = int.Parse(matches.Groups[2].Value);

                    var addresses = generateAddresses(key);

                    foreach (var address in addresses)
                    {
                        if (memory.ContainsKey(address)) { memory[address] = value; }
                        else { memory.Add(address, value); }
                    }

                }
            }

            long count = 0;
            foreach (var kvp in memory)
            {
                count += kvp.Value;
            }

            return count;
        }

        static char[] getMemoryValue2(string rawValue, string mask)
        {
            int value = int.Parse(rawValue);

            string binary = Convert.ToString(value, 2);

            char[] newMemory = new char[MASK_SIZE];
            for (int i = 0; i < MASK_SIZE; i++)
            {
                if (mask[i] == 'X' || mask[i] == '1')
                {
                    newMemory[i] = mask[i];
                }
                else if (i < MASK_SIZE - binary.Length)
                {
                    newMemory[i] = '0';
                }
                else
                {
                    newMemory[i] = binary[i - (MASK_SIZE - binary.Length)];
                }
            }

            return newMemory;
        }

        static List<string> generateAddresses(string existingAddress)
        {
            List<string> newAddresses = new List<string>();
            int index = existingAddress.IndexOf('X');
            if (index != -1)
            {
                StringBuilder builder = new StringBuilder(existingAddress);
                builder[index] = '0';
                string low = builder.ToString();

                builder[index] = '1';
                string high = builder.ToString();

                if (index != existingAddress.LastIndexOf('X'))
                {
                    newAddresses.AddRange(generateAddresses(low));
                    newAddresses.AddRange(generateAddresses(high));
                }
                else
                {
                    newAddresses.Add(low);
                    newAddresses.Add(high);
                }
            }
            else
            {
                newAddresses.Add(existingAddress);
            }

            return newAddresses;
        }
    }
}

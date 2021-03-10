using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_07
{
    class Program
    {
        static IDictionary<string, Bag> bags = new Dictionary<string, Bag>();
        static void Main(string[] args)
        {
            string content = System.IO.File.ReadAllText(@"input.txt");
            ParseContent(content);

            Console.WriteLine("Puzzle 1 : " + Puzzle1());
            Console.WriteLine("Puzzle 2 : " + Puzzle2());
        }


        public class Bag
        {
            public string Name { get; }
            private IList<Tuple<string, int>> childrenBagsData;
            private IList<Tuple<Bag, int>> ChildrenBags;
            private bool? _cache = null;

            public Bag(string line)
            {
                childrenBagsData = new List<Tuple<string, int>>();
                ChildrenBags = new List<Tuple<Bag, int>>();

                var patternParent = new Regex(@"^(.+) bags contain (.+).$");
                var matchesParent = patternParent.Match(line);
                Name = matchesParent.Groups[1].Value;

                if (matchesParent.Groups[2].Value == "no other bags") { return; }

                var tokens = matchesParent.Groups[2].Value.Split(',');
                var patternChildren = new Regex(@"(\d+) (.+) bag(s?)");
                foreach (var token in tokens)
                {
                    var matchesChildren = patternChildren.Match(token);
                    childrenBagsData.Add(new Tuple<string, int>(matchesChildren.Groups[2].Value, int.Parse(matchesChildren.Groups[1].Value)));
                }
            }

            public void UpdateBagsReferences()
            {
                foreach (var kvp in childrenBagsData)
                {
                    ChildrenBags.Add(new Tuple<Bag, int>(bags[kvp.Item1], kvp.Item2));
                }
            }

            public bool ContainsYellowBag()
            {
                if (_cache != null) { return (bool)_cache; }
                if (Name == "shiny gold") { return true; }

                foreach (var kvp in ChildrenBags)
                {
                    if (kvp.Item1.ContainsYellowBag()) { return true; }
                }

                return false;
            }

            public int GetBagsCapacity()
            {
                if (ChildrenBags.Count == 0) { return 1; }

                // return self + children capacity
                int total = 1;
                foreach (var kvp in ChildrenBags)
                {
                    total += kvp.Item1.GetBagsCapacity() * kvp.Item2;
                }

                return total;
            }
        }

        static void ParseContent(string content)
        {
            string[] parsedContent = content.Split(new[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in parsedContent)
            {
                var bag = new Bag(line);
                bags.Add(bag.Name, bag);
            }

            foreach (var keyValue in bags)
            {
                keyValue.Value.UpdateBagsReferences();
            }
        }

        static int Puzzle1()
        {
            int count = 0;
            foreach (var kvp in bags)
            {
                if (kvp.Value.ContainsYellowBag())
                {
                    count ++;
                }
            }

            // -1 because we don't count the shiny gold bag in the total
            return count - 1;
        }

        static int Puzzle2()
        {
            // again, don't count the shiny gold bag
            return bags["shiny gold"].GetBagsCapacity() - 1;
        }
    }
}

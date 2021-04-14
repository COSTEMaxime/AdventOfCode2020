using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_16
{
    class Solver
    {
        List<Rule> rules;
        List<Ticket> tickets;
        Ticket myTicket;

        public Solver(string[] input)
        {
            rules = new List<Rule>();
            tickets = new List<Ticket>();

            Parse(input);
        }

        void Parse(string[] input)
        {
            foreach (var rawRule in input[0].Split(new[] { "\r\n" }, StringSplitOptions.None))
            {
                rules.Add(new Rule(rawRule));
            }

            myTicket = new Ticket(input[1].Split(new[] { "\r\n" }, StringSplitOptions.None)[1]);

            var nearbyTickets = input[2].Split(new[] { "\r\n" }, StringSplitOptions.None);
            for (int i = 1; i < nearbyTickets.Length; i++)
            {
                tickets.Add(new Ticket(nearbyTickets[i]));
            }
        }

        public int Puzzle1()
        {
            List<int> invalidValues = new List<int>();
            foreach (var ticket in tickets)
            {
                invalidValues.AddRange(GetInvalidValues(ticket.values, rules));
            }

            return invalidValues.Sum();
        }

        public long Puzzle2()
        {
            tickets = tickets.Where(t => GetInvalidValues(t.values, rules).Count() == 0).ToList();

            Rule[] sortedRules = new Rule[tickets[0].values.Count()];
            while (rules.Count() > 0)
            {
                for (int i = 0; i < tickets[0].values.Count(); i++)
                {
                    var validRulesIndices = new List<int>();
                    for (int j = 0; j < rules.Count(); j++)
                    {
                        bool isRuleValid = true;
                        foreach (var ticket in tickets)
                        {
                            long curr = ticket.values[i];
                            if (curr < rules[j].lower1 || curr > rules[j].upper2 || (curr > rules[j].upper1 && curr < rules[j].lower2))
                            {
                                isRuleValid = false;
                                break;
                            }
                        }

                        if (isRuleValid) { validRulesIndices.Add(j); }
                    }

                    // if there are multiple valids rules, skip
                    if (validRulesIndices.Count() == 1)
                    {
                        sortedRules[i] = (rules[validRulesIndices[0]]);
                        rules.RemoveAt(validRulesIndices[0]);
                    }
                }
            }

            long result = 1;
            for (int i = 0; i < sortedRules.Count(); i++)
            {
                if (sortedRules[i].name.StartsWith("departure"))
                {
                    result *= myTicket.values[i];
                }
            }

            return result;
        }

        private static IEnumerable<int> GetInvalidValues(List<long> values, List<Rule> rules)
        {
            List<int> invalidValues = new List<int>();

            foreach (int value in values)
            {
                bool isValid = false;
                foreach (Rule rule in rules)
                {
                    if ((value >= rule.lower1 && value <= rule.upper1) || (value >= rule.lower2 && value <= rule.upper2))
                    {
                        isValid = true;
                    }
                }
                if (!isValid) { invalidValues.Add(value); }
            }

            return invalidValues;
        }
    }

    class Ticket
    {
        public List<long> values { get; set; }

        public Ticket(string input)
        {
            values = new List<long>();

            foreach (var token in input.Split(','))
            {
                values.Add(long.Parse(token));
            }
        }
    }

    class Rule
    {
        public string name { get; set; }
        public int lower1 { get; set; }
        public int upper1 { get; set; }
        public int lower2 { get; set; }
        public int upper2 { get; set; }

        public Rule(string input)
        {
            Regex r = new Regex(@"^(\D+): (\d+)-(\d+) or (\d+)-(\d+)$");
            var tokens = r.Match(input);
            name = tokens.Groups[1].Value;
            lower1 = int.Parse(tokens.Groups[2].Value);
            upper1 = int.Parse(tokens.Groups[3].Value);
            lower2 = int.Parse(tokens.Groups[4].Value);
            upper2 = int.Parse(tokens.Groups[5].Value);
        }
    }
}

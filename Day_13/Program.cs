using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");
            int earliestDepartureTime = int.Parse(content[0]);

            Console.WriteLine("Puzzle 1 : " + Puzzle1(earliestDepartureTime, content[1]));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(earliestDepartureTime, content[1]));
        }

        static int Puzzle1(int earliestDepartureTime, string busses)
        {
            string[] tokens = busses.Replace("x,", "").Split(',');
            int min = int.MaxValue;
            int answer = -1;

            for (int i = 0; i < tokens.Length; i++)
            {
                int currentTimestamp = int.Parse(tokens[i]);
                int departureTime = (int)(earliestDepartureTime - Math.Ceiling((double)earliestDepartureTime / currentTimestamp) * currentTimestamp) * -1;
                if (departureTime < min)
                {
                    min = departureTime;
                    answer = currentTimestamp * departureTime;
                }
            }

            return answer;
        }

        static long Puzzle2(int earliestDepartureTime, string busses)
        {
            // hint 1 : all numbers are prime !
            // I need to look about that Chinese Remainder Theorem...

            int size = busses.Replace("x,", "").Split(',').Length;
            long[] num = new long[size];
            long[] rem = new long[size];

            var tokens = busses.Split(',');
            int current = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] != "x")
                {
                    num[current] = long.Parse(tokens[i]);
                    rem[current] = i;
                    current++;
                }
            }

            return ChineseRemainder(num, rem);
        }

        static long ChineseRemainder(long[] num, long[] rem)
        {
            // N = n1 * n2 * n3 * ... * nk
            long productN = num.Aggregate(1L, (j, i) => i * j);
            long sum = 0;

            // for each i = 1, 2, 3, ..., k
            for (int i = 0; i < num.Length; i++)
            {
                // y_i = N / n_i
                long p = productN / num[i];

                // sum += a_i * z_i * y_i
                // where z_i = y_i^-1
                sum += rem[i] * ModularMultiplicativeInverse(p, num[i]) * p;
            }

            return productN - (sum % productN);
        }

        static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");

            SeatLayoutSolver solver = new SeatLayoutSolver(content);

            Console.WriteLine("Puzzle 1 : " + Puzzle1(solver));
            Console.WriteLine("Puzzle 2 : " + Puzzle2(solver));
        }

        static int Puzzle1(SeatLayoutSolver solver)
        {
            solver.Puzzle1Setup();
            solver.Solve();
            return solver.GetOccupiedSeatsCount();
        }

        static int Puzzle2(SeatLayoutSolver solver)
        {
            solver.Reset();
            solver.Puzzle2Setup();
            solver.Solve();
            return solver.GetOccupiedSeatsCount();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = System.IO.File.ReadAllLines(@"input.txt");


            Console.WriteLine("Puzzle 1 : " + Puzzle1(content));
            //Console.WriteLine("Puzzle 2 : " + Puzzle2(content));
        }

        static int Puzzle1(string[] input)
        {
            // start facing east
            Vector orientation = new Vector(1, 0);
            Vector position = new Vector(0, 0);
            foreach (var line in input)
            {
                char action = line[0];
                int value = int.Parse(line.Substring(1));

                switch (action)
                {
                    case 'N':
                        position.Y -= value;
                        break;
                    case 'S':
                        position.Y += value;
                        break;
                    case 'E':
                        position.X += value;
                        break;
                    case 'W':
                        position.X -= value;
                        break;
                    case 'L':
                        orientation = getOrientationVectorLeft(orientation, value);
                        break;
                    case 'R':
                        orientation = getOrientationVectorRight(orientation, value);
                        break;
                    case 'F':
                        position.X += orientation.X * value;
                        position.Y += orientation.Y * value;
                        break;
                }
            }

            return (int)(Math.Abs(position.X) + (int)Math.Abs(position.Y));
        }

        private static Vector getOrientationVectorLeft(Vector orientation, int value)
        {
            var newOrientation = new Vector(orientation.X, orientation.Y);
            for (int i = 0; i < (value / 90); i++)
            {
                newOrientation = new Vector(-newOrientation.Y, -newOrientation.X);
            }

            return newOrientation;
        }

        private static Vector getOrientationVectorRight(Vector orientation, int value)
        {
            var newOrientation = new Vector(orientation.X, orientation.Y);
            for (int i = 0; i < (value / 90); i++)
            {
                newOrientation = new Vector(newOrientation.Y, -newOrientation.X);
            }

            return newOrientation;

            //int offset = (value / 90) % 4;
            //if (offset % 2 == 0 && newOrientation.X != 0)
            //{
            //    newOrientation.X = -newOrientation.X;
            //}

            //if ((offset + 1) % 2 == 0 && newOrientation.Y == 0)
            //{
            //    newOrientation.Y = newOrientation.X;
            //}

            //for (int i = 0; i < offset; i++)
            //{
            //    int tempX = (int)newOrientation.X;
            //    if (newOrientation.X == 0) { newOrientation.X = newOrientation.Y; }
            //    else { newOrientation.X = 0; }

            //    if (newOrientation.Y == 0) { newOrientation.Y = tempX; }
            //    else { newOrientation.Y = 0; }
            //}
        }

        //static int Puzzle2(SeatLayoutSolver solver)
        //{
        //    solver.Reset();
        //    solver.Puzzle2Setup();
        //    solver.Solve();
        //    return solver.GetOccupiedSeatsCount();
        //}
    }
}

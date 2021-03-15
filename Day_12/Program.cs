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
            Console.WriteLine("Puzzle 2 : " + Puzzle2(content));
        }

        enum Direction
        {
            UP = 0,
            RIGHT,
            DOWN,
            LEFT
        }

        static int Puzzle1(string[] input)
        {
            // start facing east
            Direction direction = Direction.RIGHT;
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
                        direction = getNewDirectionLeft(direction, value);
                        break;
                    case 'R':
                        direction = getNewDirectionRight(direction, value);
                        break;
                    case 'F':
                        switch (direction)
                        {
                            case Direction.RIGHT:
                                position.X += value;
                                break;
                            case Direction.DOWN:
                                position.Y += value;
                                break;
                            case Direction.LEFT:
                                position.X -= value;
                                break;
                            case Direction.UP:
                                position.Y -= value;
                                break;
                        }
                        break;
                }
            }

            return (int)(Math.Abs(position.X) + (int)Math.Abs(position.Y));
        }

        private static Direction getNewDirectionLeft(Direction direction, int value)
        {
            return (Direction)((((int)direction - (value / 90)) % 4 + 4) % 4);
        }

        private static Direction getNewDirectionRight(Direction direction, int value)
        {
            return (Direction)(((int)direction + (value / 90)) % 4);
        }

        static int Puzzle2(string[] input)
        {
            Vector position = new Vector(0, 0);
            Vector waypoint = new Vector(10, -1);
            foreach (var line in input)
            {
                char action = line[0];
                int value = int.Parse(line.Substring(1));

                switch (action)
                {
                    case 'N':
                        waypoint.Y -= value;
                        break;
                    case 'S':
                        waypoint.Y += value;
                        break;
                    case 'E':
                        waypoint.X += value;
                        break;
                    case 'W':
                        waypoint.X -= value;
                        break;
                    case 'L':
                        waypoint = rotateVectorLeft(waypoint, value);
                        break;
                    case 'R':
                        waypoint = rotateVectorAntiClockwise(waypoint, value);
                        break;
                    case 'F':
                        position += value * waypoint;
                        break;
                }

                Console.WriteLine(action + " " + value + " : " + position.ToString() + " --- " + waypoint.ToString());
            }

            return (int)(Math.Abs(position.X) + (int)Math.Abs(position.Y));
        }

        static Vector rotateVectorAntiClockwise(Vector initial, int angle)
        {
            var newVector = new Vector(initial.X, initial.Y);
            for (int i = 0; i < (angle / 90); i++)
            {
                newVector = new Vector(-newVector.Y, newVector.X);
            }

            return newVector;
        }

        static Vector rotateVectorLeft(Vector initial, int angle)
        {
            return rotateVectorAntiClockwise(initial, 360 - angle);
        }
    }
}

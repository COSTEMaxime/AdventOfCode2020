using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_17
{
    class ConwayCube
    {
        string[] input;

        public ConwayCube(string[] input)
        {
            this.input = input;
        }

        public int Puzzle1()
        {
            var cells = new Dictionary<(int x, int y, int z), bool>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    cells[(i, j, 0)] = input[i][j] == '#';
                }
            }

            var neighborsCount = new Dictionary<(int x, int y, int z), int>();
            for (var i = 0; i < 6; i++)
            {
                neighborsCount.Clear();
                foreach (var kvp in cells)
                {
                    neighborsCount[kvp.Key] = 0;
                }

                foreach (var (x, y, z) in cells.Where(kvp => kvp.Value).Select(kvp => kvp.Key))
                {
                    foreach (var (dx, dy, dz) in neighborsCoords3D())
                    {
                        int count = neighborsCount.ContainsKey((x + dx, y + dy, z + dz)) ? neighborsCount[(x + dx, y + dy, z + dz)] : 0;
                        neighborsCount[(x + dx, y + dy, z + dz)] = count + 1;
                    }
                }

                foreach (var (cell, count) in neighborsCount.Select(kvp => (kvp.Key, kvp.Value)))
                {
                    cells[cell] = (cells.ContainsKey(cell) && cells[cell] == true && (count == 2 || count == 3))
                        || count == 3;
                }
            }

            return cells.Values.Count(x => x);
        }

        public int Puzzle2()
        {
            var cells = new Dictionary<(int x, int y, int z, int w), bool>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    cells[(i, j, 0, 0)] = input[i][j] == '#';
                }
            }

            var neighborsCount = new Dictionary<(int x, int y, int z, int w), int>();
            for (var i = 0; i < 6; i++)
            {
                neighborsCount.Clear();
                foreach (var kvp in cells)
                {
                    neighborsCount[kvp.Key] = 0;
                }

                foreach (var (x, y, z, w) in cells.Where(kvp => kvp.Value).Select(kvp => kvp.Key))
                {
                    foreach (var (dx, dy, dz, dw) in neighborsCoords4D())
                    {
                        int count = neighborsCount.ContainsKey((x + dx, y + dy, z + dz, w + dw)) ? neighborsCount[(x + dx, y + dy, z + dz, w + dw)] : 0;
                        neighborsCount[(x + dx, y + dy, z + dz, w + dw)] = count + 1;
                    }
                }

                foreach (var (cell, count) in neighborsCount.Select(kvp => (kvp.Key, kvp.Value)))
                {
                    cells[cell] = (cells.ContainsKey(cell) && cells[cell] == true && (count == 2 || count == 3))
                        || count == 3;
                }
            }

            return cells.Values.Count(x => x);
        }

        private static IEnumerable<(int x, int y, int z)> neighborsCoords3D()
        {
            return Enumerable.Range(-1, 3)
                    .SelectMany(x => Enumerable.Range(-1, 3)
                        .SelectMany(y => Enumerable.Range(-1, 3)
                            .Select(z => (x, y, z))))
                                // exclude self
                                .Where(coords => coords != (0, 0, 0));
        }

        private static IEnumerable<(int x, int y, int z, int w)> neighborsCoords4D()
        {
            return Enumerable.Range(-1, 3)
                    .SelectMany(x => Enumerable.Range(-1, 3)
                        .SelectMany(y => Enumerable.Range(-1, 3)
                            .SelectMany(z => Enumerable.Range(-1, 3)
                            .Select(w => (x, y, z, w)))))
                                // exclude self
                                .Where(coords => coords != (0, 0, 0, 0));
        }
    }
}

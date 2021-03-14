using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11
{
    delegate int SeatCountBehaviour(int i, int j);

    class SeatLayoutSolver
    {
        private string[] data;
        private char[,] layout;
        private int occupiedSeatsThreeshold;
        private SeatCountBehaviour seatCountBehaviour;

        public SeatLayoutSolver(string[] data)
        {
            this.data = data;
            LoadLayout(data);
        }

        private void LoadLayout(string[] data)
        {
            layout = new char[data.Length, data[0].Length];

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    layout[i, j] = data[i][j];
                }
            }
        }

        public void Reset()
        {
            LoadLayout(data);
        }

        public void Puzzle1Setup()
        {
            occupiedSeatsThreeshold = 4;
            seatCountBehaviour = new SeatCountBehaviour(GetOccupiedAdjacentSeatsCount);
        }

        public void Puzzle2Setup()
        {
            occupiedSeatsThreeshold = 5;
            seatCountBehaviour = new SeatCountBehaviour(GetOccupiedLineSeatsCount);
        }

        public void Solve()
        {
            char[,] newLayout = (char[,])layout.Clone();
            do
            {
                layout = (char[,])newLayout.Clone();
                for (int i = 0; i < layout.GetLength(0); i++)
                {
                    for (int j = 0; j < layout.GetLength(1); j++)
                    {
                        int occupiedAdjacentSeatsCount = seatCountBehaviour(i, j);
                        if (layout[i, j] == 'L' && occupiedAdjacentSeatsCount == 0)
                        {
                            newLayout[i, j] = '#';
                        }
                        else if (layout[i, j] == '#' && occupiedAdjacentSeatsCount >= occupiedSeatsThreeshold)
                        {
                            newLayout[i, j] = 'L';
                        }
                    }
                }
            }
            while (
            (
                Enumerable.Range(0, layout.Rank).All(dimension => layout.GetLength(dimension) == newLayout.GetLength(dimension))
                && layout.Cast<char>().SequenceEqual(newLayout.Cast<char>())
            ) == false);
        }

        private int GetOccupiedAdjacentSeatsCount(int i, int j)
        {
            int occupiedSeatsCount = 0;

            if (IsInbound(i - 1, j - 1) && IsSeatOccupied(i - 1, j - 1)) { occupiedSeatsCount++; }
            if (IsInbound(i - 1, j + 0) && IsSeatOccupied(i - 1, j + 0)) { occupiedSeatsCount++; }
            if (IsInbound(i - 1, j + 1) && IsSeatOccupied(i - 1, j + 1)) { occupiedSeatsCount++; }
            if (IsInbound(i + 0, j - 1) && IsSeatOccupied(i + 0, j - 1)) { occupiedSeatsCount++; }
            if (IsInbound(i + 0, j + 1) && IsSeatOccupied(i + 0, j + 1)) { occupiedSeatsCount++; }
            if (IsInbound(i + 1, j - 1) && IsSeatOccupied(i + 1, j - 1)) { occupiedSeatsCount++; }
            if (IsInbound(i + 1, j + 0) && IsSeatOccupied(i + 1, j + 0)) { occupiedSeatsCount++; }
            if (IsInbound(i + 1, j + 1) && IsSeatOccupied(i + 1, j + 1)) { occupiedSeatsCount++; }

            return occupiedSeatsCount;
        }

        private int GetOccupiedLineSeatsCount(int i, int j)
        {
            int occupiedSeatsCount = 0;

            if (LineHasOccupiedSeat(i, j, -1, -1)) { occupiedSeatsCount++; }
            if (LineHasOccupiedSeat(i, j, -1, +0)) { occupiedSeatsCount++; }
            if (LineHasOccupiedSeat(i, j, -1, +1)) { occupiedSeatsCount++; }
            if (LineHasOccupiedSeat(i, j, +0, -1)) { occupiedSeatsCount++; }
            if (LineHasOccupiedSeat(i, j, +0, +1)) { occupiedSeatsCount++; }
            if (LineHasOccupiedSeat(i, j, +1, -1)) { occupiedSeatsCount++; }
            if (LineHasOccupiedSeat(i, j, +1, +0)) { occupiedSeatsCount++; }
            if (LineHasOccupiedSeat(i, j, +1, +1)) { occupiedSeatsCount++; }

            return occupiedSeatsCount;
        }

        private bool LineHasOccupiedSeat(int i, int j, int iStep, int jStep)
        {
            int currentI = i + iStep;
            int currentJ = j + jStep;
            while (IsInbound(currentI, currentJ))
            {
                if (IsSeatNotEmpty(currentI, currentJ)) { return IsSeatOccupied(currentI, currentJ); }
                currentI += iStep;
                currentJ += jStep;
            }

            return false;
        }

        private bool IsInbound(int i, int j)
        {
            return (i >= 0 && j >= 0 && i < layout.GetLength(0) && j < layout.GetLength(1));
        }

        private bool IsSeatOccupied(int i, int j)
        {
            return layout[i, j] == '#';
        }

        private bool IsSeatNotEmpty(int i, int j)
        {
            return layout[i, j] != '.';
        }

        public int GetOccupiedSeatsCount()
        {
            int occupiedSeatsCount = 0;
            foreach (var element in layout)
            {
                if (element == '#')
                {
                    occupiedSeatsCount++;
                }
            }

            return occupiedSeatsCount;
        }
    }
}

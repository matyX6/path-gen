using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Array2DPath
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            char[,] pathArray = CreatePathArray();
            WriteArray2DInConsole(pathArray);
        }

        private static char[,] CreatePathArray()
        {
            char[,] pathArray = new char[9, 115];
            PopulateArray2D(pathArray);

            Random random = new Random();
            int newIndex = random.Next(5, 9);
            
            // assign path
            for (int y = 0; y < pathArray.GetLength(1); y++)
            {
                pathArray[newIndex, y] = 'O';
                pathArray[newIndex - 1, y] = 'O';
                pathArray[newIndex - 2, y] = 'O';
                pathArray[newIndex - 3, y] = 'O';
                pathArray[newIndex - 4, y] = 'O';

                if (y % 2 == 1)
                    newIndex = random.Next(Math.Max(newIndex - 1, 5), Math.Min(newIndex + 2, 9));
            }
            
            // fit blocks
            for (int y = 0; y < pathArray.GetLength(0); y++)
            {
                for (int x = 0; x < pathArray.GetLength(1); x++)
                {
                    int rand = random.Next(0, 4);

                    if (rand == 0 && FitBigBlock(y, x, pathArray))
                        continue;

                    if (rand == 1 && FitHorizontalBlock(y, x, pathArray))
                        continue;

                    if (rand == 2 && FitVerticalBlock(y, x, pathArray))
                        continue;

                    FitBlock(y, x, pathArray);
                }
            }

            return pathArray;
        }
    
        private static void FitBlock(int y, int x, char[,] array2D)
        {
            if (array2D[y, x] != '-')
            {
                return;
            }
            
            array2D[y, x] = '1';
        }

        private static bool FitHorizontalBlock(int y, int x, char[,] array2D)
        {
            if (array2D[y, x] != '-')
            {
                return false;
            }

            if (x + 1 == array2D.GetLength(1) || array2D[y, x + 1] != '-')
            {
                return false;
            }
        
            array2D[y, x] = '2';
            array2D[y, x + 1] = '2';
        
            return true;
        }
    
        private static bool FitVerticalBlock(int y, int x, char[,] array2D)
        {
            if (array2D[y, x] != '-')
            {
                return false;
            }

            if (y + 1 == array2D.GetLength(0) || array2D[y + 1, x] != '-')
            {
                return false;
            }
        
            array2D[y, x] = '3';
            array2D[y + 1, x] = '3';
        
            return true;
        }
        
        private static bool FitBigBlock(int y, int x, char[,] array2D)
        {
            if (array2D[y, x] != '-')
            {
                return false;
            }

            if (y + 1 == array2D.GetLength(0) || array2D[y + 1, x] != '-')
            {
                return false;
            }

            if (x + 1 == array2D.GetLength(1) || array2D[y, x + 1] != '-')
            {
                return false;
            }

            if (y + 1 == array2D.GetLength(0) || x + 1 == array2D.GetLength(1) || array2D[y + 1, x + 1] != '-')
            {
                return false;
            }

            array2D[y, x] = '4';
            array2D[y + 1, x] = '4';
            array2D[y, x + 1] = '4';
            array2D[y + 1, x + 1] = '4';

            return true;
        }

        private static void PopulateArray2D(char[,] array2D)
        {
            for (int x = 0; x < array2D.GetLength(0); x++)
            {
                for (int y = 0; y < array2D.GetLength(1); y++)
                {
                    array2D[x, y] = '-';
                }
            }
        }

        public static void WriteArray2DInConsole(char[,] array2D)
        {
            // write line
            for (int x = 0; x < array2D.GetLength(0); x++)
            {
                for (int y = 0; y < array2D.GetLength(1); y++)
                {
                    if (y == 0)
                    {
                        Console.WriteLine();
                    }

                    var colorMap = new Dictionary<char, ConsoleColor>
                    {
                        { 'O', ConsoleColor.Yellow },
                        { '1', ConsoleColor.DarkCyan },
                        { '2', ConsoleColor.Cyan },
                        { '3', ConsoleColor.Blue },
                        { '4', ConsoleColor.DarkBlue }
                    };

                    if (colorMap.TryGetValue(array2D[x, y], out var color))
                    {
                        Console.ForegroundColor = color;
                        Console.Write(array2D[x, y]);
                    }

                    DelayNextIteration();
                }
            }
        }

        private static void DelayNextIteration()
        {
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedTicks < (Stopwatch.Frequency / 500)) { }
        }
    }
}
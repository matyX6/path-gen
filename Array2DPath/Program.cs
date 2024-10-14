using System;

namespace Array2DPath
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start path generation algorithm.");
            Console.ReadLine();
            
            char[,] pathArray = CreatePathArray();
            WriteArray2DInConsole(pathArray);
            
            Console.ReadLine();
        }

        private static char[,] CreatePathArray()
        {
            char[,] pathArray = new char[9, 160];
            PopulateArray2D(pathArray);

            Random random = new Random();
            int newIndex = random.Next(5, 9);
            
            // assign path
            for (int y = 0; y < pathArray.GetLength(1); y++)
            {
                pathArray[newIndex, y] = ' ';
                pathArray[newIndex - 1, y] = ' ';
                pathArray[newIndex - 2, y] = ' ';
                pathArray[newIndex - 3, y] = ' ';
                pathArray[newIndex - 4, y] = ' ';

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

            array2D[y, x] = 'X';
            array2D[y + 1, x] = 'X';
            array2D[y, x + 1] = 'X';
            array2D[y + 1, x + 1] = 'X';

            return true;
        }
    
        private static void FitBlock(int y, int x, char[,] array2D)
        {
            if (array2D[y, x] != '-')
            {
                return;
            }
            
            array2D[y, x] = 'X';
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
        
            array2D[y, x] = 'X';
            array2D[y, x + 1] = 'X';
        
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
        
            array2D[y, x] = 'X';
            array2D[y + 1, x] = 'X';
        
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
                    
                    Console.Write(array2D[x, y]);
                }
            }
        }
    }
}
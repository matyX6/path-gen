using System;

namespace Array2DPath
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            char[,] pathArray = new char[8, 160];
            PopulateArray2D(pathArray);

            Random random = new Random();
            int newIndex = random.Next(2, 8);
            
            // assign path
            for (int y = 0; y < pathArray.GetLength(1); y++)
            {
                pathArray[newIndex, y] = 'X';
                pathArray[newIndex - 1, y] = 'X';
                pathArray[newIndex - 2, y] = 'X';
                
                if (y % 2 == 1)
                    newIndex = random.Next(Math.Max(newIndex - 1, 2), Math.Min(newIndex + 2, 8));
            }
            
            for (int x = 0; x < pathArray.GetLength(0); x++)
                for (int y = 0; y < pathArray.GetLength(1); y++)
                {
                    if (pathArray[x, y] != '-')
                        continue;
                    
                    if (x + 1 == pathArray.GetLength(0 ) || pathArray[x + 1, y] != '-')
                        continue;
                    
                    if (y + 1 == pathArray.GetLength(1 ) || pathArray[x, y + 1] != '-')
                        continue;
                    
                    if (x + 1 == pathArray.GetLength(0 ) || 
                        y + 1 == pathArray.GetLength(1 ) || 
                        pathArray[x + 1, y + 1] != '-')
                        continue;

                    pathArray[x, y] = '|';
                    pathArray[x + 1, y] = '|';
                    pathArray[x, y + 1] = '|';
                    pathArray[x + 1, y + 1] = '|';
                }

            WriteArray2DInConsole(pathArray);
        }

        public static void PopulateArray2D(char[,] array2D)
        {
            for (int x = 0; x < array2D.GetLength(0); x++)
                for (int y = 0; y < array2D.GetLength(1); y++)
                    array2D[x, y] = '-';
        }

        public static void WriteArray2DInConsole(char[,] array2D)
        {
            // write line
            for (int x = 0; x < array2D.GetLength(0); x++)
            {
                for (int y = 0; y < array2D.GetLength(1); y++)
                {
                    if (y == 0)
                        Console.WriteLine();
                    
                    Console.Write(array2D[x, y]);
                }
            }
        }
    }
}
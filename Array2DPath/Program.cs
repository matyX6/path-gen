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
                pathArray[newIndex, y] = '°';
                pathArray[newIndex - 1, y] = '°';
                pathArray[newIndex - 2, y] = '°';
                
                if (y % 2 == 1)
                    newIndex = random.Next(Math.Max(newIndex - 1, 2), Math.Min(newIndex + 2, 8));
            }
            
            // big block
            for (int x = 0; x < pathArray.GetLength(0); x++)
                for (int y = 0; y < pathArray.GetLength(1); y++)
                {
                    int rand = random.Next(0, 3);
                    
                    if (rand == 0 && FitBigBlock(x, y, pathArray))
                        continue;
                    
                    if (rand == 1 && FitHorizontalBlock(x, y, pathArray))
                        continue;
                    
                    if (rand == 2 && FitVerticalBlock(x, y, pathArray))
                        continue;
                    
                    FitBlock(x, y, pathArray);
                }

            WriteArray2DInConsole(pathArray);
        }

        public static bool FitBigBlock(int x, int y, char[,] array2D)
        {
            if (array2D[x, y] != '-')
                return false;
                    
            if (x + 1 == array2D.GetLength(0 ) || array2D[x + 1, y] != '-')
                return false;
                    
            if (y + 1 == array2D.GetLength(1 ) || array2D[x, y + 1] != '-')
                return false;
                    
            if (x + 1 == array2D.GetLength(0 ) || 
                y + 1 == array2D.GetLength(1 ) || 
                array2D[x + 1, y + 1] != '-')
                return false;

            array2D[x, y] = '■';
            array2D[x + 1, y] = '■';
            array2D[x, y + 1] = '■';
            array2D[x + 1, y + 1] = '■';

            return true;
        }
        
        public static void FitBlock(int x, int y, char[,] array2D)
        {
            if (array2D[x, y] != '-')
                return;

            array2D[x, y] = '▣';
        }

        public static bool FitHorizontalBlock(int x, int y, char[,] array2D)
        {
            if (array2D[x, y] != '-')
                return false;
            
            if (y + 1 == array2D.GetLength(1 ) || 
                array2D[x, y + 1] != '-')
                return false;
            
            array2D[x, y] = '□';
            array2D[x, y + 1] = '□';
            
            return true;
        }
        
        public static bool FitVerticalBlock(int x, int y, char[,] array2D)
        {
            if (array2D[x, y] != '-')
                return false;
            
            if (x + 1 == array2D.GetLength(0 ) || 
                array2D[x + 1, y] != '-')
                return false;
            
            array2D[x, y] = '▢';
            array2D[x, y + 1] = '▢';
            
            return true;
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
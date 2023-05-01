﻿using System;

namespace Array2DPath
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            char[,] pathArray = new char[8, 80];
            
            // populate
            for (int x = 0; x < pathArray.GetLength(0); x++)
            {
                for (int y = 0; y < pathArray.GetLength(1); y++)
                {
                    pathArray[x, y] = '-';
                }
            }

            Random random = new Random();
            int newIndex = random.Next(2, 8);
            
            // assign path
            for (int y = 0; y < pathArray.GetLength(1); y++)
            {
                pathArray[newIndex, y] = 'X';
                pathArray[newIndex - 1, y] = 'X';
                pathArray[newIndex - 2, y] = 'X';
                
                newIndex = random.Next(Math.Max(newIndex - 1, 2), Math.Min(newIndex + 2, 8));
            }
            
            // write line
            for (int x = 0; x < pathArray.GetLength(0); x++)
            {
                for (int y = 0; y < pathArray.GetLength(1); y++)
                {
                    if (y == 0)
                        Console.WriteLine();
                    
                    Console.Write(pathArray[x, y]);
                }
            }
        }
    }
}
using System;

namespace Lib_7
{
    public class LibClass
    {
        public static string MinItems(int[,] matr)
        {
            string minItems = "";
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                int min = matr[i, 0];
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    if (matr[i, j] < min) min = matr[i, j];
                }
                minItems += " " + min + " |";
            }
            return minItems;
        }
    }
}

using System;

namespace Sorting.utils
{
    public static class UtilClonar
    {
        public static int[] CloneArray(int[] originalArray)
        {
            if (originalArray == null)
            {
                return null;
            }
            int[] clonedArray = new int[originalArray.Length];
            Array.Copy(originalArray, clonedArray, originalArray.Length);
            return clonedArray;
        }
    }
}
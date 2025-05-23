namespace Sorting.sorting.simple
{
    public static class BubbleSort
    {
        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            long comparisons = 0;
            assignments = 0;
            swaps = 0;

            int n = array.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                assignments++; // swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    comparisons++;
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        assignments += 3; // temp, array[j], array[j+1]
                        swapped = true;
                        assignments++; // swapped = true;
                        swaps++;
                    }
                }
                comparisons++; // Condição de saída do loop externo (if (!swapped))
                if (!swapped)
                    break;
            }
            return comparisons;
        }
    }
}
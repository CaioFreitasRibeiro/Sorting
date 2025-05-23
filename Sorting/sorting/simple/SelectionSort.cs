namespace Sorting.sorting.simple
{
    public static class SelectionSort
    {
        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            long comparisons = 0;
            assignments = 0;
            swaps = 0;

            int n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int min_idx = i;
                assignments++; // min_idx = i;
                for (int j = i + 1; j < n; j++)
                {
                    comparisons++;
                    if (array[j] < array[min_idx])
                    {
                        min_idx = j;
                        assignments++; // min_idx = j;
                    }
                }

                if (min_idx != i) // Só troca se for diferente
                {
                    int temp = array[min_idx];
                    array[min_idx] = array[i];
                    array[i] = temp;
                    assignments += 3; // temp, array[min_idx], array[i]
                    swaps++;
                }
            }
            return comparisons;
        }
    }
}
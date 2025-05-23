namespace Sorting.sorting.simple
{
    public static class InsertionSort
    {
        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            long comparisons = 0;
            assignments = 0;
            swaps = 0;

            int n = array.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = array[i];
                assignments += 2; // key = array[i], int j = i - 1
                int j = i - 1;

                while (j >= 0)
                {
                    comparisons++; // j >= 0
                    if (array[j] > key)
                    {
                        comparisons++; // array[j] > key
                        array[j + 1] = array[j];
                        assignments++;
                        j = j - 1;
                        assignments++;
                        // Esta não é uma troca completa no sentido de "swaps",
                        // é um deslocamento. Então, só contamos as atribuições.
                    }
                    else
                    {
                        comparisons++; // array[j] > key (condição que falhou)
                        break;
                    }
                }
                array[j + 1] = key;
                assignments++;
            }
            return comparisons;
        }
    }
}
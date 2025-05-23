namespace Sorting.sorting.efficient
{
    public static class ShellSort
    {
        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            long comparisons = 0;
            assignments = 0;
            swaps = 0; // ShellSort não faz trocas no sentido tradicional, mas deslocamentos

            int n = array.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i += 1)
                {
                    int temp = array[i];
                    assignments += 2; // temp = array[i], int j
                    int j;
                    for (j = i; j >= gap; j -= gap)
                    {
                        comparisons++; // j >= gap
                        comparisons++; // array[j - gap] > temp
                        if (array[j - gap] > temp)
                        {
                            array[j] = array[j - gap];
                            assignments++;
                            // Não é uma troca, é um deslocamento. swaps não incrementa.
                        }
                        else
                        {
                            break;
                        }
                    }
                    array[j] = temp;
                    assignments++;
                }
            }
            return comparisons;
        }
    }
}
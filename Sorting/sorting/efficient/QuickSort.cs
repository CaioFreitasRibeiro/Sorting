namespace Sorting.sorting.efficient
{
    public static class QuickSort
    {
        private static long _comparisons = 0;
        private static long _assignments = 0;
        private static long _swaps = 0;

        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            _comparisons = 0;
            _assignments = 0;
            _swaps = 0;

            assignments = 0; // Inicializa antes da chamada interna
            swaps = 0;       // Inicializa antes da chamada interna

            if (array == null || array.Length < 2)
            {
                return 0;
            }
            InternalSort(array, 0, array.Length - 1);
            assignments = _assignments;
            swaps = _swaps;
            return _comparisons;
        }

        private static void InternalSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                _comparisons++; // low < high
                int pi = Partition(array, low, high);
                InternalSort(array, low, pi - 1);
                InternalSort(array, pi + 1, high);
            }
            else
            {
                _comparisons++; // low < high (condição que falhou)
            }
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            _assignments++; // pivot = array[high]
            int i = (low - 1);
            _assignments++; // i = (low - 1)

            for (int j = low; j < high; j++)
            {
                _comparisons++; // j < high
                _comparisons++; // array[j] <= pivot
                if (array[j] <= pivot)
                {
                    i++;
                    _assignments++; // i++
                    // Troca de array[i] e array[j]
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    _assignments += 3; // temp, array[i], array[j]
                    _swaps++;
                }
            }
            _comparisons++; // j < high (condição final)

            // Troca de array[i+1] e array[high] (pivot)
            int temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;
            _assignments += 3; // temp1, array[i+1], array[high]
            _swaps++;

            return i + 1;
        }
    }
}
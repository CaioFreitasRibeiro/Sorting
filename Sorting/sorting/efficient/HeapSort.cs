namespace Sorting.sorting.efficient
{
    public static class HeapSort
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

            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);
            }

            for (int i = n - 1; i > 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                _assignments += 3; // temp, array[0], array[i]
                _swaps++;

                Heapify(array, i, 0);
            }
            assignments = _assignments;
            swaps = _swaps;
            return _comparisons;
        }

        private static void Heapify(int[] array, int n, int i)
        {
            int largest = i;
            _assignments++; // largest = i
            int left = 2 * i + 1;
            _assignments++; // left
            int right = 2 * i + 2;
            _assignments++; // right

            if (left < n)
            {
                _comparisons++; // left < n
                _comparisons++; // array[left] > array[largest]
                if (array[left] > array[largest])
                {
                    largest = left;
                    _assignments++; // largest = left
                }
            }
            else
            {
                _comparisons++; // left < n (condição que falhou)
            }

            if (right < n)
            {
                _comparisons++; // right < n
                _comparisons++; // array[right] > array[largest]
                if (array[right] > array[largest])
                {
                    largest = right;
                    _assignments++; // largest = right
                }
            }
            else
            {
                _comparisons++; // right < n (condição que falhou)
            }


            _comparisons++; // largest != i
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;
                _assignments += 3; // swap, array[i], array[largest]
                _swaps++;

                Heapify(array, n, largest);
            }
        }
    }
}
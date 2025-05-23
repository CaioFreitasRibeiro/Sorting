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

            assignments = 0;
            swaps = 0;

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
                _comparisons++;
                int pi = Partition(array, low, high);
                InternalSort(array, low, pi - 1);
                InternalSort(array, pi + 1, high);
            }
            else
            {
                _comparisons++;
            }
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            _assignments++;
            int i = (low - 1);
            _assignments++;

            for (int j = low; j < high; j++)
            {
                _comparisons++;
                _comparisons++;
                if (array[j] <= pivot)
                {
                    i++;
                    _assignments++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    _assignments += 3;
                    _swaps++;
                }
            }
            _comparisons++;

            int temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;
            _assignments += 3;
            _swaps++;

            return i + 1;
        }
    }
}
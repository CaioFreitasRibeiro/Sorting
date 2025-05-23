namespace Sorting.sorting.efficient
{
    public static class MergeSort
    {
        private static long _comparisons = 0;
        private static long _assignments = 0;
        private static long _swaps = 0; // MergeSort não faz trocas no sentido tradicional

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

        private static void InternalSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                InternalSort(array, left, mid);
                InternalSort(array, mid + 1, right);
                Merge(array, left, mid, right);
            }
        }

        private static void Merge(int[] array, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] L = new int[n1];
            int[] R = new int[n2];
            _assignments += 2; // Alocação de arrays L e R (custo de memória, mas também atribuições implícitas)

            for (int i = 0; i < n1; ++i)
            {
                L[i] = array[left + i];
                _assignments++;
            }
            for (int j = 0; j < n2; ++j)
            {
                R[j] = array[mid + 1 + j];
                _assignments++;
            }

            int k = left;
            int iL = 0, iR = 0;
            _assignments += 3; // k, iL, iR

            while (iL < n1 && iR < n2)
            {
                _comparisons += 2; // iL < n1 && iR < n2
                _comparisons++; // L[iL] <= R[iR]
                if (L[iL] <= R[iR])
                {
                    array[k] = L[iL];
                    _assignments++;
                    iL++;
                    _assignments++;
                }
                else
                {
                    array[k] = R[iR];
                    _assignments++;
                    iR++;
                    _assignments++;
                }
                k++;
                _assignments++;
            }
            _comparisons += 2; // Condição final do while loop

            while (iL < n1)
            {
                _comparisons++; // iL < n1
                array[k] = L[iL];
                _assignments++;
                iL++;
                _assignments++;
                k++;
                _assignments++;
            }
            _comparisons++; // Condição final do while loop

            while (iR < n2)
            {
                _comparisons++; // iR < n2
                array[k] = R[iR];
                _assignments++;
                iR++;
                _assignments++;
                k++;
                _assignments++;
            }
            _comparisons++; // Condição final do while loop
        }
    }
}
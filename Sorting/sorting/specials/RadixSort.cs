using System.Linq;

namespace Sorting.sorting.specials
{
    public static class RadixSort
    {
        private static long _assignments = 0;
        private static long _swaps = 0;

        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            _assignments = 0;
            _swaps = 0;
            assignments = 0; // Inicializa antes da chamada interna
            swaps = 0;       // Inicializa antes da chamada interna

            if (array == null || array.Length == 0)
            {
                return 0;
            }

            int max = array.Max();
            _assignments++; // max
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                _assignments++; // exp *= 10
                CountSortByDigit(array, exp);
            }
            assignments = _assignments;
            swaps = _swaps;
            return 0; // Não é um algoritmo baseado em comparações
        }

        private static void CountSortByDigit(int[] array, int exp)
        {
            int n = array.Length;
            int[] output = new int[n];
            _assignments++; // alocação
            int[] count = new int[10];
            _assignments++; // alocação

            for (int i = 0; i < 10; i++)
            {
                count[i] = 0;
                _assignments++;
            }

            for (int i = 0; i < n; i++)
            {
                count[(array[i] / exp) % 10]++;
                _assignments++; // atribuição (incremento)
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
                _assignments++; // atribuição (incremento)
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(array[i] / exp) % 10] - 1] = array[i];
                _assignments++;
                count[(array[i] / exp) % 10]--;
                _assignments++; // atribuição (decremento)
            }

            for (int i = 0; i < n; i++)
            {
                array[i] = output[i];
                _assignments++;
            }
        }
    }
}
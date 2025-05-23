using System.Linq;

namespace Sorting.sorting.specials
{
    public static class CountingSort
    {
        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            assignments = 0;
            swaps = 0; // Não faz trocas

            if (array == null || array.Length == 0)
            {
                return 0;
            }

            int max = array.Max();
            int min = array.Min();
            assignments += 2; // max, min
            int range = max - min + 1;
            assignments++; // range

            int[] count = new int[range];
            assignments++; // alocação
            int[] output = new int[array.Length];
            assignments++; // alocação

            for (int i = 0; i < range; i++)
            {
                count[i] = 0;
                assignments++;
            }

            for (int i = 0; i < array.Length; i++)
            {
                count[array[i] - min]++;
                assignments++; // atribuição (incremento)
            }

            for (int i = 1; i < range; i++)
            {
                count[i] += count[i - 1];
                assignments++; // atribuição (incremento)
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[count[array[i] - min] - 1] = array[i];
                assignments++;
                count[array[i] - min]--;
                assignments++; // atribuição (decremento)
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = output[i];
                assignments++;
            }
            return 0; // Não é um algoritmo baseado em comparações
        }
    }
}
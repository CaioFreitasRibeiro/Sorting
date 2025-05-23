using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorting.sorting.specials
{
    public static class BucketSort
    {
        public static long Sort(int[] array, out long assignments, out long swaps)
        {
            assignments = 0;
            swaps = 0; // Não faz trocas no sentido tradicional

            if (array == null || array.Length <= 1)
            {
                return 0;
            }

            int maxVal = array.Max();
            assignments++; // maxVal
            int minVal = array.Min();
            assignments++; // minVal

            if (maxVal == minVal)
            {
                return 0;
            }

            int numberOfBuckets = 10;
            assignments++; // numberOfBuckets
            double rangePerBucket = (double)(maxVal - minVal + 1) / numberOfBuckets;
            assignments++; // rangePerBucket

            List<int>[] buckets = new List<int>[numberOfBuckets];
            assignments++; // alocação de buckets array
            for (int i = 0; i < numberOfBuckets; i++)
            {
                buckets[i] = new List<int>();
                assignments++; // alocação de cada List<int>
            }

            for (int i = 0; i < array.Length; i++)
            {
                int bucketIndex = (int)((array[i] - minVal) / rangePerBucket);
                assignments++; // bucketIndex
                if (bucketIndex >= numberOfBuckets)
                {
                    bucketIndex = numberOfBuckets - 1;
                    assignments++;
                }
                buckets[bucketIndex].Add(array[i]);
                // List.Add() tem custo interno, não contamos suas atribuições internas detalhadamente aqui.
            }

            int k = 0;
            assignments++; // k = 0
            foreach (List<int> bucket in buckets)
            {
                // bucket.Sort() usa IntroSort, não controlamos suas comparações/atribuições/trocas aqui.
                foreach (int item in bucket)
                {
                    array[k++] = item;
                    assignments++; // atribuição de item ao array final
                }
            }
            return 0; // Não é um algoritmo baseado em comparações diretas
        }
    }
}
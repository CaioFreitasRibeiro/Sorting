using System;
using Sorting.enums;
using Sorting.sorting.simple;
using Sorting.sorting.efficient;
using Sorting.sorting.specials;

namespace Sorting.manager
{
    public static class ManagerFileSorting
    {
        public static long Ordenar(Sortings algoritmo, int[] vet, out long assignments, out long swaps)
        {
            long comparisons = 0;
            assignments = 0;
            swaps = 0;

            switch (algoritmo)
            {
                case Sortings.BUBBLESORT:
                    comparisons = BubbleSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.SELECTIONSORT:
                    comparisons = SelectionSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.INSERTIONSORT:
                    comparisons = InsertionSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.MERGESORT:
                    comparisons = MergeSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.QUICKSORT:
                    comparisons = QuickSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.HEAPSORT:
                    comparisons = HeapSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.SHELLSORT:
                    comparisons = ShellSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.COUNTINGSORT:
                    comparisons = CountingSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.RADIXSORT:
                    comparisons = RadixSort.Sort(vet, out assignments, out swaps);
                    break;

                case Sortings.BUCKETSORT:
                    comparisons = BucketSort.Sort(vet, out assignments, out swaps);
                    break;

                default:
                    Console.WriteLine($"Algoritmo '{algoritmo}' não implementado ou reconhecido em ManagerFileSorting.");
                    comparisons = -1;
                    assignments = -1;
                    swaps = -1;
                    break;
            }
            return comparisons;
        }
    }
}
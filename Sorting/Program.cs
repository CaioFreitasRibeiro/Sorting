using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Sorting.basic_class.@static;
using Sorting.basic_class.dynamic;
using Sorting.enums;
using Sorting.manager;
using Sorting.reader;
using Sorting.utils;
using Sorting.sorting.simple;
using Sorting.sorting.efficient;
using Sorting.sorting.specials;

public class Program
{
    private const string SPECIFIC_INPUT_FILE = "1000000-aleatorios.txt";

    public static void Main(string[] args)
    {
        Console.WriteLine("--- Testador de Algoritmos de Ordenação ---");

        string inputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inputs");
        string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        string[] allInputFiles = Directory.GetFiles(inputDirectory, "*.txt")
                                            .Select(Path.GetFileName)
                                            .OrderBy(f => f)
                                            .ToArray();

        Dictionary<string, Sortings> algorithmMap = new Dictionary<string, Sortings>
        {
            { "BubbleSort", Sortings.BUBBLESORT },
            { "InsertionSort", Sortings.INSERTIONSORT },
            { "SelectionSort", Sortings.SELECTIONSORT },
            { "MergeSort", Sortings.MERGESORT },
            { "QuickSort", Sortings.QUICKSORT },
            { "HeapSort", Sortings.HEAPSORT },
            { "ShellSort", Sortings.SHELLSORT },
            { "CountingSort", Sortings.COUNTINGSORT },
            { "RadixSort", Sortings.RADIXSORT },
            { "BucketSort", Sortings.BUCKETSORT }
        };

        List<Tuple<string, string, long, long, long, long>> results = new List<Tuple<string, string, long, long, long, long>>();

        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1. Testar todos os algoritmos com todos os arquivos (apenas tempo)");
            Console.WriteLine("2. Selecionar arquivo e algoritmo específicos (apenas tempo)");
            Console.WriteLine($"3. Contar Comparações para '{SPECIFIC_INPUT_FILE}' (tempo e comparações)");
            Console.WriteLine($"4. Contar Atribuições e Trocas para '{SPECIFIC_INPUT_FILE}' (tempo, atribuições e trocas)");
            Console.WriteLine("5. Testar Fila e Pilha Estáticas com '100-aleatorios.txt'");
            Console.WriteLine("6. Testar Lista Estática e Ordenação com '1000000-aleatorios.txt'");
            Console.WriteLine("7. Testar Fila, Pilha e Lista Dinâmicas e Ordenação");
            Console.WriteLine("8. Sair");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                RunPerformanceTest(allInputFiles, algorithmMap, inputDirectory, results, MetricType.TimeOnly);
                if (results.Any())
                {
                    DisplayAndSaveResults(results, outputDirectory, MetricType.TimeOnly);
                    results.Clear();
                }
            }
            else if (choice == "2")
            {
                RunPerformanceTest(allInputFiles, algorithmMap, inputDirectory, results, MetricType.TimeOnly, specificFile: true);
                if (results.Any())
                {
                    DisplayAndSaveResults(results, outputDirectory, MetricType.TimeOnly);
                    results.Clear();
                }
            }
            else if (choice == "3")
            {
                RunPerformanceTest(new string[] { SPECIFIC_INPUT_FILE }, algorithmMap, inputDirectory, results, MetricType.Comparisons);
                if (results.Any())
                {
                    DisplayAndSaveResults(results, outputDirectory, MetricType.Comparisons);
                    results.Clear();
                }
            }
            else if (choice == "4")
            {
                RunPerformanceTest(new string[] { SPECIFIC_INPUT_FILE }, algorithmMap, inputDirectory, results, MetricType.AssignmentsAndSwaps);
                if (results.Any())
                {
                    DisplayAndSaveResults(results, outputDirectory, MetricType.AssignmentsAndSwaps);
                    results.Clear();
                }
            }
            else if (choice == "5")
            {
                RunQueueStackTest(inputDirectory);
            }
            else if (choice == "6")
            {
                RunStaticListSortTest(inputDirectory);
            }
            else if (choice == "7")
            {
                RunDynamicStructuresTest(inputDirectory);
            }
            else if (choice == "8")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }

        Console.WriteLine("Programa encerrado.");
    }

    private enum MetricType { TimeOnly, Comparisons, AssignmentsAndSwaps }

    private static void RunPerformanceTest(string[] inputFilesToTest, Dictionary<string, Sortings> algorithmMap, string inputDirectory, List<Tuple<string, string, long, long, long, long>> results, MetricType metricType, bool specificFile = false)
    {
        string currentFileName = "";
        if (specificFile)
        {
            Console.WriteLine("\nArquivos de entrada disponíveis:");
            for (int i = 0; i < inputFilesToTest.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {inputFilesToTest[i]}");
            }
            Console.Write("Escolha o número do arquivo: ");
            if (!int.TryParse(Console.ReadLine(), out int fileIndex) || fileIndex < 1 || fileIndex > inputFilesToTest.Length)
            {
                Console.WriteLine("Seleção de arquivo inválida.");
                return;
            }
            currentFileName = inputFilesToTest[fileIndex - 1];
            inputFilesToTest = new string[] { currentFileName };
        }

        string currentAlgName = "";
        Sortings currentSortEnum = Sortings.BUBBLESORT;
        bool specificAlg = false;

        if (specificFile)
        {
            Console.WriteLine("\nAlgoritmos de ordenação disponíveis:");
            List<string> algNames = algorithmMap.Keys.ToList();
            for (int i = 0; i < algNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {algNames[i]}");
            }
            Console.Write("Escolha o número do algoritmo: ");
            if (!int.TryParse(Console.ReadLine(), out int algIndex) || algIndex < 1 || algIndex > algNames.Count)
            {
                Console.WriteLine("Seleção de algoritmo inválida.");
                return;
            }
            currentAlgName = algNames[algIndex - 1];
            currentSortEnum = algorithmMap[currentAlgName];
            specificAlg = true;
        }

        foreach (string fileName in inputFilesToTest)
        {
            if (!specificFile) Console.WriteLine($"\nTestando com o arquivo: {fileName}");
            string fullPath = Path.Combine(inputDirectory, fileName);

            ReaderFile fileReader = new ReaderFile(fullPath);
            int[] originalArray = fileReader.ReadNumbersToArray();

            if (originalArray == null) continue;

            List<KeyValuePair<string, Sortings>> algorithmsToTest = specificAlg
                                                                    ? new List<KeyValuePair<string, Sortings>> { new KeyValuePair<string, Sortings>(currentAlgName, currentSortEnum) }
                                                                    : algorithmMap.ToList();

            foreach (var algorithmEntry in algorithmsToTest)
            {
                string algName = algorithmEntry.Key;
                Sortings sortEnum = algorithmEntry.Value;

                int[] arrayToSort = UtilClonar.CloneArray(originalArray);

                Console.Write($"  Executando {algName}...");

                UtilCountingTime timer = new UtilCountingTime();
                timer.Init();
                long comparisons = 0;
                long assignments = 0;
                long swaps = 0;

                comparisons = ManagerFileSorting.Ordenar(sortEnum, arrayToSort, out assignments, out swaps);

                timer.Stop();
                long elapsedMs = timer.GetElapsedTime();

                string logOutput = $" Concluído em {elapsedMs} ms.";
                if (metricType == MetricType.Comparisons)
                {
                    logOutput += $" Comparações: {comparisons:N0}";
                }
                else if (metricType == MetricType.AssignmentsAndSwaps)
                {
                    logOutput += $" Atribuições: {assignments:N0}, Trocas: {swaps:N0}";
                }
                Console.WriteLine(logOutput);

                if (!IsSorted(arrayToSort))
                {
                    Console.WriteLine($"    ATENÇÃO: Array NÃO está ordenado após {algName}!");
                }
                results.Add(Tuple.Create(algName, fileName, elapsedMs, comparisons, assignments, swaps));
            }
        }
    }

    private static void DisplayAndSaveResults(List<Tuple<string, string, long, long, long, long>> results, string outputDirectory, MetricType metricType)
    {
        Console.WriteLine("\n--- Tabela de Comparação de Algoritmos de Ordenação ---");

        string headerFormat;
        string lineFormat;
        string fileNameSuffix;

        if (metricType == MetricType.Comparisons)
        {
            headerFormat = "{0,-20} | {1,-25} | {2,-15} | {3,-18}";
            lineFormat = "{0,-20} | {1,-25} | {2,-15} | {3,-18:N0}";
            Console.WriteLine(headerFormat, "Algoritmo", "Arquivo", "Tempo (ms)", "Comparações");
            Console.WriteLine(new string('-', 85));
            fileNameSuffix = "_Comparisons";
        }
        else if (metricType == MetricType.AssignmentsAndSwaps)
        {
            headerFormat = "{0,-20} | {1,-25} | {2,-15} | {3,-18} | {4,-15}";
            lineFormat = "{0,-20} | {1,-25} | {2,-15} | {3,-18:N0} | {4,-15:N0}";
            Console.WriteLine(headerFormat, "Algoritmo", "Arquivo", "Tempo (ms)", "Atribuições", "Trocas");
            Console.WriteLine(new string('-', 105));
            fileNameSuffix = "_AssignmentsAndSwaps";
        }
        else
        {
            headerFormat = "{0,-20} | {1,-25} | {2,-15}";
            lineFormat = "{0,-20} | {1,-25} | {2,-15}";
            Console.WriteLine(headerFormat, "Algoritmo", "Arquivo", "Tempo (ms)");
            Console.WriteLine(new string('-', 65));
            fileNameSuffix = "_TimeOnly";
        }

        foreach (var res in results.OrderBy(r => r.Item2).ThenBy(r => r.Item1))
        {
            if (metricType == MetricType.Comparisons)
            {
                Console.WriteLine(lineFormat, res.Item1, res.Item2, res.Item3, res.Item4);
            }
            else if (metricType == MetricType.AssignmentsAndSwaps)
            {
                Console.WriteLine(lineFormat, res.Item1, res.Item2, res.Item3, res.Item5, res.Item6);
            }
            else
            {
                Console.WriteLine(lineFormat, res.Item1, res.Item2, res.Item3);
            }
        }

        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string outputPath = Path.Combine(outputDirectory, $"ComparisonTable_{timestamp}{fileNameSuffix}.txt");
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            if (metricType == MetricType.Comparisons)
            {
                writer.WriteLine(headerFormat, "Algoritmo", "Arquivo", "Tempo (ms)", "Comparações");
                writer.WriteLine(new string('-', 85));
            }
            else if (metricType == MetricType.AssignmentsAndSwaps)
            {
                writer.WriteLine(headerFormat, "Algoritmo", "Arquivo", "Tempo (ms)", "Atribuições", "Trocas");
                writer.WriteLine(new string('-', 105));
            }
            else
            {
                writer.WriteLine(headerFormat, "Algoritmo", "Arquivo", "Tempo (ms)");
                writer.WriteLine(new string('-', 65));
            }

            foreach (var res in results.OrderBy(r => r.Item2).ThenBy(r => r.Item1))
            {
                if (metricType == MetricType.Comparisons)
                {
                    writer.WriteLine(lineFormat, res.Item1, res.Item2, res.Item3, res.Item4);
                }
                else if (metricType == MetricType.AssignmentsAndSwaps)
                {
                    writer.WriteLine(lineFormat, res.Item1, res.Item2, res.Item3, res.Item5, res.Item6);
                }
                else
                {
                    writer.WriteLine(lineFormat, res.Item1, res.Item2, res.Item3);
                }
            }
        }
        Console.WriteLine($"\nTabela de comparação salva em: {outputPath}");
    }

    private static bool IsSorted(int[] array)
    {
        if (array == null || array.Length <= 1)
        {
            return true;
        }
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    public static void RunQueueStackTest(string inputDirectory)
    {
        Console.WriteLine("\n--- Teste de Fila e Pilha Estáticas ---");
        string filePath = Path.Combine(inputDirectory, "100-aleatorios.txt");

        ReaderFile fileReader = new ReaderFile(filePath);
        int[] numbers = fileReader.ReadNumbersToArray();

        if (numbers == null || numbers.Length == 0)
        {
            Console.WriteLine("Não foi possível carregar números do arquivo 100-aleatorios.txt");
            return;
        }

        Console.WriteLine($"\nCarregando {numbers.Length} números do arquivo: {filePath}");

        Console.WriteLine("\n--- Teste de Fila Estática ---");
        Fila fila = new Fila(numbers.Length + 5);

        foreach (int num in numbers)
        {
            fila.Inserir(num);
        }
        fila.Mostrar();

        Console.WriteLine("\nFazendo algumas remoções na Fila:");
        if (fila.cont > 0)
        {
            Console.WriteLine($"Removido: {fila.Remover()}");
        }
        if (fila.cont > 0)
        {
            Console.WriteLine($"Removido: {fila.Remover()}");
        }
        fila.Mostrar();

        Console.WriteLine("\nFazendo algumas inserções extras na Fila:");
        fila.Inserir(999);
        fila.Inserir(888);
        fila.Mostrar();

        Console.WriteLine("\n--- Teste de Pilha Estática ---");
        Pilha pilha = new Pilha(numbers.Length + 5);

        foreach (int num in numbers)
        {
            pilha.Inserir(num);
        }
        pilha.Mostrar();

        Console.WriteLine("\nFazendo algumas remoções na Pilha:");
        if (pilha.topo != -1)
        {
            Console.WriteLine($"Removido: {pilha.Remover()}");
        }
        if (pilha.topo != -1)
        {
            Console.WriteLine($"Removido: {pilha.Remover()}");
        }
        pilha.Mostrar();

        Console.WriteLine("\nFazendo algumas inserções extras na Pilha:");
        pilha.Inserir(777);
        pilha.Inserir(666);
        pilha.Mostrar();
    }

    public static void RunStaticListSortTest(string inputDirectory)
    {
        Console.WriteLine("\n--- Teste de Lista Estática e Ordenação ---");
        string filePath = Path.Combine(inputDirectory, "1000000-aleatorios.txt");

        ReaderFile fileReader = new ReaderFile(filePath);
        int[] numbers = fileReader.ReadNumbersToArray();

        if (numbers == null || numbers.Length == 0)
        {
            Console.WriteLine("Não foi possível carregar números do arquivo 1000000-aleatorios.txt");
            return;
        }

        Console.WriteLine($"\nCarregando {numbers.Length} números do arquivo para a Lista Estática...");
        Lista listaEstatica = new Lista(numbers.Length);

        foreach (int num in numbers)
        {
            listaEstatica.Inserir(num);
        }

        Console.WriteLine($"Lista Estática carregada com {listaEstatica.Size()} elementos.");

        Console.WriteLine("\nConvertendo Lista Estática para array para ordenação...");
        int[] arrayParaOrdenar = listaEstatica.ToArray();

        Console.WriteLine("\nExecutando QuickSort na Lista Estática (convertida para array)...");
        long assignments, swaps;
        UtilCountingTime timer = new UtilCountingTime();
        timer.Init();
        long comparisons = QuickSort.Sort(arrayParaOrdenar, out assignments, out swaps);
        timer.Stop();
        long elapsedMs = timer.GetElapsedTime();

        Console.WriteLine($"Ordenação concluída em {elapsedMs} ms.");
        Console.WriteLine($"Comparações: {comparisons:N0}, Atribuições: {assignments:N0}, Trocas: {swaps:N0}");

        if (IsSorted(arrayParaOrdenar))
        {
            Console.WriteLine("Array ordenado com sucesso!");
        }
        else
        {
            Console.WriteLine("ATENÇÃO: Array NÃO está ordenado após o QuickSort!");
        }

        listaEstatica.FromArray(arrayParaOrdenar);
        Console.WriteLine("Lista Estática atualizada com os elementos ordenados.");
    }

    public static void RunDynamicStructuresTest(string inputDirectory)
    {
        Console.WriteLine("\n--- Teste de Fila, Pilha e Lista Dinâmicas e Ordenação ---");
        string filePath = Path.Combine(inputDirectory, "1000000-aleatorios.txt");

        ReaderFile fileReader = new ReaderFile(filePath);
        int[] numbers = fileReader.ReadNumbersToArray();

        if (numbers == null || numbers.Length == 0)
        {
            Console.WriteLine("Não foi possível carregar números do arquivo 1000000-aleatorios.txt");
            return;
        }

        Console.WriteLine($"\nCarregando {numbers.Length} números do arquivo...");

        Console.WriteLine("\n--- Teste de Fila Dinâmica ---");
        FilaDinamica filaDinamica = new FilaDinamica();
        foreach (int num in numbers)
        {
            filaDinamica.Inserir(num);
        }
        Console.WriteLine($"Fila Dinâmica carregada com {filaDinamica.Size()} elementos.");
        //filaDinamica.Mostrar(); // Evitar para grandes volumes
        Console.WriteLine("Fazendo algumas remoções na Fila Dinâmica:");
        if (!filaDinamica.IsEmpty()) Console.WriteLine($"Removido: {filaDinamica.Remover()}");
        if (!filaDinamica.IsEmpty()) Console.WriteLine($"Removido: {filaDinamica.Remover()}");
        //filaDinamica.Mostrar();
        Console.WriteLine("Fazendo algumas inserções extras na Fila Dinâmica:");
        filaDinamica.Inserir(9999);
        filaDinamica.Inserir(8888);
        //filaDinamica.Mostrar();


        Console.WriteLine("\n--- Teste de Pilha Dinâmica ---");
        PilhaDinamica pilhaDinamica = new PilhaDinamica();
        foreach (int num in numbers)
        {
            pilhaDinamica.Inserir(num);
        }
        Console.WriteLine($"Pilha Dinâmica carregada com {pilhaDinamica.Size()} elementos.");
        //pilhaDinamica.Mostrar(); // Evitar para grandes volumes
        Console.WriteLine("Fazendo algumas remoções na Pilha Dinâmica:");
        if (!pilhaDinamica.IsEmpty()) Console.WriteLine($"Removido: {pilhaDinamica.Remover()}");
        if (!pilhaDinamica.IsEmpty()) Console.WriteLine($"Removido: {pilhaDinamica.Remover()}");
        //pilhaDinamica.Mostrar();
        Console.WriteLine("Fazendo algumas inserções extras na Pilha Dinâmica:");
        pilhaDinamica.Inserir(7777);
        pilhaDinamica.Inserir(6666);
        //pilhaDinamica.Mostrar();


        Console.WriteLine("\n--- Teste de Lista Dinâmica e Ordenação ---");
        ListaDinamica listaDinamica = new ListaDinamica();
        foreach (int num in numbers)
        {
            listaDinamica.Inserir(num);
        }
        Console.WriteLine($"Lista Dinâmica carregada com {listaDinamica.Size()} elementos.");

        Console.WriteLine("\nConvertendo Lista Dinâmica para array para ordenação...");
        int[] arrayParaOrdenar = listaDinamica.ToArray();

        Console.WriteLine("\nExecutando MergeSort na Lista Dinâmica (convertida para array)...");
        long assignments, swaps;
        UtilCountingTime timer = new UtilCountingTime();
        timer.Init();
        long comparisons = MergeSort.Sort(arrayParaOrdenar, out assignments, out swaps);
        timer.Stop();
        long elapsedMs = timer.GetElapsedTime();

        Console.WriteLine($"Ordenação concluída em {elapsedMs} ms.");
        Console.WriteLine($"Comparações: {comparisons:N0}, Atribuições: {assignments:N0}, Trocas: {swaps:N0}");

        if (IsSorted(arrayParaOrdenar))
        {
            Console.WriteLine("Array ordenado com sucesso!");
        }
        else
        {
            Console.WriteLine("ATENÇÃO: Array NÃO está ordenado após o MergeSort!");
        }

        Console.WriteLine("\nAtualizando Lista Dinâmica com os elementos ordenados do array...");
        listaDinamica.FromArray(arrayParaOrdenar);
        Console.WriteLine("Lista Dinâmica atualizada com os elementos ordenados.");
    }
}
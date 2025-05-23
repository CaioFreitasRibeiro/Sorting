using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sorting.reader
{
    public class ReaderFile
    {
        public string pathFile;

        public ReaderFile()
        {
            pathFile = "";
        }

        public ReaderFile(string pathFile)
        {
            this.pathFile = pathFile;
        }

        public string LerTodoArquivo()
        {
            // Certifique-se de que o System.IO está importado
            if (!File.Exists(this.pathFile))
            {
                Console.WriteLine($"Erro: Arquivo '{this.pathFile}' não encontrado.");
                return null;
            }
            return File.ReadAllText(this.pathFile);
        }

        public string[] LerLinhaALinha()
        {
            if (!File.Exists(this.pathFile))
            {
                Console.WriteLine($"Erro: Arquivo '{this.pathFile}' não encontrado.");
                return null;
            }
            return File.ReadLines(this.pathFile).ToArray();
        }

        public int GetNumLinhas()
        {
            if (!File.Exists(this.pathFile))
            {
                Console.WriteLine($"Erro: Arquivo '{this.pathFile}' não encontrado.");
                return 0; // Ou lançar uma exceção, dependendo do tratamento de erro desejado
            }
            return File.ReadLines(this.pathFile).Count();
        }

        // Método para ler números e convertê-los em um array de int
        public int[] ReadNumbersToArray()
        {
            List<int> numbers = new List<int>();
            try
            {
                string[] lines = this.LerLinhaALinha(); // Usa o método de instância existente
                if (lines == null) return null; // Se LerLinhaALinha já tratou a falta do arquivo

                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int number))
                    {
                        numbers.Add(number);
                    }
                    // Opcional: else para lidar com linhas que não são números
                }
            }
            catch (Exception ex) // Captura outras exceções que possam ocorrer
            {
                Console.WriteLine($"Erro inesperado ao ler o arquivo {this.pathFile}: {ex.Message}");
                return null;
            }
            return numbers.ToArray();
        }
    }
}
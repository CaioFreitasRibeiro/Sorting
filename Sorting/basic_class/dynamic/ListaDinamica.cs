using System;
using System.Collections.Generic;

namespace Sorting.basic_class.dynamic
{
    public class ListaDinamica
    {
        private List<int> lista; // Utiliza List<T> do C# para alocação dinâmica real

        public ListaDinamica()
        {
            lista = new List<int>();
        }

        public void Inserir(int item)
        {
            lista.Add(item);
        }

        public void InserirEmPosicao(int item, int posicao)
        {
            if (posicao < 0 || posicao > lista.Count)
            {
                Console.WriteLine("Posição inválida para inserção.");
                return;
            }
            lista.Insert(posicao, item);
        }

        public int Remover(int posicao)
        {
            if (posicao < 0 || posicao >= lista.Count)
            {
                Console.WriteLine("Posição inválida para remoção.");
                return -1;
            }
            int itemRemovido = lista[posicao];
            lista.RemoveAt(posicao);
            return itemRemovido;
        }

        public int Get(int posicao)
        {
            if (posicao < 0 || posicao >= lista.Count)
            {
                throw new IndexOutOfRangeException("Posição inválida na lista.");
            }
            return lista[posicao];
        }

        public int Size()
        {
            return lista.Count;
        }

        public void Mostrar()
        {
            Console.Write("Lista Dinâmica: [");
            for (int i = 0; i < lista.Count; i++)
            {
                Console.Write(lista[i]);
                if (i < lista.Count - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");
        }

        public int[] ToArray()
        {
            return lista.ToArray();
        }

        public void FromArray(int[] array)
        {
            lista.Clear();
            lista.AddRange(array);
        }
    }
}
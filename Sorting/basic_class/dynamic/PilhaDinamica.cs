using System;
using System.Collections.Generic;

namespace Sorting.basic_class.dynamic
{
    public class PilhaDinamica
    {
        private LinkedList<int> pilha;

        public PilhaDinamica()
        {
            pilha = new LinkedList<int>();
        }

        public void Inserir(int item)
        {
            pilha.AddFirst(item); // Inserir no início para ser o "topo"
        }

        public int Remover()
        {
            if (pilha.Count == 0)
            {
                Console.WriteLine("Pilha vazia, não é possível remover elemento.");
                return -1;
            }
            int itemRemovido = pilha.First.Value;
            pilha.RemoveFirst();
            return itemRemovido;
        }

        public void Mostrar()
        {
            Console.WriteLine("Pilha Dinâmica: ");
            foreach (int item in pilha)
            {
                Console.WriteLine(item);
            }
        }

        public int Size()
        {
            return pilha.Count;
        }

        public bool IsEmpty()
        {
            return pilha.Count == 0;
        }
    }
}
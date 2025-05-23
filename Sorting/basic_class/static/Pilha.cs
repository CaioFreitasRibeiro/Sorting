namespace Sorting.basic_class.@static
{
    class Pilha
    {
        public int[] pilha;
        public int topo;

        public Pilha(int n)
        {
            pilha = new int[n];
            topo = -1;
        }

        public bool Inserir(int item)
        {
            if (topo < pilha.Length - 1)
            {
                topo++;
                pilha[topo] = item;
                return true;
            }
            else
            {
                Console.WriteLine("Pilha cheia, não é possível inserir o " + item);
                return false;
            }
        }

        public int Remover()
        {
            if (topo == -1)
            {
                Console.WriteLine("Pilha vazia, por isso não é possível remover elemento.");
                return -1;
            }
            int tmp = pilha[topo];
            pilha[topo] = -1;
            topo--;
            return tmp;
        }

        public void Mostrar()
        {
            Console.WriteLine(" Pilha ");

            for (int i = topo; i > -1; i--)
            {
                Console.WriteLine(pilha[i]);
            }
        }
    }
}
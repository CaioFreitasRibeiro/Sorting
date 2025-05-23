namespace Sorting.basic_class.@static
{
    public class Lista
    {
        public int[] lista;
        public int cont; // Contador de elementos

        public Lista(int capacidade)
        {
            lista = new int[capacidade];
            cont = 0;
        }

        public bool Inserir(int item)
        {
            if (cont < lista.Length)
            {
                lista[cont] = item;
                cont++;
                return true;
            }
            return false;
        }

        public bool InserirEmPosicao(int item, int posicao)
        {
            if (posicao < 0 || posicao > cont || cont == lista.Length)
            {
                return false;
            }
            for (int i = cont; i > posicao; i--)
            {
                lista[i] = lista[i - 1];
            }
            lista[posicao] = item;
            cont++;
            return true;
        }

        public int Remover(int posicao)
        {
            if (posicao < 0 || posicao >= cont)
            {
                return -1; // Indica erro ou elemento não encontrado
            }
            int itemRemovido = lista[posicao];
            for (int i = posicao; i < cont - 1; i++)
            {
                lista[i] = lista[i + 1];
            }
            cont--;
            lista[cont] = 0; // Opcional: limpar a última posição
            return itemRemovido;
        }

        public int Get(int posicao)
        {
            if (posicao < 0 || posicao >= cont)
            {
                throw new IndexOutOfRangeException("Posição inválida na lista.");
            }
            return lista[posicao];
        }

        public int Size()
        {
            return cont;
        }

        public void Mostrar()
        {
            Console.Write("Lista: [");
            for (int i = 0; i < cont; i++)
            {
                Console.Write(lista[i]);
                if (i < cont - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");
        }

        public int[] ToArray()
        {
            int[] tempArray = new int[cont];
            Array.Copy(lista, 0, tempArray, 0, cont);
            return tempArray;
        }

        public void FromArray(int[] array)
        {
            if (array.Length > lista.Length)
            {
                // Tratar erro ou redimensionar se fosse dinâmica
                Console.WriteLine("Erro: Array de entrada maior que a capacidade da lista estática.");
                return;
            }
            Array.Copy(array, 0, lista, 0, array.Length);
            cont = array.Length;
            // Opcional: zerar o restante do array interno se cont < lista.Length
            for (int i = cont; i < lista.Length; i++)
            {
                lista[i] = 0;
            }
        }
    }
}
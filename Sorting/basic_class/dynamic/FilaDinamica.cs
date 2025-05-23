namespace Sorting.basic_class.dynamic
{
    class FilaDynamic
    {
        public Celula? primeiro;
        public Celula? ultimo;
        public FilaDynamic()
        {
            this.primeiro = this.ultimo = new Celula();
        }

        public bool Inserir(int item)
        {
            this.ultimo.valor = item;
            this.ultimo.prox = new Celula();
            this.ultimo = this.ultimo.prox;

            return true;
        }

        public int Remover()
        {
            if (this.ultimo == this.primeiro)
            {
                Console.WriteLine("Não é possível remover da fila.");
            }

            Celula? tmp = primeiro;
            this.primeiro = primeiro.prox;

            return -1;
        }
    }
}

using System;

namespace Core
{
    public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor { get; }

        public Lance(Interessada cliente, double valor)
        {
            if (valor < 0)
                throw new ArgumentNullException("Valor deve ser numero positivo");

            Cliente = cliente;
            Valor = valor;
        }
    }
}

using System;
using System.Linq;

namespace Core
{
    public class MaiorLanceProximoDoValorDesejado : IModoLeilao
    {
        public double ValorDesejado { get; private set; }

        public MaiorLanceProximoDoValorDesejado(double valorDesejado)
        {
            ValorDesejado = valorDesejado;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                           .Where(x => x.Valor > ValorDesejado)
                           .OrderBy(x => x.Valor)
                           .DefaultIfEmpty(new Lance(null, 0))
                           .FirstOrDefault();       
        }
    }
}

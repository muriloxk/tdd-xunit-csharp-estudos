using System;
using System.Linq;

namespace Core
{
    public class MaiorLance : IModoLeilao
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                         .DefaultIfEmpty(new Lance(null, 0))
                         .OrderByDescending(x => x.Valor)
                         .FirstOrDefault();
        }
    }
}

using System;
using Core;

namespace Alura.LeilaoOnline.Terminal
{
    public class Program
    {
        //CICLO TDD:
        // 1. Falha.
        // 2. Corrigir.
        // 3. Sucesso.
        // 4. Refatorar.
        // 5. Se não falhar o teste ir para o 6, se não voutar para o 2.
        // 6. Ir para um novo teste.

        public static void Main(string[] args)
        {
            var leilao = new Leilao("Van Gogh", new MaiorLance());
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            leilao.TerminaPregao();

            Console.WriteLine(leilao.Ganhador.Valor);
            Console.ReadKey();
        }
    }
}

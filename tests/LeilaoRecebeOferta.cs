using System.Linq;
using Core;
using Xunit;

namespace tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado()
        {
            //Arranje - Cenário
            var leilao = new Leilao("Picasso", new MaiorLance());
            var fulano = new Interessada("Fulano", leilao);
            var beltrano = new Interessada("Beltrano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(beltrano, 900);
            leilao.TerminaPregao();

            //Act - Método sobre teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            Assert.Equal(900, leilao.Ganhador.Valor);
            Assert.Equal(2, leilao.Lances.Count());
        }

        [Fact]
        public void NaoPermitirDoisLancesConsecutivosDoMesmoInteressado()
        {
            //Arranje
            var leilao = new Leilao("Gol quadrado", new MaiorLance());
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 1000);

            //Act
            leilao.RecebeLance(fulano, 1100);
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(leilao.Lances.Count(), 1);
            Assert.Equal(leilao.Ganhador.Valor, 1000);
        }

        [Theory]
        [InlineData(new double[] { 300, 400, 500, 900 })]
        [InlineData(new double[] { 200 })]
        [InlineData(new double[] { 300, 400 })]
        public void QtdDeLancesPermanceZeroJaQueOLeilaoNaoFoiIniciado(double[] lances)
        {
            //Arranje
            var leilao = new Leilao("Joia rara", new MaiorLance());
            var fulano = new Interessada("Fulano", leilao);

            //Act
            foreach(var lance in lances)
               leilao.RecebeLance(fulano, lance);

            //Assert
            Assert.Equal(leilao.Lances.Count(), 0);
        }
    }
}

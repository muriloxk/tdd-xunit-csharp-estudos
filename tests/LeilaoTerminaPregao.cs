using System;
using Core;
using Xunit;

namespace tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1000, new double[] { 300, 400, 500, 600, 1000 })]
        [InlineData(1000, new double[] { 1000, 400, 500, 600, 300 })]
        [InlineData(1000, new double[] { 1000 })]
        public void RetornaOMaiorValorDosLances(double valorEsperado, double[] lances)
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", new MaiorLance());
            var fulano = new Interessada("Fulano", leilao);
            var beltrano = new Interessada("Beltrano", leilao);
            leilao.IniciaPregao();

            for(var i = 0; i < lances.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, lances[i]);
                    continue;
                }
                
                leilao.RecebeLance(beltrano, lances[i]);
            }
              

            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Theory]
        [InlineData(450, 500, new double[] { 300, 400, 500, 600, 1000 })]
        [InlineData(450, 500, new double[] { 1000, 400, 500, 600, 300 })]
        [InlineData(450, 1000, new double[] { 1000 })]
        public void RetornaOValorMaiorEMaisProximoDoDesejado(double valorDesejado,
                                                             double valorEsperado,
                                                             double[] lances)
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", new MaiorLanceProximoDoValorDesejado(valorDesejado));
            var fulano = new Interessada("Fulano", leilao);
            var beltrano = new Interessada("Beltrano", leilao);
            leilao.IniciaPregao();

            for (var i = 0; i < lances.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, lances[i]);
                    continue;
                }

                leilao.RecebeLance(beltrano, lances[i]);
            }


            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Fact]
        public void DeveLancarExcessaoAoFinalizarUmPregaoNaoIniciado()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", new MaiorLance());
          
            //Assert
            Assert.Throws(typeof(InvalidOperationException), () => leilao.TerminaPregao());
        }


        [Fact]
        public void RetornaZeroQuandoNaoEhFeitoNenhumLance()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", new MaiorLance());
            leilao.IniciaPregao();

            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(0, leilao.Ganhador.Valor);
        }
    }
}

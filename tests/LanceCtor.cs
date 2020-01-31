using System;
using Xunit;
using Core;

namespace tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancarArgumentNullExceptionAoPassarLanceNegativo()
        {
            var valorNegativo = -100;

            Assert.Throws<System.ArgumentNullException>(
                  () => new Lance(new Interessada("Murilo", new Leilao("Van Gogh", new MaiorLance())), -100)
            );
        }
    }
}

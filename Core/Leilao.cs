using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public enum SituacaoLeilao
    {
        NaoIniciado,
        EmAndamento,
        Finalizado,
    }

    public class Leilao
    {
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public SituacaoLeilao Situacao { get; private set; }
        public IModoLeilao ModoLeilao { get; private set; }
       

        private Lance _ganhador;
        public Lance Ganhador
        {
            get
            {
                if (_ganhador == null)
                    return new Lance(null, 0);

                return _ganhador;
            }

            private set
            {
                _ganhador = value;
            }
        }

        public Leilao(string peca, IModoLeilao modoLeilao)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Situacao = SituacaoLeilao.NaoIniciado;
            ModoLeilao = modoLeilao;
           
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceAceito(cliente))
                _lances.Add(new Lance(cliente, valor));
        }

        private bool NovoLanceAceito(Interessada cliente)
        {
            return Situacao == SituacaoLeilao.EmAndamento && !VerificaSeEhUmLanceConsecutivoDoMesmoInteressado(cliente);
        }

        private bool VerificaSeEhUmLanceConsecutivoDoMesmoInteressado(Interessada cliente)
        {
            if (_lances.Count() <= 0)
                return false;

            return _lances.Last().Cliente.Equals(cliente);
        }

        public void IniciaPregao()
        {
           Situacao = SituacaoLeilao.EmAndamento;
        }
        
        public void TerminaPregao()
        {
            if (Situacao == SituacaoLeilao.NaoIniciado)
                throw new InvalidOperationException("Leilão não iniciado");

            NomearGanhadorDosLances();
            Situacao = SituacaoLeilao.Finalizado;
        }

        private void NomearGanhadorDosLances()
        {


            Ganhador = ModoLeilao.Avalia(this);
        }
    }
}

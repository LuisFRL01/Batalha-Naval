using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Naval.br.ufrpe.model
{
    class TiroBarco : Peca
    {
        public TiroBarco(bool orientacao) : base(orientacao)
        {
            this.setTamanho(1);
            this.setAtingido(true);
        }
    }
}

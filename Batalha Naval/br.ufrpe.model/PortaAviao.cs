using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Naval.br.ufrpe.model
{
    public class PortaAviao : Peca
    {
        public PortaAviao(bool orientacao)
           : base(orientacao)
        {
            setTamanho(6);
            setAtingido(false);
        }
    }
}

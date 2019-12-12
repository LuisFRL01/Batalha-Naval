using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Naval.br.ufrpe.model
{
    public class Agua : Peca
    {
        public Agua(bool orientacao)
            :base(orientacao)
        {
            setTamanho(1);
            setAtingido(false);
        }
       

    }

}

using System;
using System.Collections.Generic;
using Batalha_Naval.br.ufrpe.model;

namespace Batalha_Naval
{
    public class ControllerJogo
    {
        Jogo jogo;

        public ControllerJogo(Tabuleiro tabPlayer)
        {
            jogo = new Jogo(tabPlayer);
        }

        public bool jogarPlayer(int x, int y)
        {
            return jogo.jogarPlayer(x, y);
        }

        public bool jogarIA()
        {
            return jogo.jogarIA();
        }

        public bool posicionarBarcosPlayer(String infos)
        {
            int barco, x, y;
            bool orientacao;
            String[] separadas = infos.Split(',');
            barco = Int32.Parse(separadas[0]);
            orientacao = bool.Parse(separadas[1]);
            x = Int32.Parse(separadas[2]);
            y = Int32.Parse(separadas[3]);
            return jogo.posicionarBarcos(barco, orientacao, y, x);
        }

        public void posicionarBarcosIA()
        {
            jogo.getIA().posicionarBarcosIA();
        }

        public IA getIA()
        {
            return jogo.getIA();
        }

        public Tabuleiro tabPlayer()
        {
            return jogo.getTabPlayer();
        }

        public Tabuleiro tabIa()
        {
            return jogo.getTabIA();
        }

        public int countBarcosPlayer()
        {
            return jogo.getCountBarcosPlayer();
        }

        public int countBarcosIA()
        {
            return jogo.getCountBarcosIA();
        }

        public bool posicionarBarcosIA(Tabuleiro tabIA)
        {
            return true;
        }

        public int[] getBarcosSelecionaveis()
        {
            return jogo.getBarcos();
        }

    }
}
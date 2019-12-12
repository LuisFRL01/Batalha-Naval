using System;
using System.Collections.Generic;
using Batalha_Naval.br.ufrpe.model;

namespace Batalha_Naval
{
    public class Jogo
    {
        private List<int> jogadasPlayerEixoX = new List<int>();
        private List<int> jogadasPlayerEixoY = new List<int>();

        private int[] barcos;

        private IA ia;

        private int countBarcosIA = 5;
        private int countBarcosPlayer = 5;

        private Tabuleiro tabPlayer;

        private Submarino subPlayer = new Submarino(true);
        private Encouracado encouraPlayer = new Encouracado(true);
        private Encouracado encoura1Player = new Encouracado(true);
        private NavioGuerra navioGPlayer = new NavioGuerra(true);
        private PortaAviao portaPlayer = new PortaAviao(true);

        public Jogo(Tabuleiro tabPlayer)
        {
            this.barcos = new int[5];
            this.tabPlayer = tabPlayer;
            this.ia = new IA(countBarcosPlayer, this.tabPlayer.getTamanhoTab());
            for (int i = 0; i < 5; i++)
            {
                barcos[i] = 1;
            }
        }

        public bool posicionarBarcos(int barco, bool orientacao, int x, int y)
        {

            bool validarPosicao = true;
            Peca tipoBarco;

            if (barco == 0)
            {
                tipoBarco = subPlayer;
            }
            else if (barco == 1)
            {
                tipoBarco = encouraPlayer;
            }
            else if (barco == 2)
            {
                tipoBarco = encoura1Player;
            }
            else if (barco == 3)
            {
                tipoBarco = navioGPlayer;
            }
            else
            {
                tipoBarco = portaPlayer;
            }

            for (int i = 0; i < tipoBarco.getTamanho(); i++)
            {
                if (orientacao)
                {
                    if ((x + tipoBarco.getTamanho()) > tabPlayer.getTamanhoTab() ||
                    tabPlayer.getMatrizPecas()[x + i, y].GetType() != typeof(Agua))
                    {
                        validarPosicao = false;
                        break;
                    }
                }
                else
                {
                    if ((y + tipoBarco.getTamanho()) > tabPlayer.getTamanhoTab() ||
                    tabPlayer.getMatrizPecas()[x, y + i].GetType() != typeof(Agua))
                    {
                        validarPosicao = false;
                        break;
                    }
                }
            }

            if (validarPosicao)
            {
                for (int i = 0; i < tipoBarco.getTamanho(); i++)
                {
                    if (orientacao)
                    {
                        tabPlayer.getMatrizPecas()[x + i, y] = tipoBarco;
                    }
                    else
                    {
                        tabPlayer.getMatrizPecas()[x, y + i] = tipoBarco;
                    }
                }
            }

            tipoBarco.setOrientacao(orientacao);
            tipoBarco.setXInicial(x);
            tipoBarco.setYInicial(y);
            return validarPosicao;
        }

        public bool jogarPlayer(int x, int y)
        {
            bool acertou = false;

            Peca barco = getTabIA().getMatrizPecas()[x, y];
            int tamanhoBarco = barco.getTamanho();

            jogadasPlayerEixoX.Add(x);
            jogadasPlayerEixoY.Add(y);

            if (barco.GetType() != typeof(Agua) && !barco.getAtingido())
            {
                getTabIA().getMatrizPecas()[x, y].setTamanho(tamanhoBarco - 1);

                tamanhoBarco = getTabIA().getMatrizPecas()[x, y].getTamanho();

                if (tamanhoBarco <= 0)
                {
                    getTabIA().getMatrizPecas()[x, y].setAtingido(true);
                }

                getTabIA().getMatrizPecas()[x, y] = new TiroBarco(true);
                acertou = true;
            }
            else if (barco.getAtingido())
            {
                acertou = true;
            }
            else
            {
                getTabIA().getMatrizPecas()[x, y] = new TiroAgua(true);
                acertou = false;
            }

            if (tamanhoBarco <= 0)
            {
                getTabIA().getMatrizPecas()[x, y] = new TiroBarco(true);
                countBarcosIA -= 1;
            }
            return acertou;
        }

        public bool jogarIA()
        {
            return ia.atirar(tabPlayer);
        }

        public IA getIA()
        {
            return this.ia;
        }

        public Tabuleiro getTabIA()
        {
            return this.ia.getTabIA();
        }

        public Tabuleiro getTabPlayer()
        {
            return this.tabPlayer;
        }

        public int getCountBarcosIA()
        {
            return this.countBarcosIA;
        }

        public int getCountBarcosPlayer()
        {
            return getIA().getCountBarcosPlayer();
        }

        public int[] getBarcos()
        {
            return this.barcos;
        }
    }
}
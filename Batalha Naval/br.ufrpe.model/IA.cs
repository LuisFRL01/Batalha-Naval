using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Naval.br.ufrpe.model
{
    public class IA
    {

        private Random r = new Random();

        private List<int> ultimasPosicaosBarcoAtingidasX = new List<int>();
        private List<int> ultimasPosicaosBarcoAtingidasY = new List<int>();
        private List<int> jogadasIAEixoX = new List<int>();
        private List<int> jogadasIAEixoY = new List<int>();

        private int countBarcosPlayer;
        private int tamanhoTab;

        private Tabuleiro tabIA;

        private List<Peca> barcosIA = new List<Peca>();

        private Submarino subIA = new Submarino(true);
        private Encouracado encouraIA = new Encouracado(true);
        private Encouracado encoura1IA = new Encouracado(true);
        private NavioGuerra navioGIA = new NavioGuerra(true);
        private PortaAviao portaIA = new PortaAviao(true);

        enum direcaoCerta { nada, x_Plus, x_Minus, y_Plus, y_Minus }
        direcaoCerta lastDir = direcaoCerta.x_Plus;

        public IA(int countBarcosPlayer, int tamanhoTab)
        {
            this.countBarcosPlayer = countBarcosPlayer;
            this.tamanhoTab = tamanhoTab;
            tabIA = new Tabuleiro(this.tamanhoTab);
        }

        private void addBarcosListIA()
        {
            barcosIA.Add(subIA);
            barcosIA.Add(encouraIA);
            barcosIA.Add(encoura1IA);
            barcosIA.Add(navioGIA);
            barcosIA.Add(portaIA);
        }

        private bool validarXYBarcoIA(Peca barco, bool orientacao, int x, int y)
        {

            bool validarPosicao = true;

            for (int i = 0; i < barco.getTamanho(); i++)
            {
                if (orientacao)
                {
                    if ((x + barco.getTamanho()) > tabIA.getTamanhoTab() ||
                    tabIA.getMatrizPecas()[x + i, y].GetType() != typeof(Agua))
                    {
                        validarPosicao = false;
                        break;
                    }
                }
                else
                {
                    if ((y + barco.getTamanho()) > tabIA.getTamanhoTab() ||
                    tabIA.getMatrizPecas()[x, y + i].GetType() != typeof(Agua))
                    {
                        validarPosicao = false;
                        break;
                    }
                }
            }

            if (validarPosicao)
            {
                for (int i = 0; i < barco.getTamanho(); i++)
                {
                    if (orientacao)
                    {
                        tabIA.getMatrizPecas()[x + i, y] = barco;
                        //Console.WriteLine("true x" + (x + i) + " - y:" + y);
                    }
                    else
                    {
                        tabIA.getMatrizPecas()[x, y + i] = barco;
                        //Console.WriteLine("false x" + x + " - y:" + (y + i));
                    }
                }
            }

            barco.setOrientacao(orientacao);
            barco.setXInicial(x);
            barco.setYInicial(y);

            return validarPosicao;
        }

        public void posicionarBarcosIA()
        {
            addBarcosListIA();

            bool[] arayOrientacao = new bool[2];
            arayOrientacao[0] = true;
            arayOrientacao[1] = false;

            int indexOrientacao;
            int xRandom;
            int yRandom;

            for (int i = 0; i < barcosIA.Count; i++)
            {
                while (true)
                {
                    bool check = false;

                    indexOrientacao = r.Next(0, 2);
                    xRandom = r.Next(0, tamanhoTab - 1);
                    yRandom = r.Next(0, tamanhoTab - 1);

                    check = validarXYBarcoIA(barcosIA[i], arayOrientacao[indexOrientacao], xRandom, yRandom);

                    if (check)
                    {
                        Console.WriteLine("Começa em x" + xRandom + " - y:" + yRandom + " bool " + arayOrientacao[indexOrientacao]);
                        break;
                    }
                }
            }
        }

        private (int, int) decidirXY(Tabuleiro tabPlayer)
        {
            int eixoX = 0;
            int eixoY = 0;

            while (true)
            {
                if (jogadasIAEixoX.Count == 0)
                {
                    eixoX = r.Next(2, 8);
                    eixoY = r.Next(2, 7);
                }
                else
                {
                    eixoX = r.Next(0, 10);
                    eixoY = r.Next(0, 10);
                }

                if (ultimasPosicaosBarcoAtingidasX.Count > 0)
                {
                    eixoX = ultimasPosicaosBarcoAtingidasX[ultimasPosicaosBarcoAtingidasX.Count - 1];
                    eixoY = ultimasPosicaosBarcoAtingidasY[ultimasPosicaosBarcoAtingidasY.Count - 1];

                    if (lastDir.Equals(direcaoCerta.x_Plus))
                    {
                        if (eixoX + 1 < 10 && !tabPlayer.getMatrizPecas()[eixoX + 1, eixoY].getAtingido())
                        {
                            eixoX++;
                        }
                        else
                        {
                            lastDir = direcaoCerta.x_Minus;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.x_Minus))
                    {
                        if (eixoX - 1 >= 0 && !tabPlayer.getMatrizPecas()[eixoX - 1, eixoY].getAtingido())
                        {
                            eixoX--;
                        }
                        else
                        {
                            lastDir = direcaoCerta.y_Plus;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.y_Plus))
                    {
                        if (eixoY + 1 < 10 && !tabPlayer.getMatrizPecas()[eixoX, eixoY + 1].getAtingido())
                        {
                            eixoY++;
                        }
                        else
                        {
                            lastDir = direcaoCerta.y_Minus;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.y_Minus))
                    {
                        if (eixoY - 1 >= 0 && !tabPlayer.getMatrizPecas()[eixoX, eixoY - 1].getAtingido())
                        {
                            eixoY--;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.nada))
                    {
                        ultimasPosicaosBarcoAtingidasX.RemoveRange(1, ultimasPosicaosBarcoAtingidasX.Count - 1);
                        ultimasPosicaosBarcoAtingidasY.RemoveRange(1, ultimasPosicaosBarcoAtingidasY.Count - 1);
                    }
                }

                bool checkPosicaoTiro = true;
                for (int i = 0; i < jogadasIAEixoX.Count; i++)
                {
                    if (jogadasIAEixoX[i] == eixoX && jogadasIAEixoY[i] == eixoY)
                    {
                        checkPosicaoTiro = false;
                        break;
                    }
                }

                if (!tabPlayer.getMatrizPecas()[eixoX, eixoY].getAtingido() && checkPosicaoTiro)
                {
                    break;
                }
                else
                {
                    if (lastDir.Equals(direcaoCerta.y_Minus))
                    {
                        lastDir = direcaoCerta.nada;
                    }
                    else
                    {
                        lastDir++;
                    }
                }
            }
            return (eixoX, eixoY);
        }

        public bool atirar(Tabuleiro tabPlayer)
        {
            bool acertou = false;
            var (eixoXVar, eixoYVar) = decidirXY(tabPlayer);

            int eixoX = eixoXVar;
            int eixoY = eixoYVar;

            Peca barco = tabPlayer.getMatrizPecas()[eixoX, eixoY];
            int tamanhoBarco = barco.getTamanho();

            jogadasIAEixoX.Add(eixoX);
            jogadasIAEixoY.Add(eixoY);

            if (barco.GetType() != typeof(Agua))
            {
                tabPlayer.getMatrizPecas()[eixoX, eixoY].setTamanho(tamanhoBarco - 1);
                tamanhoBarco = tabPlayer.getMatrizPecas()[eixoX, eixoY].getTamanho();

                if (tamanhoBarco <= 0)
                {
                    tabPlayer.getMatrizPecas()[eixoX, eixoY].setAtingido(true);
                }

                tabPlayer.getMatrizPecas()[eixoX, eixoY] = new TiroBarco(true);
                ultimasPosicaosBarcoAtingidasX.Add(eixoX);
                ultimasPosicaosBarcoAtingidasY.Add(eixoY);
                acertou = true;
            }
            else
            {
                tabPlayer.getMatrizPecas()[eixoX, eixoY] = new TiroAgua(true);
                acertou = false;
            }

            if (tamanhoBarco <= 0)
            {
                tabPlayer.getMatrizPecas()[eixoX, eixoY] = new TiroBarco(true);
                countBarcosPlayer -= 1;
                ultimasPosicaosBarcoAtingidasX.Clear();
                ultimasPosicaosBarcoAtingidasY.Clear();
                lastDir = direcaoCerta.x_Plus;
            }
            return acertou;
        }

        public int getCountBarcosPlayer()
        {
            return this.countBarcosPlayer;
        }

        public Tabuleiro getTabIA()
        {
            return this.tabIA;
        }
    }
}
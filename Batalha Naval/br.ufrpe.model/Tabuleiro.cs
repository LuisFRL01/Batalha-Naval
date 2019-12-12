using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Naval.br.ufrpe.model
{
    public  class Tabuleiro
    {
        
        private int tamanhoTab;
        private Peca[,] matrizPecas;//pode ser atributo estático?

        public Tabuleiro(int tamanhoTab = 10)
        {
            this.tamanhoTab = tamanhoTab;
            this.matrizPecas = gerarMatriz();
        }

        public int getTamanhoTab()
        {
            return this.tamanhoTab;
        }

        public void setTamanhoTab(int tamanhoTab)
        {
            this.tamanhoTab = tamanhoTab;
        }

        public Peca[,] getMatrizPecas()
        {
            return this.matrizPecas;
        }

        public void setMatrizPecas(Peca[,] pecas)
        {
            this.matrizPecas = pecas;
        }

        public Peca[,] gerarMatriz()
        {
            this.matrizPecas = new Peca[this.tamanhoTab, this.tamanhoTab];
            for (int i = 0; i < tamanhoTab; i++)
            {
                for (int j = 0; j < tamanhoTab; j++)
                {
                    Peca agua = new Agua(true);
                    matrizPecas[i, j] = agua;
                }
            }            
            return matrizPecas;
        }
    
    public override string ToString()
        {
            return "Tamnho do tabuleiro: " + this.tamanhoTab+ "\nTabuleiro:" + this.matrizPecas;
        }
    }

}

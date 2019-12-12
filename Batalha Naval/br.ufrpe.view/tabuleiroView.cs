using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.Window;
using Batalha_Naval.br.ufrpe.model;

namespace Batalha_Naval.br.ufrpe.view
{
    public class tabuleiroView
    {        

        //Informações para os loops
        Tabuleiro tab = new Tabuleiro();
        private int tamanhoTab;
        private int barco = -1;
        private bool setup = true;
        private bool orientacao = true; //true horizontal / false vertical

        //Textures
        public Texture aguaTexture = new Texture("Assets/quadrado.png");
        public Texture submarinoTexture = new Texture("Assets/submarino.png");
        public Texture encouracadoTexture = new Texture("Assets/encouracado.png");
        public Texture navioGuerraTexture = new Texture("Assets/navioguerra.png");
        public Texture portaAvioesTexture = new Texture("Assets/portaavioes.png");
        public Texture tiroNaAguaTexture = new Texture("Assets/tironaagua.png");
        public Texture tiroNoBarcoTexture = new Texture("Assets/tironobarco.png");

        //Font
        Font arial = new Font("Font/arial.ttf");

        public void desenharTabuleiroPlayer(RenderWindow janela, int posX, int posY)
        {
            for (int i = 0; i < tab.getTamanhoTab(); i++)
            {                
                char caracter = Convert.ToChar('A' + i);
                Text letrasColuna = new Text(caracter.ToString(), arial);
                letrasColuna.Position = new SFML.System.Vector2f((posX + 10) + i * 40, posY-40);
                letrasColuna.Color = Color.Red;
                janela.Draw(letrasColuna);
                for (int j = 0; j < tab.getTamanhoTab(); j++)
                {
                    Text numerosLinha = new Text((j+1).ToString(), arial);
                    numerosLinha.Position = new SFML.System.Vector2f(posX - 40, (posY) + j * 40);
                    numerosLinha.Color = Color.Red;
                    janela.Draw(numerosLinha);
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(Agua));
                    {
                        Sprite aguaSprite = new Sprite(aguaTexture, new IntRect(0, 0, 40, 40));
                        aguaSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(aguaSprite);
                    }

                    //TiroNaAgua 1x1
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(TiroAgua) && tab.getMatrizPecas()[i, j].getOrientacao() == true)
                    {
                        Sprite tiroNaAguaSprite = new Sprite(tiroNaAguaTexture, new IntRect(0, 0, 40, 40));
                        tiroNaAguaSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(tiroNaAguaSprite);
                    }

                    //TiroNoBarco 1x1
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(TiroBarco) && tab.getMatrizPecas()[i, j].getOrientacao() == true)
                    {
                        Sprite tiroNoBarcoSprite = new Sprite(tiroNoBarcoTexture, new IntRect(0, 0, 40, 40));
                        tiroNoBarcoSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(tiroNoBarcoSprite);
                    }

                    //Submarino 1x1
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(Submarino) && tab.getMatrizPecas()[i,j].getOrientacao() == true)
                    {
                        Sprite submarinoSprite = new Sprite(submarinoTexture, new IntRect(0, 0, 40, 40));
                        submarinoSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(submarinoSprite);
                    } else if(tab.getMatrizPecas()[i, j].GetType() == typeof(Submarino) && tab.getMatrizPecas()[i, j].getOrientacao() == false)
                    {
                        Sprite submarinoSprite = new Sprite(submarinoTexture, new IntRect(0, 0, 40, 40));
                        submarinoSprite.Position = new SFML.System.Vector2f(posX + i * 40 + 40, posY + j * 40);
                        submarinoSprite.Rotation = 90;
                        janela.Draw(submarinoSprite);
                    }                    

                    //Encouraçado 1x3                    
                    if (tab.getMatrizPecas()[i,j].GetType() == typeof(Encouracado) && tab.getMatrizPecas()[i, j].getOrientacao() == true)
                    {
                        Sprite encouracadoSprite = new Sprite(encouracadoTexture, new IntRect(0, 0, 120, 40));
                        encouracadoSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i,j].getXInicial() * 40, posY + j * 40);
                        janela.Draw(encouracadoSprite);

                    } else if(tab.getMatrizPecas()[i, j].GetType() == typeof(Encouracado) && tab.getMatrizPecas()[i, j].getOrientacao() == false)
                    {                      
                        Sprite encouracadoSprite = new Sprite(encouracadoTexture, new IntRect(0, 0, 120, 40));
                        encouracadoSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40 + 40, posY + tab.getMatrizPecas()[i, j].getYInicial() * 40);
                        encouracadoSprite.Rotation = 90;
                        janela.Draw(encouracadoSprite);                        
                    }

                    //Navio de Guerra
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(NavioGuerra) && tab.getMatrizPecas()[i, j].getOrientacao() == true)
                    {                        
                        Sprite navioGuerraSprite = new Sprite(navioGuerraTexture, new IntRect(0, 0, 160, 40));
                        navioGuerraSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40, posY + j * 40);
                        janela.Draw(navioGuerraSprite);                        
                    }
                    else if (tab.getMatrizPecas()[i, j].GetType() == typeof(NavioGuerra) && tab.getMatrizPecas()[i, j].getOrientacao() == false)
                    {                        
                        Sprite navioGuerraSprite = new Sprite(navioGuerraTexture, new IntRect(0, 0, 160, 40));
                        navioGuerraSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40 + 40, posY + tab.getMatrizPecas()[i, j].getYInicial() * 40);
                        navioGuerraSprite.Rotation = 90;
                        janela.Draw(navioGuerraSprite);                        
                    }

                    //Porta Aviões
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(PortaAviao) && tab.getMatrizPecas()[i, j].getOrientacao() == true)
                    {
                        Sprite portaAvioesSprite = new Sprite(portaAvioesTexture, new IntRect(0, 0, 240, 40));
                        portaAvioesSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40, posY + j * 40);
                        janela.Draw(portaAvioesSprite);                            
                    }
                    else if (tab.getMatrizPecas()[i, j].GetType() == typeof(PortaAviao) && tab.getMatrizPecas()[i, j].getOrientacao() == false)
                    {                           
                        Sprite portaAvioesSprite = new Sprite(portaAvioesTexture, new IntRect(0, 0, 240, 40));
                        portaAvioesSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40 + 40, posY + tab.getMatrizPecas()[i, j].getYInicial() * 40);
                        portaAvioesSprite.Rotation = 90;
                        janela.Draw(portaAvioesSprite);                    
                    }                    
                }
            }                    
        }

        public void desenharTabuleiroIA(RenderWindow janela, int posX, int posY)
        {
            for (int i = 0; i < tab.getTamanhoTab(); i++)
            {
                char caracter = Convert.ToChar('A' + i);
                Text letrasColuna = new Text(caracter.ToString(), arial);
                letrasColuna.Position = new SFML.System.Vector2f((posX + 10) + i * 40, posY - 40);
                letrasColuna.Color = Color.Red;
                janela.Draw(letrasColuna);
                for (int j = 0; j < tab.getTamanhoTab(); j++)
                {
                    Text numerosLinha = new Text((j + 1).ToString(), arial);
                    numerosLinha.Position = new SFML.System.Vector2f(posX - 40, (posY) + j * 40);
                    numerosLinha.Color = Color.Red;
                    janela.Draw(numerosLinha);
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(Agua)) ;
                    {
                        Sprite aguaSprite = new Sprite(aguaTexture, new IntRect(0, 0, 40, 40));
                        aguaSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(aguaSprite);
                    }

                    //TiroNaAgua 1x1
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(TiroAgua) && tab.getMatrizPecas()[i, j].getOrientacao() == true)
                    {
                        Sprite tiroNaAguaSprite = new Sprite(tiroNaAguaTexture, new IntRect(0, 0, 40, 40));
                        tiroNaAguaSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(tiroNaAguaSprite);
                    }

                    //TiroNoBarco 1x1
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(TiroBarco) && tab.getMatrizPecas()[i, j].getOrientacao() == true)
                    {
                        Sprite tiroNoBarcoSprite = new Sprite(tiroNoBarcoTexture, new IntRect(0, 0, 40, 40));
                        tiroNoBarcoSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(tiroNoBarcoSprite);
                    }

                    //Submarino 1x1
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(Submarino) && tab.getMatrizPecas()[i, j].getOrientacao() == true
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite submarinoSprite = new Sprite(submarinoTexture, new IntRect(0, 0, 40, 40));
                        submarinoSprite.Position = new SFML.System.Vector2f(posX + i * 40, posY + j * 40);
                        janela.Draw(submarinoSprite);
                    }
                    else if (tab.getMatrizPecas()[i, j].GetType() == typeof(Submarino) && tab.getMatrizPecas()[i, j].getOrientacao() == false
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite submarinoSprite = new Sprite(submarinoTexture, new IntRect(0, 0, 40, 40));
                        submarinoSprite.Position = new SFML.System.Vector2f(posX + i * 40 + 40, posY + j * 40);
                        submarinoSprite.Rotation = 90;
                        janela.Draw(submarinoSprite);
                    }
                    //Encouraçado 1x3                    
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(Encouracado) && tab.getMatrizPecas()[i, j].getOrientacao() == true
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite encouracadoSprite = new Sprite(encouracadoTexture, new IntRect(0, 0, 120, 40));
                        encouracadoSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40, posY + j * 40);
                        janela.Draw(encouracadoSprite);

                    }
                    else if (tab.getMatrizPecas()[i, j].GetType() == typeof(Encouracado) && tab.getMatrizPecas()[i, j].getOrientacao() == false
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite encouracadoSprite = new Sprite(encouracadoTexture, new IntRect(0, 0, 120, 40));
                        encouracadoSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40 + 40, posY + tab.getMatrizPecas()[i, j].getYInicial() * 40);
                        encouracadoSprite.Rotation = 90;
                        janela.Draw(encouracadoSprite);
                    }

                    //Navio de Guerra
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(NavioGuerra) && tab.getMatrizPecas()[i, j].getOrientacao() == true
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite navioGuerraSprite = new Sprite(navioGuerraTexture, new IntRect(0, 0, 160, 40));
                        navioGuerraSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40, posY + j * 40);
                        janela.Draw(navioGuerraSprite);
                    }
                    else if (tab.getMatrizPecas()[i, j].GetType() == typeof(NavioGuerra) && tab.getMatrizPecas()[i, j].getOrientacao() == false
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite navioGuerraSprite = new Sprite(navioGuerraTexture, new IntRect(0, 0, 160, 40));
                        navioGuerraSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40 + 40, posY + tab.getMatrizPecas()[i, j].getYInicial() * 40);
                        navioGuerraSprite.Rotation = 90;
                        janela.Draw(navioGuerraSprite);
                    }

                    //Porta Aviões
                    if (tab.getMatrizPecas()[i, j].GetType() == typeof(PortaAviao) && tab.getMatrizPecas()[i, j].getOrientacao() == true
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite portaAvioesSprite = new Sprite(portaAvioesTexture, new IntRect(0, 0, 240, 40));
                        portaAvioesSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40, posY + j * 40);
                        janela.Draw(portaAvioesSprite);
                    }
                    else if (tab.getMatrizPecas()[i, j].GetType() == typeof(PortaAviao) && tab.getMatrizPecas()[i, j].getOrientacao() == false
                        && tab.getMatrizPecas()[i, j].getAtingido().Equals(true))
                    {
                        Sprite portaAvioesSprite = new Sprite(portaAvioesTexture, new IntRect(0, 0, 240, 40));
                        portaAvioesSprite.Position = new SFML.System.Vector2f(posX + tab.getMatrizPecas()[i, j].getXInicial() * 40 + 40, posY + tab.getMatrizPecas()[i, j].getYInicial() * 40);
                        portaAvioesSprite.Rotation = 90;
                        janela.Draw(portaAvioesSprite);

                    }
                }
            }
        }

        public void desenharBarraBarcos(RenderWindow janela, int posX, int posY)
        {
            Sprite submarinoSprite = new Sprite(submarinoTexture, new IntRect(0, 0, 40, 40));
            submarinoSprite.Position = new SFML.System.Vector2f(posX, posY);
            janela.Draw(submarinoSprite);

            Sprite encouracadoSprite = new Sprite(encouracadoTexture, new IntRect(0, 0, 120, 40));
            encouracadoSprite.Position = new SFML.System.Vector2f(posX, posY + 50);
            janela.Draw(encouracadoSprite);

            Sprite encouracado1Sprite = new Sprite(navioGuerraTexture, new IntRect(0, 0, 120, 40));
            encouracadoSprite.Position = new SFML.System.Vector2f(posX, posY + 100);
            janela.Draw(encouracadoSprite);

            Sprite navioGuerraSprite = new Sprite(navioGuerraTexture, new IntRect(0, 0, 160, 40));
            navioGuerraSprite.Position = new SFML.System.Vector2f(posX, posY + 150);
            janela.Draw(navioGuerraSprite);

            Sprite portaAvioesSprite = new Sprite(portaAvioesTexture, new IntRect(0, 0, 240, 40));
            portaAvioesSprite.Position = new SFML.System.Vector2f(posX, posY + 200);
            janela.Draw(portaAvioesSprite);

            Text orientacao = new Text("Horizontal", arial);
            if (this.orientacao.Equals(true))
            {
                orientacao = new Text("Horizontal", arial);
            } else
            {
                orientacao = new Text("Vertical", arial);
            }
            
            orientacao.Position = new SFML.System.Vector2f(posX, posY + 250);
            orientacao.Color = Color.Red;
            janela.Draw(orientacao);

        }

        public bool getSetup()
        {
            return this.setup;
        }

        public void changeSetup()
        {
            this.setup = !(setup);
        }

        public int getTamanhoTab()
        {
            return this.tamanhoTab = tab.getTamanhoTab();
        }

        public void changeOrientacao()
        {
            this.orientacao = !(orientacao);
        }

        public bool getOrientacao()
        {
            return this.orientacao;
        }

        public Tabuleiro getTabuleiro()
        {
            return this.tab;
        }

        public void setTabuleiro(Tabuleiro tab)
        {
            this.tab = tab;
        }

        public void setBarco(int barco)
        {
            this.barco = barco;
        }
        public int getBarco()
        {
            return this.barco;
        }
    }
}

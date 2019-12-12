using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Window;
using SFML.Graphics;
using Batalha_Naval;

namespace Batalha_Naval.br.ufrpe.view
{
    class Interface
    {
        TelaInicial telaInicial;

        static tabuleiroView tabuleiroIA = new tabuleiroView();
        static tabuleiroView tabuleiroPlayer = new tabuleiroView();

        private ControllerJogo controladorJogo = new ControllerJogo(tabuleiroPlayer.getTabuleiro());

        private bool jogadaPlayer = true;
        private bool jogadaCPU = false;

        private bool jogo = true;

        private String vencedor;

        //Atributos da Tela
        static uint width = VideoMode.DesktopMode.Width;
        static uint height = VideoMode.DesktopMode.Height;
        static RenderWindow janela = new RenderWindow(new VideoMode(width, height), "Batalha Naval", Styles.Fullscreen);

        //static void Main(string[] args)
        public void Partida()
        {
            janela.Closed += Janela_Closed;
            Texture bgTexture = new Texture("Assets/background.jpg");
            Sprite bgSprite = new Sprite(bgTexture);
            Sprite submarinoSprite = new Sprite(tabuleiroPlayer.submarinoTexture);
            Sprite encouracadoSprite = new Sprite(tabuleiroPlayer.encouracadoTexture);
            Sprite navioGuerraSprite = new Sprite(tabuleiroPlayer.navioGuerraTexture);
            Sprite portaAvioesSprite = new Sprite(tabuleiroPlayer.portaAvioesTexture);

            Font arial = new Font("Font/arial.ttf");

            while (janela.IsOpen && jogo)
            {
                //loop do setup
                while (tabuleiroPlayer.getSetup().Equals(true))
                {
                    janela.DispatchEvents();
                    janela.Draw(bgSprite);
                    tabuleiroPlayer.desenharTabuleiroPlayer(janela, 80, 160);
                    tabuleiroPlayer.desenharBarraBarcos(janela, (int)width / 2, 160);
                    changeOrientacao();
                    janela.Display();
                    getBarcoPlayer();
                    //Loop selecionar e posicionar os barcos
                    while (tabuleiroPlayer.getBarco() != -1)
                    {
                        janela.DispatchEvents();
                        janela.Draw(bgSprite);
                        tabuleiroPlayer.desenharTabuleiroPlayer(janela, 80, 160);
                        tabuleiroPlayer.desenharBarraBarcos(janela, (int)width / 2, 160);
                        changeOrientacao();
                        if (tabuleiroPlayer.getBarco() == 0)
                        {
                            if (tabuleiroPlayer.getOrientacao().Equals(true))
                            {
                                submarinoSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X - 20, Mouse.GetPosition().Y - 20);
                                submarinoSprite.Rotation = 0;
                            }
                            if (tabuleiroPlayer.getOrientacao().Equals(false))
                            {
                                submarinoSprite.Rotation = 90;
                                submarinoSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X + 20, Mouse.GetPosition().Y - 20);
                            }
                            janela.Draw(submarinoSprite);
                        }
                        else if (tabuleiroPlayer.getBarco() == 1)
                        {

                            if (tabuleiroPlayer.getOrientacao().Equals(true))
                            {
                                encouracadoSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X - 20, Mouse.GetPosition().Y - 20);
                                encouracadoSprite.Rotation = 0;
                            }
                            if (tabuleiroPlayer.getOrientacao().Equals(false))
                            {
                                encouracadoSprite.Rotation = 90;
                                encouracadoSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X + 20, Mouse.GetPosition().Y - 20);
                            }
                            janela.Draw(encouracadoSprite);
                        }
                        else if (tabuleiroPlayer.getBarco() == 2)
                        {

                            if (tabuleiroPlayer.getOrientacao().Equals(true))
                            {
                                encouracadoSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X - 20, Mouse.GetPosition().Y - 20);
                                encouracadoSprite.Rotation = 0;
                            }
                            if (tabuleiroPlayer.getOrientacao().Equals(false))
                            {
                                encouracadoSprite.Rotation = 90;
                                encouracadoSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X + 20, Mouse.GetPosition().Y - 20);
                            }
                            janela.Draw(encouracadoSprite);
                        }
                        else if (tabuleiroPlayer.getBarco() == 3)
                        {

                            if (tabuleiroPlayer.getOrientacao().Equals(true))
                            {
                                navioGuerraSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X - 20, Mouse.GetPosition().Y - 20);
                                navioGuerraSprite.Rotation = 0;
                            }
                            if (tabuleiroPlayer.getOrientacao().Equals(false))
                            {
                                navioGuerraSprite.Rotation = 90;
                                navioGuerraSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X + 20, Mouse.GetPosition().Y - 20);
                            }
                            janela.Draw(navioGuerraSprite);
                        }
                        else if (tabuleiroPlayer.getBarco() == 4)
                        {

                            if (tabuleiroPlayer.getOrientacao().Equals(true))
                            {
                                portaAvioesSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X - 20, Mouse.GetPosition().Y - 20);
                                portaAvioesSprite.Rotation = 0;
                            }
                            if (tabuleiroPlayer.getOrientacao().Equals(false))
                            {
                                portaAvioesSprite.Rotation = 90;
                                portaAvioesSprite.Position = new SFML.System.Vector2f(Mouse.GetPosition().X + 20, Mouse.GetPosition().Y - 20);
                            }
                            janela.Draw(portaAvioesSprite);
                        }
                        janela.Display();
                        int linha = getLinhaPlayer();
                        int coluna = getColunaPlayer();
                        if (linha >= 0 && linha <= 9 && coluna >= 0 && coluna <= 9)
                        {
                            if (controladorJogo.posicionarBarcosPlayer(getPosicaoBarco(tabuleiroPlayer.getBarco(), tabuleiroPlayer.getOrientacao(), linha, coluna)))
                            {
                                controladorJogo.getBarcosSelecionaveis()[tabuleiroPlayer.getBarco()] -= 1;
                                tabuleiroPlayer.setTabuleiro(controladorJogo.tabPlayer());
                            }
                            tabuleiroPlayer.setBarco(-1);
                        }
                        if (controladorJogo.getBarcosSelecionaveis()[0] == 0
                            && controladorJogo.getBarcosSelecionaveis()[1] == 0
                            && controladorJogo.getBarcosSelecionaveis()[2] == 0
                            && controladorJogo.getBarcosSelecionaveis()[3] == 0
                            && controladorJogo.getBarcosSelecionaveis()[4] == 0)
                        {
                            controladorJogo.getIA().posicionarBarcosIA();
                            tabuleiroIA.setTabuleiro(controladorJogo.tabIa());
                            tabuleiroPlayer.changeSetup();
                        }
                    }
                }
                janela.DispatchEvents();
                janela.Draw(bgSprite);

                if (jogadaPlayer)
                {
                    int linhaIa = getLinhaIA();
                    int colunaIa = getColunaIA();
                    if (linhaIa >= 0 && linhaIa <= 9 && colunaIa >= 0 && colunaIa <= 9)
                    {
                        bool jogada = controladorJogo.jogarPlayer(colunaIa, linhaIa);
                        if (jogada)
                        {
                            tabuleiroIA.setTabuleiro(controladorJogo.tabIa());
                        }
                        else
                        {
                            tabuleiroIA.setTabuleiro(controladorJogo.tabIa());
                            jogadaCPU = true;
                            jogadaPlayer = false;
                        }
                        if (controladorJogo.countBarcosIA() == 0)
                        {
                            vencedor = "Player";
                            jogo = false;
                        }
                    }
                }
                else if (jogadaCPU)
                {
                    bool jogada = controladorJogo.jogarIA();
                    if (jogada)
                    {
                        tabuleiroPlayer.setTabuleiro(controladorJogo.tabPlayer());

                    }
                    else
                    {
                        tabuleiroPlayer.setTabuleiro(controladorJogo.tabPlayer());
                        jogadaPlayer = true;
                        jogadaCPU = false;
                    }
                    if (controladorJogo.countBarcosPlayer() == 0)
                    {
                        vencedor = "Computador";
                        jogo = false;
                    }
                }
                tabuleiroPlayer.desenharTabuleiroPlayer(janela, 80, 160);
                tabuleiroIA.desenharTabuleiroIA(janela, (int)width / 2 + 80, 160);
                janela.Display();
            }

            for (int x = 0; x < 1000; x++)
            {
                Text fimDeJogo;
                janela.DispatchEvents();
                janela.Draw(bgSprite);
                if (vencedor.Equals("Player"))
                {
                    fimDeJogo = new Text("O Vencedor Foi: " + vencedor + "\n\n Voltando Para Tela Inicial!", arial);
                    fimDeJogo.Position = new SFML.System.Vector2f((int)width / 2 - 250, (int)height - 200);
                    fimDeJogo.Color = Color.Magenta;
                }
                else
                {
                    fimDeJogo = new Text("O Vencedor Foi: " + vencedor + "\n\n  Voltando Para Tela Inicial!", arial);
                    fimDeJogo.Position = new SFML.System.Vector2f((int)width / 2 - 250, (int)height - 200);
                    fimDeJogo.Color = Color.Black;
                }

                tabuleiroPlayer.desenharTabuleiroPlayer(janela, 80, 160);
                tabuleiroIA.desenharTabuleiroIA(janela, (int)width / 2 + 80, 160);
                janela.Draw(fimDeJogo);
                janela.Display();
            }
            telaInicial = new TelaInicial();
            telaInicial.redenrizar();
            janela.Close();

        }

        private static SFML.System.Vector2i getClickPosition()
        {
            SFML.System.Vector2i posi = new SFML.System.Vector2i(-1, -1);
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                posi = Mouse.GetPosition(janela);

            }
            System.Threading.Thread.Sleep(10);
            return posi;
        }

        public int getColunaIA()
        {
            if (getClickPosition().X > (int)width / 2 + 80 && getClickPosition().X < (int)width / 2 + (tabuleiroPlayer.getTamanhoTab() * 40 + 80)
                && getClickPosition().Y > 160 && getClickPosition().Y < (tabuleiroPlayer.getTamanhoTab() * 40 + 160))
            {
                float posMatriz = (getClickPosition().X - ((int)width / 2) - 80) / 40;
                int posicao = -1;
                //Converte um float para String para então pegar apenas a primeira casa do numero;
                String a = posMatriz.ToString();
                //Converte um char para um Int, conversão direta de char para int não funciona.
                try
                {
                    posicao = Int32.Parse(a[0].ToString());
                }
                catch (Exception e)
                {
                    return -1;
                }
                return posicao;
            }
            else
            {
                return -1;
            }
        }

        public int getLinhaIA()
        {
            if (getClickPosition().X > (int)width / 2 + 80 && getClickPosition().X < (int)width / 2 + (tabuleiroPlayer.getTamanhoTab() * 40 + 80)
                && getClickPosition().Y > 160 && getClickPosition().Y < (tabuleiroPlayer.getTamanhoTab() * 40 + 160))
            {
                float posMatriz = (getClickPosition().Y - 160) / 40;
                int posicao = -1;
                //Converte um float para String para então pegar apenas a primeira casa do numero;
                String a = posMatriz.ToString();
                //Converte um char para um Int, conversão direta de char para int não funciona.
                try
                {
                    posicao = Int32.Parse(a[0].ToString());
                }
                catch (Exception e)
                {
                    return -1;
                }
                return posicao;
            }
            else
            {
                return -1;
            }
        }

        public int getColunaPlayer()
        {
            if (getClickPosition().X > 80 && getClickPosition().X < (tabuleiroPlayer.getTamanhoTab() * 40 + 80)
                && getClickPosition().Y > 160 && getClickPosition().Y < (tabuleiroPlayer.getTamanhoTab() * 40 + 160))
            {
                float posMatriz = (getClickPosition().X - 80) / 40;
                int posicao = -1;
                //Converte um float para String para então pegar apenas a primeira casa do numero;
                String a = posMatriz.ToString();
                //Converte um char para um Int, conversão direta de char para int não funciona.
                try
                {
                    posicao = Int32.Parse(a[0].ToString());
                }
                catch (Exception e)
                {
                    return -1;
                }
                return posicao;
            }
            else
            {
                return -1;
            }
        }

        public int getLinhaPlayer()
        {
            if (getClickPosition().X > 80 && getClickPosition().X < (tabuleiroPlayer.getTamanhoTab() * 40 + 80)
                && getClickPosition().Y > 160 && getClickPosition().Y < (tabuleiroPlayer.getTamanhoTab() * 40 + 160))
            {
                float posMatriz = (getClickPosition().Y - 160) / 40;
                int posicao = -1;
                //Converte um float para String para então pegar apenas a primeira casa do numero;
                String a = posMatriz.ToString();
                //Converte um char para um Int, conversão direta de char para int não funciona.
                try
                {
                    posicao = Int32.Parse(a[0].ToString());
                }
                catch (Exception e)
                {
                    return -1;
                }
                return posicao;
            }
            else
            {
                return -1;
            }
        }

        //Retorna as informações para o posicionamento do barco
        public String getPosicaoBarco(int barco, bool orientacao, int x, int y)
        {

            return barco + "," + orientacao.ToString() + "," + x + "," + y;
        }

        //Seleciona o barco para posicionar
        public void getBarcoPlayer()
        {
            if (getClickPosition().X > (int)width / 2 && getClickPosition().X < ((int)width / 2 + 240)
                && getClickPosition().Y > 160 && getClickPosition().Y < 400 && tabuleiroPlayer.getSetup().Equals(true))
            {
                if (getClickPosition().Y > 160 && getClickPosition().Y < 200
                    && getClickPosition().X > (int)width / 2 && getClickPosition().X < (int)width / 2 + 40
                    && controladorJogo.getBarcosSelecionaveis()[0] > 0)
                {
                    tabuleiroPlayer.setBarco(0); //Submarino
                }
                else if (getClickPosition().Y > 210 && getClickPosition().Y < 250
                    && getClickPosition().X > (int)width / 2 && getClickPosition().X < (int)width / 2 + 120
                    && controladorJogo.getBarcosSelecionaveis()[1] > 0)
                {
                    tabuleiroPlayer.setBarco(1); //Encouracado
                }
                else if (getClickPosition().Y > 260 && getClickPosition().Y < 300
                    && getClickPosition().X > (int)width / 2 && getClickPosition().X < (int)width / 2 + 120
                    && controladorJogo.getBarcosSelecionaveis()[2] > 0)
                {
                    tabuleiroPlayer.setBarco(2);//Encouracado1
                }
                else if (getClickPosition().Y > 310 && getClickPosition().Y < 350
                    && getClickPosition().X > (int)width / 2 && getClickPosition().X < (int)width / 2 + 160
                    && controladorJogo.getBarcosSelecionaveis()[3] > 0)
                {
                    tabuleiroPlayer.setBarco(3);//NavioDeGuerra
                }
                else if (getClickPosition().Y > 360 && getClickPosition().Y < 400
                    && getClickPosition().X > (int)width / 2 && getClickPosition().X < (int)width / 2 + 240
                    && controladorJogo.getBarcosSelecionaveis()[4] > 0)
                {
                    tabuleiroPlayer.setBarco(4); //PortaAvioes
                }
            }
            else
            {
                tabuleiroPlayer.setBarco(-1);
            }
        }

        public void changeOrientacao()
        {
            if (getClickPosition().X > (int)width / 2 && getClickPosition().X < ((int)width / 2 + 134)
                && getClickPosition().Y > 410 && getClickPosition().Y < 432 && tabuleiroPlayer.getSetup().Equals(true))
            {
                tabuleiroPlayer.changeOrientacao();
            }
        }

        private static void Janela_Closed(object sender, EventArgs e)
        {
            ((Window)sender).Close();
        }

    }
}
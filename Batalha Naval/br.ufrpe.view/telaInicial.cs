using System;
using System.Collections.Generic;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.Window;


namespace Batalha_Naval.br.ufrpe.view
{
    class TelaInicial
    {
        private Interface jogo;
        private TelaConfiguracao configuracao;
        RenderWindow window;
        Menu menu;
        Texture bgTexture;
        Sprite bgSprite;

        public TelaInicial()
        {
            this.window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height), "Batalha Naval");
            this.window.Closed += windowClosed;
            this.window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            this.bgTexture = new Texture("Assets/backgroundTelaInicial.jpg");       
            this.bgSprite = new Sprite(bgTexture);
            //Codigo para redimensionar a Imagem através da Sprite.Scale
            float ScaleX = (float)VideoMode.DesktopMode.Width / bgTexture.Size.X;
            float ScaleY = (float)VideoMode.DesktopMode.Height / bgTexture.Size.Y;
            bgSprite.Scale = new SFML.System.Vector2f(ScaleX, ScaleY);

            this.menu = new Menu(window.Size.X, window.Size.Y);
        }
        public void redenrizar()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.White);
                window.Draw(bgSprite);
                menu.draw(window);
                window.Display();                           
            }

        }
       private void windowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Close();
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            switch(e.Code)
            {
                case Keyboard.Key.Up:
                    menu.moveParaCima();
                    break;
                case Keyboard.Key.Down:
                    menu.moveParaBaixo();
                    break;
                case Keyboard.Key.Return:
                    switch (menu.getItemPressionado())
                    {
                        case 0:
                            jogo = new Interface();
                            window.Close();
                            jogo.Partida();                                                                                  
                            break;
                        case 1:
                            configuracao = new TelaConfiguracao();
                            configuracao.redenrizar();
                            window.Close();
                            break;
                        case 2:
                            window.Close();
                            break;

                    }                    
                    break;
            }
        }
    }
}

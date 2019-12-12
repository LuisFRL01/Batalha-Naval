using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Batalha_Naval.br.ufrpe.view
{
    class Menu
    {
        public const int NUMERO_MAXIMO_DE_ITENS = 3;

        private int itemSelecionado;
        private Font fonte;
        private Text[] itens = new Text[NUMERO_MAXIMO_DE_ITENS];

        public Menu(float width, float height)
        {
            this.fonte = new Font("Font/arial.ttf");
         
            this.itens[0] = new Text("Jogar", fonte);
            this.itens[0].Color = Color.Green;
            this.itens[0].Position = new Vector2f(width / 5, height / (NUMERO_MAXIMO_DE_ITENS + 1) * 1);

            this.itens[1] = new Text("Configurar", fonte);
            this.itens[1].Color = Color.Blue;
            this.itens[1].Position = new Vector2f(width / 5, height / (NUMERO_MAXIMO_DE_ITENS + 1) * 2);

            this.itens[2] = new Text("Sair", fonte);
            this.itens[2].Color = Color.Blue;
            this.itens[2].Position = new Vector2f(width / 5, height / (NUMERO_MAXIMO_DE_ITENS + 1) * 3);

            itemSelecionado = 0;
        }

        public void draw(RenderWindow window)
        {
            for(int i = 0; i < NUMERO_MAXIMO_DE_ITENS; i++)
            {
                window.Draw(itens[i]);
            }
        }

        public void moveParaCima()
        {
            if(itemSelecionado - 1 >= 0)
            {
                itens[itemSelecionado].Color = Color.Blue;
                itemSelecionado--;
                itens[itemSelecionado].Color = Color.Green;
            }
        }

        public void moveParaBaixo()
        {
            if (itemSelecionado + 1 < NUMERO_MAXIMO_DE_ITENS)
            {               
                itens[itemSelecionado].Color = Color.Blue;
                itemSelecionado++;
                itens[itemSelecionado].Color = Color.Green;
            }
        }
        
        public int getItemPressionado()
        {
            return itemSelecionado;
        }
    }
}

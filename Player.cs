using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplayer_Game_Sample
{
    class Player
    {
        public int id;
        public Color color;

        public int x, y;
        public const int width = 32, height = 32;


        public Player()
        {
            color = Color.Black;
            x = 0;
            y = 0;
        }


        public void Draw(Graphics g)
        {
            Pen pen = new Pen(color);

            var rect = new Rectangle(x, y, width, height);

            g.DrawRectangle(pen, rect);

        }


    }
}

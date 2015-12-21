using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        public void Update()
        {

        }


        public void Draw(Graphics g)
        {
            Brush col = new SolidBrush(color);
            var rect = new RectangleF(x, y, width, height);
            g.FillRectangle(col, rect);

            g.DrawString(id.ToString(),Constants.FONT, Brushes.White, new PointF(x, y));
        }

        public void Input(Keys key)
        {
            NetworkService service = new NetworkService();
            service.ClientSend();

            switch (key)
            {
                case Keys.Up:
                    y -= Constants.SPEED;
                    break;
                case Keys.Down:
                    y += Constants.SPEED;
                    break;
                case Keys.Left:
                    x -= Constants.SPEED;
                    break;
                case Keys.Right:
                    x += Constants.SPEED;
                    break;

            }
        }

    }
}

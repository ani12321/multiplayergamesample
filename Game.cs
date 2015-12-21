using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiplayer_Game_Sample
{
    public partial class Game : Form
    {
        Stopwatch timer = new Stopwatch();
        long interval = (long)TimeSpan.FromSeconds(1.0 / 30).TotalMilliseconds;
        long startTime;

        Graphics graphics, imgGraphics;

        int width, height;

        bool started = false;

        List<Player> players = new List<Player>();


        public Game()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;

            graphics = this.CreateGraphics();

            Player p = new Player();
            players.Add(p);
        }


        public void Loop()
        {
            timer.Start();

            while (this.Created)
            {
                startTime = timer.ElapsedMilliseconds;
                Update();
                Draw();
                while (timer.ElapsedMilliseconds - startTime < interval) ;
                Application.DoEvents();
                started = true;
            }
        }
        
        public void Update()
        {

        }

        public void Draw()
        {
            graphics.Clear(Color.White);

            foreach(var player in players)
                player.Draw(graphics);
            
    
            this.Invalidate();
        }


    }
}

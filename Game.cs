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
        
        int width, height;
        

        List<Player> players = new List<Player>();


        public Game()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.Draw);
        }

        public void Start()
        {
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            

            Player p = new Player();
            players.Add(p);

            Loop();
        }


        public void Loop()
        {
            timer.Start();

            while (this.Created)
            {
                startTime = timer.ElapsedMilliseconds;
                Update();

                Application.DoEvents();
                while (timer.ElapsedMilliseconds - startTime < interval);
            }
        }
        

        public void Update()
        {
            foreach (var player in players)
                player.Update();
        }

        public void Draw(object sender, PaintEventArgs e)
        {

            e.Graphics.Clear(Color.White);
            foreach (var player in players)
                player.Draw(e.Graphics);

            this.Invalidate();
            this.Update();

        }


    }
}

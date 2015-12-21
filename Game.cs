using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        Player PlayerServer = null;
        Player CurrentPlayer = null;

        public Game()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.Draw);
            this.KeyDown += new KeyEventHandler(this.Input);
        }

        public void Start()
        {
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            

            for (int i = 0; i<5; i++)
            {
                Player p = new Player();
                p.id = i;
                players.Add(p);
                p.x = i * 32;

                if (PlayerServer == null)
                    PlayerServer = p;
                if (CurrentPlayer == null)
                    CurrentPlayer = p;
            }
            

            Loop();
        }


        public void Loop()
        {
            timer.Start();

            while (this.Created)
            {
                startTime = timer.ElapsedMilliseconds;

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

            Update();
            e.Graphics.Clear(Color.White);
            foreach (var player in players)
                player.Draw(e.Graphics);

            this.Invalidate();

        }

        //global input
        public void Input(object sender, KeyEventArgs e)
        {
            CurrentPlayer.Input(e.KeyCode);

        }

    }
}

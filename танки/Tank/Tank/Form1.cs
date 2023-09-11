using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank.Domains;

namespace Tank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var graphic = CreateGraphics();
            Field.Set(600);
            CreateLevel();
            ClientSize = new Size(800, 600);
            DoubleBuffered = true;
            var player = new Player(new Point(120, 120), 100, Direction.Up, 5);  //тем кем вы управляете
            var game = new Game(GameStage.InProgress, player);

            var timer = new Timer();
            timer.Interval = 1;
            timer.Tick += (sender, args) =>
            {
                Field.ChangeCoordinateShots();
                foreach (var tank in Field.ListTanks)
                    tank.ChangeCoordinate();
                Invalidate();
            };
            timer.Start();

            Paint += (sender, args) =>
            {
                args.Graphics.FillRectangle(Brushes.Black, Field.Hitbox);
                PaintPlayer(args, player);
                foreach (var tank in Field.ListTanks)
                    PaintPlayer(args, tank);
                PaintShots(args);
                PaintObstacles(args);
            };

            KeyPress += (sender, args) =>
            {
                switch (args.KeyChar)
                {
                    case 'ц':
                    case 'w':
                        player.ChangeDirection(Direction.Up);
                        player.ChangeCoordinate();
                        break;
                    case 'в':
                    case 'd':
                        player.ChangeDirection(Direction.Right);
                        player.ChangeCoordinate();
                        break;
                    case 'ы':
                    case 's':
                        player.ChangeDirection(Direction.Down);
                        player.ChangeCoordinate();
                        break;
                    case 'ф':
                    case 'a':
                        player.ChangeDirection(Direction.Left);
                        player.ChangeCoordinate();
                        break;
                    case 'а':
                    case 'f':
                        GenerateShot(player);
                        break;
                }
            };
        }

        private void CreateLevel()
        {
            for (var i = 0; i < 525; i+= 105)
                Field.ListObstacles.Add(new Obstacle(new Point(i, 0), 100));
            Field.ListTanks.Add(new Enemy(new Point(300, 300), 100, Direction.Up, 2));
        }

        private void PaintObstacles(PaintEventArgs args)
        {
            foreach (var obstacle in Field.ListObstacles)
                args.Graphics.DrawImage(Properties.Resources.wall, obstacle.Сoordinate);
        }

        private void PaintPlayer(PaintEventArgs args, ITank player)
        {
            switch (player.CurrentDirection)
            {
                case Direction.Down:
                    args.Graphics.DrawImage(Properties.Resources.down, player.CurrentСoordinate);
                    break;
                case Direction.Up:
                    args.Graphics.DrawImage(Properties.Resources.up, player.CurrentСoordinate);
                    break;
                case Direction.Right:
                    args.Graphics.DrawImage(Properties.Resources.right, player.CurrentСoordinate);
                    break;
                case Direction.Left:
                    args.Graphics.DrawImage(Properties.Resources.left, player.CurrentСoordinate);
                    break;
            }
        }

        private void GenerateShot(Player player)
        {
            switch (player.CurrentDirection)
            {
                case Direction.Up:
                    Field.AddShot(new Shot(4, player.CurrentDirection, 50, 
                        new Point(player.CurrentСoordinate.X + player.Size.Width / 2 - 5, 
                                  player.CurrentСoordinate.Y)));
                    break;
                case Direction.Right:
                    Field.AddShot(new Shot(4, player.CurrentDirection, 50,
                        new Point(player.CurrentСoordinate.X + player.Size.Width, 
                                  player.CurrentСoordinate.Y + player.Size.Height / 2 - 5)));
                    break;
                case Direction.Down:
                    Field.AddShot(new Shot(4, player.CurrentDirection, 50,
                        new Point(player.CurrentСoordinate.X + player.Size.Width / 2 - 9, 
                                  player.CurrentСoordinate.Y + player.Size.Height)));
                    break;
                case Direction.Left:
                    Field.AddShot(new Shot(4, player.CurrentDirection, 50,
                        new Point(player.CurrentСoordinate.X, 
                                  player.CurrentСoordinate.Y + player.Size.Height / 2 - 9)));
                    break;
            }
            
        }

        private void PaintShots(PaintEventArgs args)
        {
            foreach (var shot in Field.ListShots)
                switch (shot.CurrentDirection)
                {
                    case Direction.Up:
                        args.Graphics.DrawImage(Properties.Resources.shotUp, shot.Coordinate);
                        break;
                    case Direction.Down:
                        args.Graphics.DrawImage(Properties.Resources.shotDown, shot.Coordinate);
                        break;
                    case Direction.Left:
                        args.Graphics.DrawImage(Properties.Resources.shotLeft, shot.Coordinate);
                        break;
                    case Direction.Right:
                        args.Graphics.DrawImage(Properties.Resources.shotRight, shot.Coordinate);
                        break;
                }
                    
        }  
    }
}

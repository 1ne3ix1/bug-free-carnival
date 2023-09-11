using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Domains
{
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    public enum GameStage
    {
        NotStarted,
        InProgress
    }

    class Game
    {
        public GameStage Stage { get; set; }
        public Player CurrentPlayer { get; set; }

        public Game(GameStage stage, Player player)
        {
            Stage = stage;
            CurrentPlayer = player;
        }
    }
}

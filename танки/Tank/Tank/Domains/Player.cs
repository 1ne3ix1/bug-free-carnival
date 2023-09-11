using System;
using System.Drawing;
using System.Linq;

namespace Tank.Domains
{
    class Player : ITank
    {
        public Point CurrentСoordinate { get; private set; }
        public int Speed { get; }
        public int Health { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public Size Size { get; }
        public Rectangle HitBox { get; private set; }

        public Player(Point originPoint, int health, Direction direction, int speed)
        {
            CurrentСoordinate = originPoint;
            Health = health;
            CurrentDirection = direction;
            Speed = speed;
            Size = new Size(105, 105);
            HitBox = new Rectangle(originPoint, Size);
        }

        public void ChangeHealth(int damage)
        {
            Health -= damage;
        }

        public void ChangeDirection(Direction direction)
        {
            CurrentDirection = direction;
        }

        public void ChangeCoordinate()
        {
            var newPoint = Field.GetNewCoordinate(CurrentСoordinate, CurrentDirection, Speed);
            if (!CheckСollision(newPoint))
            {
                CurrentСoordinate = newPoint;
                HitBox = new Rectangle(CurrentСoordinate, Size);
            }       
        }

        private bool CheckСollision(Point newPoint)
        {
            var newHitBox = new Rectangle(newPoint, Size);
            foreach (var obstacle in Field.ListObstacles)
                if (Field.IsCrossingHitBoxes(newHitBox, obstacle.HitBox))
                    return true;
            if (!Field.IsInHitBoxField(newHitBox))
                return true;
            return false;
        }

        
    }
}

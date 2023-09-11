using System;
using System.Drawing;
using System.Linq;

namespace Tank.Domains
{
    class Shot
    {
        public int Speed { get; }
        public Direction CurrentDirection { get; }
        public int Damage { get; }
        public Point Coordinate { get; private set; }
        public Size Size { get; }
        public Rectangle HitBox { get; private set; }
        
        public Shot(int speed, Direction direction, int damage, Point coordinate)
        {
            Speed = speed;
            CurrentDirection = direction;
            Damage = damage;
            Coordinate = coordinate;
            if (direction == Direction.Left || direction == Direction.Right)
                Size = new Size(23, 13);
            else
                Size = new Size(13, 23);
            HitBox = new Rectangle(Coordinate, Size);
        }

        public void ChangeCoordinate()
        {
            if (!IsHit())
            {
                Coordinate = Field.GetNewCoordinate(Coordinate, CurrentDirection, Speed);
                HitBox = new Rectangle(Coordinate, Size);
            } 
        }

        private bool IsHit()
        {
            if (!Field.IsInHitBoxField(HitBox) || IsHitObstacles())
            {
                DeleteShot();
                return true;
            }
            return false;
        }

        private bool IsHitObstacles()
        {
            var tempListObstacles = Field.ListObstacles.ToList();
            foreach (var obstacle in tempListObstacles)
                if (Field.IsCrossingHitBoxes(obstacle.HitBox, HitBox))
                {
                    obstacle.ChangeHealth(Damage);
                    Field.ListShots.Remove(this);
                    return true;
                }
            var tempListTanks = Field.ListTanks.ToList();
            foreach (var enemy in tempListTanks)
                if (Field.IsCrossingHitBoxes(enemy.HitBox, HitBox))
                {
                    enemy.ChangeHealth(Damage);
                    Field.ListShots.Remove(this);
                    return true;
                }
            return false;
        }

        private void DeleteShot()
        {
            Field.ListShots.Remove(this);
        }
    }
}

using System.Drawing;

namespace Tank.Domains
{
    class Obstacle
    {
        //тип препятствия
        //здоровье
        //восприимчивость к выстрелам
        public Point Сoordinate { get; }
        public Rectangle HitBox { get; private set; }
        public int Health { get; private set; }

        public Obstacle(Point point, int health)
        {
            Сoordinate = point;
            HitBox = new Rectangle(Сoordinate, new Size(105, 105));
            Health = health;
        }
        public void ChangeHealth(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Field.ListObstacles.Remove(this);
        }
    }
}

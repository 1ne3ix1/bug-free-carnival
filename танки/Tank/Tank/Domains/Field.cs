using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Tank.Domains
{
    static class Field
    {
        static public Rectangle Hitbox { get; private set; }
        static public List<Shot> ListShots { get; private set; }
        static public List<Obstacle> ListObstacles { get; private set; }
        static public List<ITank> ListTanks { get; private set; }
        static public Direction[] ArrayAllDirections { get; private set; }

        static public void Set(int size)
        {
            ListShots = new List<Shot>();
            ListObstacles = new List<Obstacle>();
            ListTanks = new List<ITank>();
            Hitbox = new Rectangle(new Point(0, 0), new Size(size, size));
            ArrayAllDirections = new Direction[4] { Direction.Down, Direction.Left,
                                                    Direction.Up, Direction.Right };
        }

        static public void ChangeCoordinateShots()
        {
            var tempList = ListShots.ToList();
            foreach (var shot in tempList)
                shot.ChangeCoordinate();
        }

        static public void AddShot(Shot shot)
        {
            ListShots.Add(shot);
        }

        static public bool IsCrossingHitBoxes(Rectangle r1, Rectangle r2)
        {
            var horizontalCrossing = Math.Min(r1.Right, r2.Right) >= Math.Max(r1.Left, r2.Left);
            var verticalCrossing = Math.Max(r1.Top, r2.Top) <= Math.Min(r1.Bottom, r2.Bottom);
            return horizontalCrossing && verticalCrossing;
        }

        static public bool IsInHitBoxField(Rectangle r1)
        {
            return r1.Left >= Hitbox.Left && r1.Right <= Hitbox.Right && 
                   r1.Top >= Hitbox.Top && r1.Bottom <= Hitbox.Bottom;
        }

        static public Point GetNewCoordinate(Point point, Direction direction, int speed)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Point(point.X, point.Y + speed);
                case Direction.Up:
                    return new Point(point.X, point.Y - speed);
                case Direction.Left:
                    return new Point(point.X - speed, point.Y);
                case Direction.Right:
                    return new Point(point.X + speed, point.Y);
                default:
                    return new Point();
            }
        }

        static public Direction GetOppositeDirection(Direction direction)
        {
            if (direction == Direction.Down)
                return Direction.Up;
            if (direction == Direction.Up)
                return Direction.Down;
            if (direction == Direction.Right)
                return Direction.Left;
            return Direction.Right;
        }
    }
}

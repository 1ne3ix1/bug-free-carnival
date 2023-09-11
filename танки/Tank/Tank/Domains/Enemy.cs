using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tank.Domains
{
    class Enemy : ITank
    {
        public Point CurrentСoordinate { get; private set; }
        public int Speed { get; }
        public int Health { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public Size Size { get; }
        public Rectangle HitBox { get; private set; }
        public Dictionary<Direction, bool> AvailableDirections { get; }

        public Enemy(Point originPoint, int health, Direction direction, int speed)
        {
            Health = health;
            Speed = speed;
            Size = new Size(105, 105);
            AvailableDirections = new Dictionary<Direction, bool>();
            Update(originPoint, direction);
        }

        public void ChangeCoordinate()
        {
            foreach (var direction in Field.ArrayAllDirections) // пробегаемся по всем четырем направлениям
            {
                var newPoint = Field.GetNewCoordinate(CurrentСoordinate, direction, Speed);
                if (CurrentDirection != Field.GetOppositeDirection(direction) ||
                    CurrentDirection != direction)
                    if (!AvailableDirections[direction] && !CheckСollision(newPoint))
                    {
                        var oppositeDirection = Field.GetOppositeDirection(direction);
                        var oppositeNewPoint = Field.GetNewCoordinate(CurrentСoordinate, oppositeDirection, Speed);
                        if (!AvailableDirections[oppositeDirection] && !CheckСollision(oppositeNewPoint))
                        {
                            if (new Random().Next(0, 1) == 0)
                            {
                                Update(newPoint, direction);
                                return;
                            }                                           //ИИ писать через кучу if, а как иначе то?
                            else
                            {
                                Update(oppositeNewPoint, oppositeDirection);
                                return;
                            }
                        }
                        Update(newPoint, direction);
                        return;
                    }
            }
            var forwardPoint = Field.GetNewCoordinate(CurrentСoordinate, CurrentDirection, Speed);
            var backPoint = Field.GetNewCoordinate(CurrentСoordinate, 
                            Field.GetOppositeDirection(CurrentDirection), Speed);
            if (!CheckСollision(forwardPoint))
                Update(forwardPoint, CurrentDirection);
            else if (!CheckСollision(backPoint))
                Update(backPoint, Field.GetOppositeDirection(CurrentDirection)); 
        }

        private void Update(Point newPoint, Direction newDirection)
        {
            CurrentСoordinate = newPoint;
            CurrentDirection = newDirection;
            HitBox = new Rectangle(CurrentСoordinate, Size);
            foreach (var direction in Field.ArrayAllDirections)
                AvailableDirections[direction] = !CheckСollision(
                    Field.GetNewCoordinate(CurrentСoordinate, CurrentDirection, Speed));
        }

        public void ChangeHealth(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Field.ListTanks.Remove(this);
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

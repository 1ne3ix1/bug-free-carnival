using System.Drawing;

namespace Tank.Domains
{
    interface ITank
    {
        Point CurrentСoordinate { get; }
        int Speed { get; }
        int Health { get; }
        Direction CurrentDirection { get; }
        Size Size { get; }
        Rectangle HitBox { get; }
        void ChangeHealth(int damage);
        void ChangeCoordinate();
    }
}

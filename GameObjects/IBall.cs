using System.Numerics;

namespace MauiBrickBreak.GameObjects
{
    public interface IBall
    {
        bool BallAttached { get; }
        Color Color { get; set; }
        Vector3 Location { get; set; }
        SizeF Size { get; }
        Vector3 Start { get; set; }
        Vector3 Velocity { get; set; }
    }
}
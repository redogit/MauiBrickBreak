
using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameObjects;
/// <summary>
/// Ball renders its own movement rules.
/// </summary>
public class Ball : GameObject
{
    public Vector3 Start = Vector3.Zero;
    public Vector3 Location = Vector3.Zero;
    public SizeF Size = new(10, 10);
    public Vector3 Velocity = Vector3.Zero;
    public bool BallAttached = true;
    public Color Color = Colors.Black;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        canvas.FillColor = Color;
        canvas.FillCircle(Location.X, Location.Y, Size.Width);
        Bounds = new(Location.X, Location.Y, Size.Width, Size.Height);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }

    public void UpdateBall(Vector3 Offset)
    {
        Location += Offset;
    }
}

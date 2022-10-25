using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameObjects;

public class Paddle : GameObject
{
    internal Vector3 Start;
    public Vector3 Location = Vector3.Zero;
    public SizeF Size = new(150, 1);
    public float LeftPitch = 0;
    public float RightPitch = 0;
    public Color Color { get; set; }
    
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        canvas.StrokeSize = 10;
        canvas.StrokeColor = Color;
        canvas.DrawLine(Location.X, Location.Y + LeftPitch, Location.X + Size.Width, Location.Y + RightPitch);
        Bounds = new(new(Location.X, Location.Y), Size);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
    public void UpdatePaddle(Vector3 Offset)
    {
        Location += Offset;
    }
}

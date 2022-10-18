using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameObjects;

public class Paddle : GameObject
{
    internal Vector3 PaddleStart;
    public Vector3 Left { get; set; }

    internal readonly float Width = 150;
    public Color Color { get; set; }
    
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        canvas.StrokeSize = 10;
        canvas.StrokeColor = Color;
        canvas.DrawLine(Left.X, Left.Y, Left.X + Width, Left.Y);
        Bounds = new(new PointF(Left.X, Left.Y), new(Width, 10));
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
    public void UpdatePaddle(Vector3 Right)
    {
        Left = Vector3.Add(Left, Right);
    }
}

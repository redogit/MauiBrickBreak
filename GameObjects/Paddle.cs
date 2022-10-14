using Orbit.Engine;

namespace MauiBrickBreak.GameObjects;

public class Paddle : GameObject
{

    public PointF Left { get; set; }

    internal readonly float Width = 150;
    public Color Color { get; set; }
    
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        canvas.StrokeSize = 10;
        canvas.StrokeColor = Color;
        canvas.DrawLine(Left.X, Left.Y, Left.X + Width, Left.Y);
        Bounds = new(Left, new(Width, 10));
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
    public void UpdatePaddle(float x, float y)
    {
        Left = new(Left.X + x, Left.Y + y);
    }
}

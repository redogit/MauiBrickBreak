using Orbit.Engine;

namespace MauiBrickBreak.GameObjects;

public class Block : GameObject
{
    public PointF Left { get; set; }
    public SizeF Size = new(100, 50);
    public Color Color { get; set; }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        
        canvas.StrokeColor = Color;
        canvas.DrawRectangle(Left.X, Left.Y, 100, 50);
        Bounds = new(Left, Size);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
}

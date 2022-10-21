using Orbit.Engine;

namespace MauiBrickBreak.GameObjects;

public class Block : GameObject
{
    private PointF leftTop;
    public PointF LeftTop { get => leftTop; set => leftTop = BuildLines(value); }
    public SizeF Size = new(100, 50);
    public Color Color { get; set; }
    public RectF[] HitLines { get; set; }
    public int RequiredHits = 3;
    public bool HasBonus = false;
    private PointF BuildLines(PointF point)
    {
        HitLines = new[]
        {
            // top
            new RectF(point.X, point.Y, Size.Width, 1),
            // left
            new RectF(point, new(1, Size.Height)),
            // right
            new RectF(point.X + Size.Width, point.Y, 1, Size.Height),
            // bottom
            new RectF(new PointF(point.X, point.Y + Size.Height), new(Size.Width, 1))
        };
        return point;
    }

    public RectF GetIntersectingHitLine(RectF area)
    {
        if (area.IntersectsWith(HitLines[0]))
        {
            return HitLines[0];
        }
        else if (area.IntersectsWith(HitLines[1]))
        {
            return HitLines[1];
        }
        else if (area.IntersectsWith(HitLines[2]))
        {
            return HitLines[2];
        }
        else if (area.IntersectsWith(HitLines[3]))
        {
            return HitLines[3];
        }
        return default!;
    }
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        
        canvas.StrokeColor = Color;

        if (RequiredHits > 0)
        {
            canvas.DrawRectangle(LeftTop.X, LeftTop.Y, 100, 50);
        }
        if (RequiredHits > 1)
        {
            canvas.DrawRectangle(LeftTop.X + 10, LeftTop.Y + 5, 80, 40);
        }
        if (RequiredHits > 2)
        {
            canvas.DrawRectangle(LeftTop.X + 20, LeftTop.Y + 10, 60, 30);
        }
        Bounds = new(LeftTop, Size);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
}

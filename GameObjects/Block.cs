using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameObjects;

public class Block : GameObject
{
    private Vector3 leftTop;
    public Vector3 LeftTop { get => leftTop; set => leftTop = BuildLines(value); } 
    public RectF[] HitLines { get; set; }
    public int RequiredHits = 3;
    public bool HasBonus = false;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        LoadedImage ??= LoadImage("MauiBrickBreak.Resources.Raw." + ImageName);
        base.Render(canvas, dimensions);

        if (RequiredHits > 0)
        {
            canvas.DrawImage(LoadedImage, LeftTop.X, LeftTop.Y, 100, 50);
        }
        if (RequiredHits > 1)
        {
            canvas.DrawRectangle(LeftTop.X + 10, LeftTop.Y + 5, 80, 40);
        }
        if (RequiredHits > 2)
        {
            canvas.DrawRectangle(LeftTop.X + 20, LeftTop.Y + 10, 60, 30);
        }
        Bounds = new(LeftTop.X, LeftTop.Y, LoadedImage.Width, LoadedImage.Height);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
    private Vector3 BuildLines(Vector3 point)
    {

            HitLines = new[]
            {
                // top
                new RectF(point.X, point.Y,100, 1),
                // left
                new RectF(point.X, point.Y, 1,50),
                // right
                new RectF(point.X + 100, point.Y, 1, 50),
                // bottom
                new RectF(new PointF(point.X, point.Y + 50), new(100, 1))
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
}

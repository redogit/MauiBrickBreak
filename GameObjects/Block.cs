﻿using Microsoft.Maui.Controls.Shapes;
using Orbit.Engine;

namespace MauiBrickBreak.GameObjects;

public class Block : GameObject
{
    private PointF leftTop;
    public PointF LeftTop { get => leftTop; set => leftTop = BuildLines(value); }
    public SizeF Size = new(100, 50);
    public Color Color { get; set; }
    public RectF[] HitLines { get; set; }
    public int RequiredHits = 2;
    private PointF BuildLines(PointF point)
    {
        HitLines = new[]
        {
            // top
            new RectF(point,new(Size.Width, 1)),
            // left
            new RectF(point, new(1, Size.Height)),
            // right
            new RectF(point.X + Size.Width, point.Y, 1, Size.Height),
            // bottom
            new RectF(new PointF(point.X, point.Y + Size.Height), new(Size.Width,1))
        };
        return point;
    }

    public RectF GetIntersectingHitLine(Ball ball)
    {
        RectF ballLine = new(ball.CenterPoint.X + ball.BallVelocityX + ball.BallVelocityX, ball.CenterPoint.Y + ball.BallVelocityY + ball.BallVelocityY, ball.RadiusX, ball.RadiusY); ;
        if(ballLine.IntersectsWith(HitLines[0]))
        {
            return HitLines[0];
        }
        else if(ballLine.IntersectsWith(HitLines[1]))
        {
            return HitLines[1];
        }
        else if (ballLine.IntersectsWith(HitLines[2]))
        {
            return HitLines[2];
        }
        else if (ballLine.IntersectsWith(HitLines[3]))
        {
            return HitLines[3];
        }
        return default!;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        
        canvas.StrokeColor = Color;
        canvas.DrawRectangle(LeftTop.X, LeftTop.Y, 100, 50);
        Bounds = new(LeftTop, Size);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
}

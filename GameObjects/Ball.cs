using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameObjects;

public class Ball : GameObject
{
    public Vector3 CenterPoint = Vector3.Zero;
    internal readonly float RadiusX = 10;
    internal readonly float RadiusY = 10;
    public Vector3 Velocity = Vector3.Zero;
    public bool BallAttached { get; internal set; }
    internal Vector3 BallStart;
    public Color Color { get; set; }
    
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.FillColor = Color;
        canvas.FillCircle(CenterPoint.X, CenterPoint.Y, RadiusX);
        Bounds = new(CenterPoint.X, CenterPoint.Y, RadiusX, RadiusY);
        var points = MapBallLine(dimensions);
        for (int i = 0; i < 200; i += 2)
        {
            canvas.DrawLine(points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y);
        }
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }

    public void UpdateBall(Vector3 Offset)
    {
        CenterPoint = Vector3.Add(CenterPoint, Offset);
    }
    public void LaunchBall(double width)
    {
        BallAttached = !BallAttached;
        Velocity = new(-5, -5, 0);
    }
    public List<Vector3> MapBallLine(RectF dimensions)
    {
        Vector3 velocity = new(-5, -5, 0);
        List<Vector3> Points = new()
        {
            CenterPoint
        };
        if (Velocity != Vector3.Zero)
        {
            velocity = Velocity;
        }
        for (int i = 0; i < 199; i++)
        {
            if ((Points[^1] + velocity).X < 215 || (Points[^1] + velocity).X > dimensions.Width - 215)
            {
                velocity.X *= -1;
            }
            else if ((Points[^1] + Velocity).Y < 15)
            {
                velocity.Y *= -1;
            }
            else if ((Points[^1] + Velocity).X > 415 && (Points[^1] + Velocity).X < 1315 && (Points[^1] + Velocity).Y < 170)
            {
                velocity.X *= -1;
            }
            else if((Points[^1] + Velocity).X > 415 && (Points[^1] + Velocity).X < 1315 && (Points[^1] + Velocity).Y < 175)
            {
                velocity.Y *= -1;
            }
            
            Points.Add(Points[^1] + velocity);
        }
        return Points;
    }
}

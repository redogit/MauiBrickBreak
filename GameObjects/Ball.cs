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
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }

    public void UpdateBall(Vector3 Offset)
    {
        CenterPoint = Vector3.Add(CenterPoint, Offset);
    }
    public void LaunchBall()
    {
        BallAttached = !BallAttached;
        Velocity = new(-5, -5, 0);
    }
}

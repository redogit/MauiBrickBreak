using Orbit.Engine;

namespace MauiBrickBreak.GameObjects;

public class Ball : GameObject
{
    public PointF CenterPoint { get; set; }
    internal readonly float RadiusX = 10;
    internal readonly float RadiusY = 10;
    public float BallVelocityX { get; internal set; }
    public float BallVelocityY { get; internal set; }
    public bool BallAttached { get; set; }
    internal PointF BallStart;
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

    public void UpdateBall(float x, float y)
    {
        CenterPoint = new(CenterPoint.X + x, CenterPoint.Y + y);
    }
    public void LaunchBall()
    {
        BallAttached = !BallAttached;
        BallVelocityX = -5;
        BallVelocityY = -5;
    }
}

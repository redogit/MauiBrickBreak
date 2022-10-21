using MauiBrickBreak.GameScenes;
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
        if (!BallAttached)
        {
            // var query = (from x in CurrentScene.GameObjectsSnapshot
            //             where x.GetType() == typeof(Block) || x.GetType() == typeof(Paddle)
            //             select x).ToList();

            var paddle = ((MainScene)CurrentScene).Paddle;
            var blocks = ((MainScene)CurrentScene).Blocks;
            // Check paddle collision.
            if (paddle.Bounds.IntersectsWith(Bounds))
            {
                Velocity.Y *= -1f;
                ((MainScene)CurrentScene).Score++;
            }
            // Check ceiling collision.
            else if (CenterPoint.Y < 15)
            {
                Velocity.Y *= -1;
            }
            // Check for wall collision.
            else if (CenterPoint.X < 215 || CenterPoint.X > dimensions.Right - 215)
            {
                Velocity.X *= -1;
            }
            // Check if ball is below paddle.
            else if (CenterPoint.Y >= paddle.Left.Y)
            {
                paddle.Left = paddle.PaddleStart;
                CenterPoint = BallStart;
                BallAttached = true;
                Velocity.X = 0;
                Velocity.Y = 0;
            }
            else if (CenterPoint.Y < 190)
            {
                int i = ((MainScene)CurrentScene).FindCollision(new(CenterPoint.X + Velocity.X + Velocity.X, CenterPoint.Y + Velocity.Y + Velocity.Y, RadiusX, RadiusY), ref Velocity);
                if (i != -1)
                {
                    if (--blocks[i].RequiredHits == 0)
                    {
                        Remove(blocks[i]);
                        blocks.RemoveAt(i);
                    }
                }
            }
            UpdateBall(Velocity);
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
}

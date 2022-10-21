using MauiBrickBreak.GameObjects;
using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameScenes;

public class MainScene : GameScene
{
    internal readonly Paddle Paddle;

    internal readonly Ball Ball;

    internal int Score = 0;
    internal List<Block> Blocks = new();
    public MainScene(Paddle Paddle, Ball Ball)
    {
        Paddle.Color = Colors.Red;
        this.Paddle = Paddle;
        Ball.Color = Colors.Black;
        this.Ball = Ball;
        Add(Paddle);
        Add(Ball);

        Blocks.Add(new Block() { LeftTop = new(415, 15), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = new(415, 70), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = new(415, 125), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        foreach(Block block in Blocks)
        {
            Add(block);
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.StrokeSize = 15;
        canvas.StrokeColor = Colors.Black;

        canvas.DrawRectangle(dimensions);

        canvas.StrokeSize = 1;
        canvas.StrokeColor = Colors.Black;
        Vector3 velo = new(-5, -5, 0);
        Vector3 velocity = Ball.Velocity == Vector3.Zero ? velo : Ball.Velocity;
        Vector3 Location = Ball.CenterPoint;
        for (int i = 0; i < 199; i++)
        {
            if ((Location + velocity).X < 215 || (Location + velocity).X > dimensions.Width - 215)
            {
                velocity.X *= -1;
            }
            else if ((Location + velocity).Y < 15)
            {
                velocity.Y *= -1;
            }
            if (Location.Y < 190)
            {
                for (int x = 0; x < Blocks.Count; x++)
                {
                    FindCollision(new(Location.X + velocity.X + velocity.X, Location.Y + velocity.Y + velocity.Y, Ball.RadiusX, Ball.RadiusY), ref velocity);
                }
            }
            canvas.DrawLine(new(Location.X, Location.Y), new(Location.X + velocity.X, Location.Y + velocity.Y));
            Location += velocity;
        }

        #region Score
        canvas.DrawString($"Score:{Score}", 15, 10, 100, 50, HorizontalAlignment.Left, VerticalAlignment.Top);
        #endregion Score

    }
    
    public void UpdatePaddle(Vector3 Update)
    {
       Paddle.UpdatePaddle(Update);
    }

    public void UpdateBall(Vector3 Update)
    {
        Ball.UpdateBall(Update);
    }

    public int FindCollision(RectF target, ref Vector3 velocity)
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            var LineHit = Blocks[i].GetIntersectingHitLine(target);
            if (!LineHit.IsEmpty)
            {
                if (LineHit.Width == 1)
                {
                    velocity.X *= -1;
                }
                else
                {
                    velocity.Y *= -1;
                }
                return i;
            }
        }
        return -1;
    }
}
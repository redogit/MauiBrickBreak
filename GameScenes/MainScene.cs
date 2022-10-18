using MauiBrickBreak.GameObjects;
using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameScenes;

public class MainScene : GameScene
{
    internal readonly Paddle Paddle;

    internal readonly Ball Ball;

    private int Score = 0;

    internal List<Block> Blocks = new();
    public MainScene(Paddle Paddle, Ball Ball)
    {
        Paddle.Color = Colors.Red;
        this.Paddle = Paddle;
        Ball.Color = Colors.Black;
        this.Ball = Ball;
        Add(Paddle);
        Add(Ball);
        Blocks.Add(new() { LeftTop = new(415, 15), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = new(415, 70), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = new(415, 125), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { LeftTop = Blocks[^1].LeftTop.Offset(105, 0), Color = Colors.Grey });

        foreach (var block in Blocks)
        {
            Add(block);
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.StrokeSize = 15;
        canvas.StrokeColor = Colors.Black;
        dimensions.Left += 200;
        dimensions.Size = new Size(dimensions.Size.Width - 400, dimensions.Size.Height);
        canvas.DrawRectangle(dimensions);
        if (!Ball.BallAttached)
        {
            #region ball
            // Check paddle collision.
            if (Paddle.Bounds.IntersectsWith(Ball.Bounds))
            {
                Ball.Velocity.Y *= -1f;
                Score++;
            }
            // Check ceiling collision.
            else if (Ball.CenterPoint.Y < 15)
            {
                Ball.Velocity.Y *= -1;
            }
            // Check for wall collision.
            else if (Ball.CenterPoint.X < 215 || Ball.CenterPoint.X > dimensions.Right - 15)
            {
                Ball.Velocity.X *= -1;
            }
            // Check if ball is below paddle.
            else if (Ball.CenterPoint.Y >= Paddle.Left.Y)
            {
                Paddle.Left = Paddle.PaddleStart;
                Ball.CenterPoint = Ball.BallStart;
                Ball.BallAttached = true;
                Ball.Velocity.X = 0;
                Ball.Velocity.Y = 0;
            }
            else if (Ball.CenterPoint.Y < 200 && Ball.CenterPoint.X > 415 && Ball.CenterPoint.X < 1450)
            {
                int i = FindBallCollision();
                if (i != -1)
                {
                    if (--Blocks[i].RequiredHits == 0)
                    {
                        Remove(Blocks[i]);
                        Blocks.RemoveAt(i);
                    }
                }
            }
            UpdateBall(Ball.Velocity);
            #endregion ball

            #region Score
            canvas.DrawString($"Score:{Score}", 15, 10, 100, 50, HorizontalAlignment.Left, VerticalAlignment.Top);
            #endregion Score
            
        }
    }
    public void UpdatePaddle(Vector3 Update)
    {
       Paddle.UpdatePaddle(Update);
    }

    public void UpdateBall(Vector3 Update)
    {
        Ball.UpdateBall(Update);
    }

    public int FindBallCollision()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            if (Blocks[i].Bounds.IntersectsWith(Ball.Bounds.Offset(new PointF(Ball.Velocity.X * 2, Ball.Velocity.Y * 2))))
            {
                var LineHit = Blocks[i].GetIntersectingHitLine(Ball);
                if (!LineHit.IsEmpty)
                {
                    if (LineHit.Width == 1)
                    {
                        Ball.Velocity.X *= -1;
                    }
                    else
                    {
                        Ball.Velocity.Y *= -1;
                    }
                    return i;
                }
            }
        }
        return -1;
    }
}
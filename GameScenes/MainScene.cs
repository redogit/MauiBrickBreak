using MauiBrickBreak.GameObjects;
using Orbit.Engine;

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
        Blocks.Add(new() { Left = new(415, 15), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = new(415, 70), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = new(415, 125), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });
        Blocks.Add(new() { Left = Blocks[^1].Left.Offset(105, 0), Color = Colors.Grey });

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
        canvas.DrawRectangle(dimensions);
        if (!Ball.BallAttached)
        {
            #region ball
            // Check paddle collision.
            if (Paddle.Bounds.IntersectsWith(Ball.Bounds))
            {
                Ball.BallVelocityY *= -1;
                Score++;
            }
            // Check ceiling collision.
            else if (Ball.CenterPoint.Y < 15)
            {
                Ball.BallVelocityY *= -1;
            }
            // Check for wall collision.
            else if (Ball.CenterPoint.X < 15 || Ball.CenterPoint.X > dimensions.Right - 15)
            {
                Ball.BallVelocityX *= -1;
            }
            // Check if ball is below paddle.
            else if (Ball.CenterPoint.Y >= Paddle.Left.Y)
            {
                Paddle.Left = Paddle.PaddleStart;
                Ball.CenterPoint = Ball.BallStart;
                Ball.BallAttached = true;
                Ball.BallVelocityX = 0;
                Ball.BallVelocityY = 0;
            }
            if (Ball.CenterPoint.Y < 200)
            {
                FindBlockBallCollision();
            }
            UpdateBall(Ball.BallVelocityX, Ball.BallVelocityY);
            #endregion ball

            #region Score
            canvas.DrawString($"Score:{Score}", 15, 10, 100, 50, HorizontalAlignment.Left, VerticalAlignment.Top);
            #endregion Score
            
        }
    }
    public void UpdatePaddle(float x, float y)
    {
        Paddle.UpdatePaddle(x, y);
    }

    public void UpdateBall(float x, float y)
    {
        Ball.UpdateBall(x, y);
    }
    public bool FindBlockBallCollision()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            if (Blocks[i].Bounds.IntersectsWith(Ball.Bounds))
            {
                if (Blocks[i].RequiredHits-- == 0)
                {
                    Remove(Blocks[i]);
                    Blocks.RemoveAt(i);
                    
                }
                var bi = Blocks[i].Bounds.Intersect(Ball.Bounds);
                Ball.BallVelocityY *= -1;
                return true;
            }
        }
        return false;
    }
}
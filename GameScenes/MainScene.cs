using MauiBrickBreak.GameObjects;
using Orbit.Engine;

namespace MauiBrickBreak.GameScenes;

public class MainScene : GameScene
{
    internal GameManager GameManager { get; }
    internal List<Block> Blocks = new();
    public MainScene(GameManager gameManager)
    {

        Add(gameManager.Paddle);
        Add(gameManager.Ball);
        GameManager = gameManager;
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
        if (!GameManager.BallAttached)
        {
            #region ball
            // Check paddle collision.
            if (GameManager.Paddle.Bounds.IntersectsWith(GameManager.Ball.Bounds))
            {
                GameManager.BallVelocityY *= -1;
                GameManager.Score++;
            }
            // Check ceiling collision.
            else if (GameManager.Ball.CenterPoint.Y < 15)
            {
                GameManager.BallVelocityY *= -1;
            }
            // Check for wall collision.
            else if (GameManager.Ball.CenterPoint.X < 15 || GameManager.Ball.CenterPoint.X > dimensions.Right - 15)
            {
                GameManager.BallVelocityX *= -1;
            }
            // Check if ball is below paddle.
            else if (GameManager.Ball.CenterPoint.Y >= GameManager.Paddle.Left.Y)
            {
                GameManager.Paddle.Left = GameManager.PaddleStart;
                GameManager.Ball.CenterPoint = GameManager.BallStart;
                GameManager.BallAttached = true;
                GameManager.BallVelocityX = 0;
                GameManager.BallVelocityY = 0;
            }
            if (GameManager.Ball.CenterPoint.Y < 200)
            {
                FindBlockBallCollision();
            }
            UpdateBall(GameManager.BallVelocityX, GameManager.BallVelocityY);
            #endregion ball

            #region Score
            canvas.DrawString($"Score:{GameManager.Score}", 15, 10, 100, 50, Microsoft.Maui.Graphics.HorizontalAlignment.Left, Microsoft.Maui.Graphics.VerticalAlignment.Top);
            #endregion Score
            
        }
    }
    public void UpdatePaddle(float x, float y)
    {
        GameManager.Paddle.UpdatePaddle(x, y);
    }

    public void UpdateBall(float x, float y)
    {
        GameManager.Ball.UpdateBall(x, y);
    }
    public bool FindBlockBallCollision()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            if (Blocks[i].Bounds.IntersectsWith(GameManager.Ball.Bounds))
            {
                Remove(Blocks[i]);
                Blocks.RemoveAt(i);
                GameManager.BallVelocityY *= -1;
                return true;
            }
        }
        return false;
    }
}
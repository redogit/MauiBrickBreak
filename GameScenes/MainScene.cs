using MauiBrickBreak.GameObjects;
using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameScenes;

public class MainScene : GameScene
{
    internal readonly Paddle Paddle;
    internal readonly Ball Ball;
    internal bool BallAttached;
    internal int Score = 0;
    internal List<Block> Blocks = new();
    public MainScene(Paddle Paddle, Ball Ball)
    {
        this.Paddle = Paddle;
        this.Ball = Ball;
        Add(Paddle);
        Add(Ball);
        Blocks.Add(new Block() { LeftTop = new(415, 15, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = new(415, 70, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = new(415, 125,0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
        Blocks.Add(new Block() { LeftTop = Blocks[^1].LeftTop + new Vector3(105, 0, 0) });
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
        canvas.DrawRectangle(dimensions.Left + 200, dimensions.Top, dimensions.Size.Width - 400, dimensions.Size.Height);
        
        if (!BallAttached)
        {
            if (Paddle.Bounds.IntersectsWith(Ball.Bounds))
            {
                Ball.Velocity.Y *= -1;
                Score++;
            }
            // Check ceiling collision.
            else if (Ball.Location.Y < 15)
            {
                Ball.Velocity.Y *= -1;
            }
            // Check for wall collision.
            else if (Ball.Location.X < 215 || Ball.Location.X > dimensions.Right - 215)
            {
                Ball.Velocity.X *= -1;
            }
            // Check if ball is below paddle.
            else if (Ball.Location.Y >= Paddle.Location.Y)
            {
                Paddle.Location = Paddle.Start;
                Ball.Location = Ball.Start;
                BallAttached = true;
                Ball.Velocity = new(0, 0, 0);
            }
            // check if ball will collide with block and which side.
            else if (Ball.Location.Y < 190)
            {
                int i = FindCollision(new(Ball.Location.X + Ball.Velocity.X + Ball.Velocity.X, Ball.Location.Y + Ball.Velocity.Y + Ball.Velocity.Y, 20, 20), ref Ball.Velocity);
                if (i != -1)
                {
                    // if Required hits equals 0 remove
                    if (--Blocks[i].RequiredHits == 0)
                    {
                        // the Block from MainScene and from Block list.
                        Remove(Blocks[i]);
                        Blocks.RemoveAt(i);
                    }
                }
            }
        }
        UpdateBall(Ball.Velocity);

        canvas.StrokeSize = 1;
        canvas.StrokeColor = Colors.Black;
        var velocity_adjust = (float)Math.Clamp(Paddle.LeftPitch > 0 ? Paddle.LeftPitch : Paddle.RightPitch * -1, -25, 25);
        Vector3 velo = new(velocity_adjust / 5, -5, 0);
        Vector3 velocity = Ball.Velocity == Vector3.Zero ? velo : Ball.Velocity;
        Vector3 Location = new(Ball.Location.X + (20 / 2), Ball.Location.Y - (20 / 2),0);

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
            else if (Location.Y < 190)
            {
                FindCollision(new(Location.X + velocity.X + velocity.X, Location.Y + velocity.Y + velocity.Y, 20, 20), ref velocity);
            }
            else if(Paddle.Bounds.IntersectsWith(new(Location.X + velocity.X + velocity.X, Location.Y + velocity.Y + velocity.Y, 20, 20)))
            {
                velocity.Y *= -1;
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
    public void LaunchBall()
    {
        BallAttached = !BallAttached;
        if (Paddle.LeftPitch != 0)
        {
            Ball.Velocity = new(Paddle.LeftPitch / 5, -5, 0);
        }
        else
        {
            Ball.Velocity = new(Paddle.RightPitch * -1 / 5, -5, 0);
        }
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
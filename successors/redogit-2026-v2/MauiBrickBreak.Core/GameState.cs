namespace MauiBrickBreak.Core;

public readonly record struct Vec2(double X, double Y)
{
    public static Vec2 operator +(Vec2 a, Vec2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vec2 operator *(Vec2 value, double scalar) => new(value.X * scalar, value.Y * scalar);
}

public readonly record struct Rect(double X, double Y, double Width, double Height)
{
    public double Right => X + Width;
    public double Bottom => Y + Height;

    public bool Intersects(Rect other) =>
        X < other.Right && Right > other.X && Y < other.Bottom && Bottom > other.Y;
}

public sealed class Ball
{
    public Vec2 Position { get; set; }
    public Vec2 Velocity { get; set; }
    public double Size { get; init; } = 10;
    public Rect Bounds => new(Position.X, Position.Y, Size, Size);
}

public sealed class Paddle
{
    public Vec2 Position { get; set; }
    public double Width { get; init; } = 150;
    public double Height { get; init; } = 10;
    public Rect Bounds => new(Position.X, Position.Y, Width, Height);
}

public sealed class Block
{
    public required Rect Bounds { get; init; }
    public int RequiredHits { get; private set; } = 3;
    public bool Destroyed => RequiredHits <= 0;

    internal void Hit()
    {
        if (RequiredHits > 0) RequiredHits--;
    }
}

public enum StepEvent
{
    WallBounce,
    PaddleBounce,
    BlockHit,
    BallLost
}

public sealed record StepResult(IReadOnlyList<StepEvent> Events)
{
    public bool Contains(StepEvent value) => Events.Contains(value);
}

public sealed class GameState
{
    public double Width { get; init; } = 800;
    public double Height { get; init; } = 600;
    public Ball Ball { get; } = new();
    public Paddle Paddle { get; } = new();
    public List<Block> Blocks { get; } = [];

    public StepResult Step(double seconds)
    {
        if (seconds < 0) throw new ArgumentOutOfRangeException(nameof(seconds));

        var events = new List<StepEvent>();
        Ball.Position += Ball.Velocity * seconds;

        if (Ball.Position.X < 0)
        {
            Ball.Position = new(0, Ball.Position.Y);
            Ball.Velocity = new(Math.Abs(Ball.Velocity.X), Ball.Velocity.Y);
            events.Add(StepEvent.WallBounce);
        }
        else if (Ball.Position.X + Ball.Size > Width)
        {
            Ball.Position = new(Width - Ball.Size, Ball.Position.Y);
            Ball.Velocity = new(-Math.Abs(Ball.Velocity.X), Ball.Velocity.Y);
            events.Add(StepEvent.WallBounce);
        }

        if (Ball.Position.Y < 0)
        {
            Ball.Position = new(Ball.Position.X, 0);
            Ball.Velocity = new(Ball.Velocity.X, Math.Abs(Ball.Velocity.Y));
            events.Add(StepEvent.WallBounce);
        }

        if (Ball.Position.Y >= Height)
        {
            events.Add(StepEvent.BallLost);
            return new(events);
        }

        if (Ball.Bounds.Intersects(Paddle.Bounds) && Ball.Velocity.Y > 0)
        {
            Ball.Position = new(Ball.Position.X, Paddle.Position.Y - Ball.Size);
            Ball.Velocity = new(Ball.Velocity.X, -Math.Abs(Ball.Velocity.Y));
            events.Add(StepEvent.PaddleBounce);
        }

        foreach (var block in Blocks.Where(block => !block.Destroyed))
        {
            if (!Ball.Bounds.Intersects(block.Bounds)) continue;

            block.Hit();
            Ball.Velocity = new(Ball.Velocity.X, -Ball.Velocity.Y);
            events.Add(StepEvent.BlockHit);
            break;
        }

        return new(events);
    }
}

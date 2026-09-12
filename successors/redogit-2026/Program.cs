namespace MauiBrickBreak.Redo;

public readonly record struct Vec2(double X, double Y)
{
    public static Vec2 operator +(Vec2 a, Vec2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vec2 operator *(Vec2 v, double scalar) => new(v.X * scalar, v.Y * scalar);
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

    public void Hit()
    {
        if (RequiredHits > 0) RequiredHits--;
    }
}

public sealed class GameState
{
    public double Width { get; init; } = 800;
    public double Height { get; init; } = 600;
    public Ball Ball { get; } = new();
    public Paddle Paddle { get; } = new();
    public List<Block> Blocks { get; } = [];

    public void Step(double seconds)
    {
        if (seconds < 0) throw new ArgumentOutOfRangeException(nameof(seconds));

        Ball.Position += Ball.Velocity * seconds;
        ReflectFromWalls();

        if (Ball.Bounds.Intersects(Paddle.Bounds) && Ball.Velocity.Y > 0)
            Ball.Velocity = new(Ball.Velocity.X, -Math.Abs(Ball.Velocity.Y));

        foreach (var block in Blocks.Where(block => !block.Destroyed))
        {
            if (!Ball.Bounds.Intersects(block.Bounds)) continue;
            block.Hit();
            Ball.Velocity = new(Ball.Velocity.X, -Ball.Velocity.Y);
            break;
        }
    }

    private void ReflectFromWalls()
    {
        if (Ball.Position.X < 0)
        {
            Ball.Position = new(0, Ball.Position.Y);
            Ball.Velocity = new(Math.Abs(Ball.Velocity.X), Ball.Velocity.Y);
        }
        else if (Ball.Position.X + Ball.Size > Width)
        {
            Ball.Position = new(Width - Ball.Size, Ball.Position.Y);
            Ball.Velocity = new(-Math.Abs(Ball.Velocity.X), Ball.Velocity.Y);
        }

        if (Ball.Position.Y < 0)
        {
            Ball.Position = new(Ball.Position.X, 0);
            Ball.Velocity = new(Ball.Velocity.X, Math.Abs(Ball.Velocity.Y));
        }
    }
}

public static class Program
{
    public static int Main()
    {
        var checks = new[]
        {
            CheckWallReflection(),
            CheckPaddleReflection(),
            CheckBlockHit()
        };

        foreach (var check in checks)
            Console.WriteLine($"{check.Name}: {(check.Passed ? "PASS" : "FAIL")}");

        return checks.All(c => c.Passed) ? 0 : 1;
    }

    private static Check CheckWallReflection()
    {
        var game = new GameState();
        game.Ball.Position = new(1, 100);
        game.Ball.Velocity = new(-100, 0);
        game.Step(0.1);
        return new("wall reflection", game.Ball.Velocity.X > 0 && game.Ball.Position.X >= 0);
    }

    private static Check CheckPaddleReflection()
    {
        var game = new GameState();
        game.Paddle.Position = new(300, 500);
        game.Ball.Position = new(350, 489);
        game.Ball.Velocity = new(0, 20);
        game.Step(0.1);
        return new("paddle reflection", game.Ball.Velocity.Y < 0);
    }

    private static Check CheckBlockHit()
    {
        var game = new GameState();
        var block = new Block { Bounds = new(100, 100, 100, 50) };
        game.Blocks.Add(block);
        game.Ball.Position = new(130, 90);
        game.Ball.Velocity = new(0, 20);
        game.Step(0.5);
        return new("block hit depletion", block.RequiredHits == 2 && game.Ball.Velocity.Y < 0);
    }

    private sealed record Check(string Name, bool Passed);
}

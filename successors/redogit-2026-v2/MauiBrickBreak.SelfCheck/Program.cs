using MauiBrickBreak.Core;

var checks = new[]
{
    CheckWallReflection(),
    CheckPaddleReflection(),
    CheckBlockHit(),
    CheckBallLost()
};

foreach (var check in checks)
    Console.WriteLine($"{check.Name}: {(check.Passed ? "PASS" : "FAIL")}");

return checks.All(check => check.Passed) ? 0 : 1;

static Check CheckWallReflection()
{
    var game = new GameState();
    game.Ball.Position = new(1, 100);
    game.Ball.Velocity = new(-100, 0);
    var result = game.Step(0.1);
    return new("wall reflection", result.Contains(StepEvent.WallBounce) && game.Ball.Velocity.X > 0 && game.Ball.Position.X >= 0);
}

static Check CheckPaddleReflection()
{
    var game = new GameState();
    game.Paddle.Position = new(300, 500);
    game.Ball.Position = new(350, 489);
    game.Ball.Velocity = new(0, 20);
    var result = game.Step(0.1);
    return new("paddle reflection", result.Contains(StepEvent.PaddleBounce) && game.Ball.Velocity.Y < 0);
}

static Check CheckBlockHit()
{
    var game = new GameState();
    var block = new Block { Bounds = new(100, 100, 100, 50) };
    game.Blocks.Add(block);
    game.Ball.Position = new(130, 90);
    game.Ball.Velocity = new(0, 20);
    var result = game.Step(0.5);
    return new("block hit depletion", result.Contains(StepEvent.BlockHit) && block.RequiredHits == 2 && game.Ball.Velocity.Y < 0);
}

static Check CheckBallLost()
{
    var game = new GameState();
    game.Ball.Position = new(100, 595);
    game.Ball.Velocity = new(0, 20);
    var result = game.Step(0.5);
    return new("ball lost consequence", result.Contains(StepEvent.BallLost));
}

sealed record Check(string Name, bool Passed);

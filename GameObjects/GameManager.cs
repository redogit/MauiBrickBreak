using MauiBrickBreak.GameObjects;
using Orbit.Engine;

namespace MauiBrickBreak;

public class GameManager : GameObject
{
    internal readonly Paddle Paddle;

    internal readonly Ball Ball;

    internal PointF PaddleStart;
    internal PointF BallStart;

    public GameManager(Paddle paddle, Ball ball)
    {

        paddle.Color = Colors.Red;
        Paddle = paddle;
        ball.Color = Colors.Black;
        Ball = ball;

       
    }
	public float BallVelocityX { get; internal set; }
	public float BallVelocityY { get; internal set; }
    public bool BallAttached { get; set; }
    public int Score { get; set; }
    public void LaunchBall()
    {
        BallAttached = !BallAttached;
        BallVelocityX = -5;
        BallVelocityY = -5;
    }

}

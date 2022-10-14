using MauiBrickBreak.GameScenes;
using Orbit.Engine;

namespace MauiBrickBreak;

public partial class MainPage : ContentPage
{
	private readonly IGameSceneManager gameSceneManager;
	private readonly MainScene mainScene;

    // Variables that hold direction
    private readonly PointF Left = new(-10, 0);
    private readonly PointF Right = new(10, 0);
    private readonly PointF Up = new(0, -10);
    private readonly PointF Down = new(0, 10);

    public MainPage(IGameSceneManager gameSceneManager, MainScene mainScene)
	{
		InitializeComponent();
        this.gameSceneManager = gameSceneManager;
       
		this.mainScene = mainScene;
		gameSceneManager.LoadScene(mainScene, GameView);
        gameSceneManager.Start();
	}

	/// <summary>
	/// When Entry is loaded, force focus..
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void CommandEntry_Loaded(object sender, EventArgs e)
	{
        CommandEntry.Focus();
	}
	/// <summary>
	/// If Entry is unfocused, force focus back.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void CommandEntry_Unfocused(object sender, FocusEventArgs e)
	{
		// if Entry loses focus force it back so no commands are lost.
		CommandEntry.Focus();
	}

	/// <summary>
	/// Setup game variables.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void GameCanvas_Loaded(object sender, EventArgs e)
	{
        #region Game Setup
        // round to 10's place

        GameView.WidthRequest = (float)((int)(contentPage.Width / 10) * 10) - 30;
        GameView.HeightRequest = (float)((int)(contentPage.Height / 10) * 10) - 50;
        mainScene.Paddle.Left = new(15, (float)GameView.HeightRequest - 30);
        mainScene.Ball.CenterPoint = new(85, (float)GameView.HeightRequest - 45);

        mainScene.Paddle.PaddleStart = mainScene.Paddle.Left;
        mainScene.Ball.BallStart = mainScene.Ball.CenterPoint;

        mainScene.Ball.BallAttached = true;
        #endregion Game Setup
    }
    /// <summary>
    /// handle keys as commands
    /// a = left
    /// s = down
    /// w = up
    /// d = right
    /// bounds movement to canvas rect (left, right and bottom), and up is bound to the top of the playarea(road).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CommandProcess(object sender, TextChangedEventArgs e)
    {

        switch (e.NewTextValue)
        {
            // Handle Left Command
            case "a":
                if (mainScene.Paddle.Left.X - 10 > 15)
                {
                    mainScene.UpdatePaddle(Left.X, Left.Y);
                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Left.X, Left.Y);
                    }
                }
                break;
            // Handle Down Command
            case "s":
                if (mainScene.Paddle.Left.Y + 10 < GameView.HeightRequest - 40)
                {
                    mainScene.UpdatePaddle(Down.X, Down.Y);
                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Down.X, Down.Y);
                    }
                }
                break;
            // Handle Up Command
            case "w":
                if (mainScene.Paddle.Left.Y - 10 > GameView.HeightRequest - 140)
                {
                    mainScene.UpdatePaddle(Up.X, Up.Y);

                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Up.X, Up.Y);
                    }
                }
                break;
            // Handle Right Command
            case "d":
                if (mainScene.Paddle.Left.X + mainScene.Paddle.Width + 10 < GameView.WidthRequest - 10)
                {
                    mainScene.UpdatePaddle(Right.X, Right.Y);
                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Right.X, Right.Y);
                    }
                }
                break;
            // Handle Launch Command
            case "l":
                if (mainScene.Ball.BallAttached)
                {
                    mainScene.Ball.LaunchBall();

                }
                break;
            default:
                break;
        }
        CommandEntry.Text = "";
    }
}

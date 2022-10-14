using MauiBrickBreak.GameScenes;
using Orbit.Engine;

namespace MauiBrickBreak;

public partial class MainPage : ContentPage
{
	private readonly IGameSceneManager gameSceneManager;
	private readonly MainScene mainScene;
    private readonly GameManager gameManager;

    // Variables that hold direction
    private readonly PointF Left = new(-10, 0);
    private readonly PointF Right = new(10, 0);
    private readonly PointF Up = new(0, -10);
    private readonly PointF Down = new(0, 10);

    public MainPage(IGameSceneManager gameSceneManager, MainScene mainScene, GameManager gameManager)
	{
		InitializeComponent();
        this.gameSceneManager = gameSceneManager;
        this.gameManager = gameManager;
       
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
        gameManager.Paddle.Left = new(15, (float)GameView.HeightRequest - 30);
        gameManager.Ball.CenterPoint = new(85, (float)GameView.HeightRequest - 45);

        gameManager.PaddleStart = gameManager.Paddle.Left;
        gameManager.BallStart = gameManager.Ball.CenterPoint;

        gameManager.BallAttached = true;
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
                if (gameManager.Paddle.Left.X - 10 > 15)
                {
                    mainScene.UpdatePaddle(Left.X, Left.Y);
                    if (gameManager.BallAttached)
                    {
                        mainScene.UpdateBall(Left.X, Left.Y);
                    }
                }
                break;
            // Handle Down Command
            case "s":
                if (gameManager.Paddle.Left.Y + 10 < GameView.HeightRequest - 40)
                {
                    mainScene.UpdatePaddle(Down.X, Down.Y);
                    if (gameManager.BallAttached)
                    {
                        mainScene.UpdateBall(Down.X, Down.Y);
                    }
                }
                break;
            // Handle Up Command
            case "w":
                if (gameManager.Paddle.Left.Y - 10 > GameView.HeightRequest - 140)
                {
                    mainScene.UpdatePaddle(Up.X, Up.Y);

                    if (gameManager.BallAttached)
                    {
                        mainScene.UpdateBall(Up.X, Up.Y);
                    }
                }
                break;
            // Handle Right Command
            case "d":
                if (gameManager.Paddle.Left.X + gameManager.Paddle.Width + 10 < GameView.WidthRequest - 10)
                {
                    mainScene.UpdatePaddle(Right.X, Right.Y);
                    if (gameManager.BallAttached)
                    {
                        mainScene.UpdateBall(Right.X, Right.Y);
                    }
                }
                break;
            // Handle Launch Command
            case "l":
                if (gameManager.BallAttached)
                {
                    gameManager.LaunchBall();

                }
                break;
            default:
                break;
        }
        CommandEntry.Text = "";
    }
}

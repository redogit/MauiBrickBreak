using MauiBrickBreak.GameObjects;
using MauiBrickBreak.GameScenes;
using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager GameSceneManager;
    private readonly MainScene mainScene;

    // Variables that hold direction
    private readonly Vector3 Left = new(-10, 0, 0);
    private readonly Vector3 Right = new(10, 0, 0);
    private readonly Vector3 Up = new(0, -10, 0);
    private readonly Vector3 Down = new(0, 10, 0);

    public MainPage(IGameSceneManager gameSceneManager, MainScene mainScene)
    {
        InitializeComponent();
        GameSceneManager = gameSceneManager;
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

        System.Reflection.Assembly assembly = typeof(Microsoft.Maui.Graphics.Win2D.W2DCanvas).Assembly;
        var type = assembly.GetType("Microsoft.Maui.Graphics.Win2D.W2DGraphicsService");
        var prop = type.GetProperty("GlobalCreator");

        var graphicsView = (GraphicsView)sender;
        var view = (Microsoft.Maui.Platform.PlatformTouchGraphicsView)graphicsView.Handler.PlatformView;
        var view2 = (Microsoft.Maui.Graphics.Win2D.W2DGraphicsView)view.Content;
        prop.SetValue(null, view2.Content);

    #region Game Setup
    // round to 10's place

    GameView.WidthRequest = ((int)(contentPage.Width / 10) * 10);
        GameView.HeightRequest = ((int)(contentPage.Height / 10) * 10);
        mainScene.Paddle.Location = new Vector3((float)(GameView.WidthRequest * 0.5), (float)GameView.HeightRequest - 50, 0);
        mainScene.Paddle.Start = mainScene.Paddle.Location;
        mainScene.Ball.Location = new(mainScene.Paddle.Location.X + 75, (float)GameView.HeightRequest - 70, 0);
        mainScene.Ball.Start = mainScene.Ball.Location;

        mainScene.Ball.BallAttached = true;
#endregion Game Setup
    }
    /// <summary>
    /// handle keys as commands
    /// a = left
    /// s = down
    /// w = up
    /// d = right
    /// l = launch
    /// bounds movement to canvas rect (left, right and bottom), and up is bound to the top of the playarea(road).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CommandProcess(object sender, TextChangedEventArgs e)
    {
        if(mainScene.Ball.BallAttached)
        {
            ProcessAngle(e);
        }
        switch (e.NewTextValue)
        {
            // Handle Left Command
            case "a":
                if (mainScene.Paddle.Location.X - 10 > 215)
                {
                    mainScene.UpdatePaddle(Left);
                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Left);
                    }
                }
                break;
            // Handle Down Command
            case "s":
                if (mainScene.Paddle.Location.Y + 10 < GameView.HeightRequest - 40)
                {
                    mainScene.UpdatePaddle(Down);
                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Down);
                    }
                }
                break;
            // Handle Up Command
            case "w":
                if (mainScene.Paddle.Location.Y - 10 > GameView.HeightRequest - 140)
                {
                    mainScene.UpdatePaddle(Up);

                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Up);
                    }
                }
                break;
            // Handle Right Command
            case "d":
                if (mainScene.Paddle.Location.X + mainScene.Paddle.Size.Width + 10 < GameView.WidthRequest - 215)
                {
                    mainScene.UpdatePaddle(Right);
                    if (mainScene.Ball.BallAttached)
                    {
                        mainScene.UpdateBall(Right);
                    }
                }
                break;

            // Handle Launch Command
            case "l":
                if (mainScene.Ball.BallAttached)
                {
                    mainScene.LaunchBall();
                    mainScene.Paddle.LeftPitch = 0;
                    mainScene.Paddle.RightPitch = 0;
                }
                break;
            default:
                break;
        }
        CommandEntry.Text = "";
    }
    public void ProcessAngle(TextChangedEventArgs e)
    {
        switch (e.NewTextValue)
        {
            // Handle Left pitch
            case "q":
                if (mainScene.Ball.BallAttached)
                {
                    mainScene.Paddle.RightPitch = 0;
                    mainScene.Paddle.LeftPitch += 1;
                }
                break;
            case "e":
                if (mainScene.Ball.BallAttached)
                {
                    mainScene.Paddle.LeftPitch = 0;
                    mainScene.Paddle.RightPitch += 1;
                }
                break;
            default:
                break;
        }
    } 
}

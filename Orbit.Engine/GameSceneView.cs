namespace Orbit.Engine;

public class GameSceneView : GraphicsView
{
    public GameSceneView()
    {
    }

    private IGameScene scene;

    public IGameScene Scene
    {
        get => scene;
        internal set
        {
            scene = value;
            Drawable = value;
        }
    }
}

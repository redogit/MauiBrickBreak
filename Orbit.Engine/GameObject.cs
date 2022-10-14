using System.Reflection;
using Microsoft.Maui.Graphics.Win2D;
namespace Orbit.Engine;

/// <summary>
/// Base class definition representing an object in a game.
/// </summary>
public abstract class GameObject : GameObjectContainer, IGameObject, IDrawable
{
    public GameScene CurrentScene { get; internal set; } // TODO: weak reference?

    public virtual bool IsCollisionDetectionEnabled { get; }

    public RectF Bounds { get; set; }

    protected Microsoft.Maui.Graphics.IImage LoadImage(string imageName)
    {
        var assembly = GetType().GetTypeInfo().Assembly;

        using var stream = assembly.GetManifestResourceStream("Orbit.Resources.EmbeddedResources." + imageName);
            return new W2DImageLoadingService().FromStream(stream);

    }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();
        canvas.ResetState();

        Render(canvas, dirtyRect);

        canvas.RestoreState();
    }
}

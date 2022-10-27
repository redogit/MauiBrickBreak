
using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameObjects;
/// <summary>
/// Ball renders its own movement rules.
/// </summary>
/// 
public class Ball : GameObject
{
    public Vector3 Start = Vector3.Zero;
    public Vector3 Location = Vector3.Zero;
    public Vector3 Velocity = Vector3.Zero;
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        LoadedImage ??= LoadImage("MauiBrickBreak.Resources.Raw." + ImageName);
        base.Render(canvas, dimensions);
        canvas.DrawImage(LoadedImage, Location.X, Location.Y, LoadedImage.Width, LoadedImage.Height);
        Bounds = new(Location.X, Location.Y, LoadedImage.Width, LoadedImage.Height);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }

    public void UpdateBall(Vector3 Offset)
    {
        Location += Offset;
    }
}

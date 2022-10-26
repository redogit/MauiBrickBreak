
using Orbit.Engine;
using System.Numerics;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace MauiBrickBreak.GameObjects;
/// <summary>
/// Ball renders its own movement rules.
/// </summary>
/// 
public class Ball : GameObject
{
    public Vector3 Start = Vector3.Zero;
    public Vector3 Location = Vector3.Zero;
    public SizeF Size = new(20, 20);
    public Vector3 Velocity = Vector3.Zero;
    public bool BallAttached = true;
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        LoadedImage ??= LoadImage("MauiBrickBreak.Resources.Raw." + ImageName);
        base.Render(canvas, dimensions);
        canvas.DrawImage(LoadedImage, Location.X, Location.Y, 20, 20);
        Bounds = new(Location.X, Location.Y, Size.Width, Size.Height);
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

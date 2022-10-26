using Orbit.Engine;
using System.Numerics;

namespace MauiBrickBreak.GameObjects;

public class Paddle : GameObject
{
    internal Vector3 Start;
    public Vector3 Location = Vector3.Zero;
    public SizeF Size = new(150, 10);
    public float LeftPitch = 0;
    public float RightPitch = 0;
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        LoadedImage ??= LoadImage("MauiBrickBreak.Resources.Raw." + ImageName);
        base.Render(canvas, dimensions);
        canvas.DrawImage(LoadedImage, Location.X, Location.Y, Size.Width, Size.Height);
        Bounds = new(new(Location.X, Location.Y), Size);
    }
    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }
    public void UpdatePaddle(Vector3 Offset)
    {
        Location += Offset;
    }
}

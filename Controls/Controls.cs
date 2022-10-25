using MauiBrickBreak.GameObjects;
using System.Numerics;

namespace MauiBrickBreak.Controls
{
    internal class BallControl : View, IBall
    {
        public BallControl()
        {
        }

        public static readonly BindableProperty BallAttachedProperty =
        BindableProperty.Create(nameof(BallAttached), typeof(bool), typeof(BallControl), true);

        public static readonly BindableProperty ColorProperty =
        BindableProperty.Create(nameof(Color), typeof(Color), typeof(BallControl), Colors.Black);

        public static readonly BindableProperty LocationProperty =
            BindableProperty.Create(nameof(Location), typeof(Vector3), typeof(BallControl), Vector3.Zero);

        public static readonly BindableProperty SizeProperty =
        BindableProperty.Create(nameof(Size), typeof(Vector3), typeof(BallControl), Vector3.Zero);

        public static readonly BindableProperty StartProperty =
        BindableProperty.Create(nameof(Start), typeof(Vector3), typeof(BallControl), Vector3.Zero);

        public static readonly BindableProperty VelocityProperty =
        BindableProperty.Create(nameof(Velocity), typeof(Vector3), typeof(BallControl), Vector3.Zero);

        public bool BallAttached
        {
            get { return (bool)GetValue(BallAttachedProperty); }
            set { SetValue(BallAttachedProperty, value); }
        }


        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public Vector3 Location
        {
            get { return (Vector3)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public SizeF Size
        {
            get { return (SizeF)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public Vector3 Start
        {
            get { return (Vector3)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        public Vector3 Velocity
        {
            get => (Vector3)GetValue(VelocityProperty);
            set { SetValue(VelocityProperty, value); }
        }

    }
}

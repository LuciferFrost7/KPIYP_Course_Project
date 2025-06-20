using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace The_Fallen_Snow_WPF.classes
{
    internal class GlowEffect
    {
        public static Ellipse CreateGlow(Canvas parentCanvas, double width, double height, double left, double top, Color centerColor, double pulseDuration = 1200)
        {
            var glowBrush = new RadialGradientBrush
            {
                GradientOrigin = new Point(0.5, 0.5),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5
            };

            glowBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, centerColor.R, centerColor.G, centerColor.B), 0.0));
            glowBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0, centerColor.R, centerColor.G, centerColor.B), 0.55));

            var glow = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = glowBrush,
                Opacity = 0,
                IsHitTestVisible = false
            };

            Canvas.SetLeft(glow, left);
            Canvas.SetTop(glow, top);

            parentCanvas.Children.Add(glow);
            return glow;
        }

        public static void StartPulsing(Ellipse glowElement, double pulseDuration = 1200)
        {
            var anim = new DoubleAnimation
            {
                From = 1.0,
                To = 0.05,
                Duration = TimeSpan.FromMilliseconds(pulseDuration),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            glowElement.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        public static void StopPulsing(Ellipse glowElement)
        {
            glowElement.BeginAnimation(UIElement.OpacityProperty, null);
            glowElement.Opacity = 0;
        }
    }
}

using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trabajo_DEINT_PasapalabraUI.Utils
{
    public class clsCircularPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in Children)
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            return base.MeasureOverride(availableSize);
        }

        // Arrange stuff in a circle
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count > 0)
            {
                // Center & radius of panel
                var ch = Children.FirstOrDefault().DesiredSize.Height; 
                var cw = Children.FirstOrDefault().DesiredSize.Width; 
                Point center = new Point((finalSize.Width - cw) / 2, (finalSize.Height - ch) / 2);
                double radius = Math.Min(finalSize.Width, finalSize.Height) / 2.0;
                radius *= 0.8;   // To avoid hitting edges

                // # radians between children
                double angleIncrRadians = 2.0 * Math.PI / Children.Count;

                double angleInRadians = -1.5708;

                foreach (UIElement child in Children)
                {
                    Point childPosition = new Point(
                        radius * Math.Cos(angleInRadians) + center.X,
                        radius * Math.Sin(angleInRadians) + center.Y);

                    child.Arrange(new Rect(childPosition, child.DesiredSize));

                    angleInRadians += angleIncrRadians;
                }
            }

            return finalSize;
        }
    }
}

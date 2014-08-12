namespace System.Windows.Controls.Samples
{
    public class RadialPanel : Panel
    {
        public static double GetAngle(DependencyObject obj)
        {
            return (double)obj.GetValue(AngleProperty);
        }

        public static void SetAngle(DependencyObject obj, double value)
        {
            obj.SetValue(AngleProperty, value);
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.RegisterAttached("Angle", typeof(double), typeof(RadialPanel), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsParentArrange));

        public static readonly DependencyProperty UseCircleProperty =
            DependencyProperty.Register("UseCircle", typeof(bool), typeof(RadialPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange));

        public bool UseCircle
        {
            get { return (bool)GetValue(UseCircleProperty); }
            set { SetValue(UseCircleProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement elem in InternalChildren)
            {
                elem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (InternalChildren.Count == 0)
            {
                return finalSize;
            }
            double angle = 0;

            double incrementalAngularSpace = (360.0 / InternalChildren.Count) * (Math.PI / 180);

            // an approximate radii based on the avialable size
            double radiusX, radiusY;
            if (UseCircle)
            {
                double size = Math.Min(finalSize.Width, finalSize.Height) / 2.4;
                radiusX = size;
                radiusY = size;
            }
            else
            {
                radiusX = finalSize.Width / 2.4;
                radiusY = finalSize.Height / 2.4;
            }

            foreach (UIElement elem in InternalChildren)
            {
                var localAngle = GetAngle(elem);
                if (double.IsNaN(localAngle))
                {
                    localAngle = angle;
                    angle += incrementalAngularSpace;
                }
                else
                {
                    localAngle *= (Math.PI / 180);
                }
                var childPoint = new Point(Math.Cos(localAngle) * radiusX, -Math.Sin(localAngle) * radiusY);
                var actualChildPoint = new Point(
                    finalSize.Width / 2 + childPoint.X - elem.DesiredSize.Width / 2,
                    finalSize.Height / 2 + childPoint.Y - elem.DesiredSize.Height / 2);

                elem.Arrange(new Rect(actualChildPoint.X, actualChildPoint.Y,
                    elem.DesiredSize.Width, elem.DesiredSize.Height));
            }
            return finalSize;
        }
    }
}
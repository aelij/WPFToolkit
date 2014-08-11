// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
// All other rights reserved.

using System.Windows.Media.Animation;

namespace System.Windows.Controls.Samples
{
    public class GridLengthAnimation : AnimationTimeline
    {
        public static readonly DependencyProperty FromProperty;
        public static readonly DependencyProperty ToProperty;

        static GridLengthAnimation()
        {
            FromProperty = DependencyProperty.Register("From", typeof (GridLength), typeof (GridLengthAnimation));
            ToProperty = DependencyProperty.Register("To", typeof (GridLength), typeof (GridLengthAnimation));
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        public override Type TargetPropertyType
        {
            get { return typeof (GridLength); }
        }

        public GridLength From
        {
            get { return (GridLength) GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public GridLength To
        {
            get { return (GridLength) GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue,
            AnimationClock animationClock)
        {
            double fromValue = ((GridLength) GetValue(FromProperty)).Value;
            double toValue = ((GridLength) GetValue(ToProperty)).Value;

            if (fromValue > toValue)
            {
                return new GridLength((1 - animationClock.CurrentProgress.Value)*(fromValue - toValue) + toValue,
                    To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
            return new GridLength((animationClock.CurrentProgress.Value)*(toValue - fromValue) + fromValue,
                To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
        }
    }
}
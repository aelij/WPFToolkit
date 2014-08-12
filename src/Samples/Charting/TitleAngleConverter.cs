using System.Globalization;
using System.Windows.Data;

namespace System.Windows.Controls.Samples
{
    public class TitleAngleConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // (ActualOffsetRatio + ActualRatio / 2) * 360
            return 90 - ((double)values[0] + (double)values[1] / 2) * 360;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
// All other rights reserved.

using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace System.Windows.Controls.Samples
{
    /// <summary>
    ///     A simple value converter.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class DictionaryKeyValueConverter<TKey, TValue> : IValueConverter
    {
        /// <summary>
        ///     Converts the value back.
        /// </summary>
        /// <param name="value">The object reference.</param>
        /// <param name="targetType">The type object.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The optional culture.</param>
        /// <returns>Returns an object or null.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        /// <summary>
        ///     Convert the object to a string.
        /// </summary>
        /// <param name="value">The object reference.</param>
        /// <param name="targetType">The type object.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The optional culture.</param>
        /// <returns>Returns an object or null.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is KeyValuePair<TKey, TValue>)
            {
                var pair = (KeyValuePair<TKey, TValue>) value;
                return pair.Key.ToString();
            }

            return null;
        }
    }
}
﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls.DataVisualization.Charting
{
    /// <summary>
    ///     A label used to display numeric axis values.
    /// </summary>
    public class NumericAxisLabel : AxisLabel
    {
#if !SILVERLIGHT
        /// <summary>
        ///     Initializes the static members of the NumericAxisLabel class.
        /// </summary>
        static NumericAxisLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (NumericAxisLabel),
                new FrameworkPropertyMetadata(typeof (NumericAxisLabel)));
        }

#endif
    }
}
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls.DataVisualization.Charting
{
    /// <summary>
    ///     Represents a data point used for a column series.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [TemplateVisualState(Name = StateCommonNormal, GroupName = GroupCommonStates)]
    [TemplateVisualState(Name = StateCommonMouseOver, GroupName = GroupCommonStates)]
    [TemplateVisualState(Name = StateSelectionUnselected, GroupName = GroupSelectionStates)]
    [TemplateVisualState(Name = StateSelectionSelected, GroupName = GroupSelectionStates)]
    [TemplateVisualState(Name = StateRevealShown, GroupName = GroupRevealStates)]
    [TemplateVisualState(Name = StateRevealHidden, GroupName = GroupRevealStates)]
    public class ColumnDataPoint : DataPoint
    {
#if !SILVERLIGHT
        /// <summary>
        ///     Initializes the static members of the ColumnDataPoint class.
        /// </summary>
        static ColumnDataPoint()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ColumnDataPoint),
                new FrameworkPropertyMetadata(typeof (ColumnDataPoint)));
        }

#endif
    }
}
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Collections.Generic;

namespace System.Windows.Controls.DataVisualization.Charting
{
    /// <summary>
    ///     Represents a control that contains a data series to be rendered in X/Y scatter format.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [StyleTypedProperty(Property = DataPointStyleName, StyleTargetType = typeof (ScatterDataPoint))]
    [StyleTypedProperty(Property = "LegendItemStyle", StyleTargetType = typeof (LegendItem))]
    [TemplatePart(Name = PlotAreaName, Type = typeof (Canvas))]
    public class ScatterSeries : DataPointSingleSeriesWithAxes
    {
        /// <summary>
        ///     Gets the dependent axis as a range axis.
        /// </summary>
        public IRangeAxis ActualDependentRangeAxis
        {
            get { return InternalActualDependentAxis as IRangeAxis; }
        }

        #region public IRangeAxis DependentRangeAxis

        /// <summary>
        ///     Gets or sets the dependent range axis.
        /// </summary>
        public IRangeAxis DependentRangeAxis
        {
            get { return GetValue(DependentRangeAxisProperty) as IRangeAxis; }
            set { SetValue(DependentRangeAxisProperty, value); }
        }

        /// <summary>
        ///     Identifies the DependentRangeAxis dependency property.
        /// </summary>
        public static readonly DependencyProperty DependentRangeAxisProperty =
            DependencyProperty.Register(
                "DependentRangeAxis",
                typeof (IRangeAxis),
                typeof (ScatterSeries),
                new PropertyMetadata(null, OnDependentRangeAxisPropertyChanged));

        /// <summary>
        ///     DependentRangeAxisProperty property changed handler.
        /// </summary>
        /// <param name="d">ScatterSeries that changed its DependentRangeAxis.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnDependentRangeAxisPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (ScatterSeries) d;
            var newValue = (IRangeAxis) e.NewValue;
            source.OnDependentRangeAxisPropertyChanged(newValue);
        }

        /// <summary>
        ///     DependentRangeAxisProperty property changed handler.
        /// </summary>
        /// <param name="newValue">New value.</param>
        private void OnDependentRangeAxisPropertyChanged(IRangeAxis newValue)
        {
            InternalDependentAxis = newValue;
        }

        #endregion public IRangeAxis DependentRangeAxis

        /// <summary>
        ///     Gets the independent axis as a range axis.
        /// </summary>
        public IAxis ActualIndependentAxis
        {
            get { return InternalActualIndependentAxis; }
        }

        #region public IAxis IndependentAxis

        /// <summary>
        ///     Gets or sets the independent range axis.
        /// </summary>
        public IAxis IndependentAxis
        {
            get { return GetValue(IndependentAxisProperty) as IAxis; }
            set { SetValue(IndependentAxisProperty, value); }
        }

        /// <summary>
        ///     Identifies the IndependentAxis dependency property.
        /// </summary>
        public static readonly DependencyProperty IndependentAxisProperty =
            DependencyProperty.Register(
                "IndependentAxis",
                typeof (IAxis),
                typeof (ScatterSeries),
                new PropertyMetadata(null, OnIndependentAxisPropertyChanged));

        /// <summary>
        ///     IndependentAxisProperty property changed handler.
        /// </summary>
        /// <param name="d">ScatterSeries that changed its IndependentAxis.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIndependentAxisPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (ScatterSeries) d;
            var newValue = (IAxis) e.NewValue;
            source.OnIndependentAxisPropertyChanged(newValue);
        }

        /// <summary>
        ///     IndependentAxisProperty property changed handler.
        /// </summary>
        /// <param name="newValue">New value.</param>
        private void OnIndependentAxisPropertyChanged(IAxis newValue)
        {
            InternalIndependentAxis = newValue;
        }

        #endregion public IAxis IndependentAxis

        /// <summary>
        ///     Acquire a horizontal linear axis and a vertical linear axis.
        /// </summary>
        /// <param name="firstDataPoint">The first data point.</param>
        protected override void GetAxes(DataPoint firstDataPoint)
        {
            GetAxes(
                firstDataPoint,
                axis => axis.Orientation == AxisOrientation.X,
                () =>
                {
                    IAxis axis = CreateRangeAxisFromData(firstDataPoint.IndependentValue);
                    if (axis == null)
                    {
                        axis = new CategoryAxis();
                    }
                    axis.Orientation = AxisOrientation.X;
                    return axis;
                },
                axis => axis.Orientation == AxisOrientation.Y && axis is IRangeAxis,
                () =>
                {
                    var axis = (DisplayAxis) CreateRangeAxisFromData(firstDataPoint.DependentValue);
                    if (axis == null)
                    {
                        throw new InvalidOperationException(
                            Properties.Resources
                                .DataPointSeriesWithAxes_NoSuitableAxisAvailableForPlottingDependentValue);
                    }
                    axis.ShowGridLines = true;
                    axis.Orientation = AxisOrientation.Y;
                    return axis;
                });
        }

        /// <summary>
        ///     Creates a new scatter data point.
        /// </summary>
        /// <returns>A scatter data point.</returns>
        protected override DataPoint CreateDataPoint()
        {
            return new ScatterDataPoint();
        }

        /// <summary>
        ///     Returns the custom ResourceDictionary to use for necessary resources.
        /// </summary>
        /// <returns>
        ///     ResourceDictionary to use for necessary resources.
        /// </returns>
        protected override IEnumerator<ResourceDictionary> GetResourceDictionaryEnumeratorFromHost()
        {
            return GetResourceDictionaryWithTargetType(SeriesHost, typeof (ScatterDataPoint), true);
        }

        /// <summary>
        ///     This method updates a single data point.
        /// </summary>
        /// <param name="dataPoint">The data point to update.</param>
        protected override void UpdateDataPoint(DataPoint dataPoint)
        {
            double PlotAreaHeight =
                ActualDependentRangeAxis.GetPlotAreaCoordinate(ActualDependentRangeAxis.Range.Maximum).Value;
            double dataPointX = ActualIndependentAxis.GetPlotAreaCoordinate(dataPoint.ActualIndependentValue).Value;
            double dataPointY = ActualDependentRangeAxis.GetPlotAreaCoordinate(dataPoint.ActualDependentValue).Value;

            if (ValueHelper.CanGraph(dataPointX) && ValueHelper.CanGraph(dataPointY))
            {
                dataPoint.Visibility = Visibility.Visible;

                // Set the Position
                Canvas.SetLeft(
                    dataPoint,
                    Math.Round(dataPointX - (dataPoint.ActualWidth/2)));
                Canvas.SetTop(
                    dataPoint,
                    Math.Round(PlotAreaHeight - (dataPointY + (dataPoint.ActualHeight/2))));
            }
            else
            {
                dataPoint.Visibility = Visibility.Collapsed;
            }
        }
    }
}
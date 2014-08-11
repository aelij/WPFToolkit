// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.ComponentModel;

namespace System.Windows.Controls.Samples
{
    /// <summary>
    /// Sample page demonstrating AreaSeries.
    /// </summary>
    [Sample("Area Series", DifficultyLevel.Basic, "Area Series")]
    public partial class AreaSample : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the AreaSample class.
        /// </summary>
        public AreaSample()
        {
            InitializeComponent();

            SampleGenerators.GenerateNumericSeriesSamples(GeneratedChartsPanel, () => new AreaSeries(), true);
            SampleGenerators.GenerateDateTimeValueSeriesSamples(GeneratedChartsPanel, () => new AreaSeries());
            SampleGenerators.GenerateMultipleValueSeriesSamples(GeneratedChartsPanel, () => new AreaSeries(), true);
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SampleAttribute : Attribute
    {
        public string Name { get; private set; }
        public DifficultyLevel Level { get; private set; }
        public string Description { get; private set; }

        public SampleAttribute(string name, DifficultyLevel level, string description)
        {
            Name = name;
            Level = level;
            Description = description;
        }
    }

    /// <summary>
    /// Describes the level of difficulty of the sample.
    /// </summary>
    public enum DifficultyLevel
    {
        /// <summary>
        /// Used when Sample does not need to be categorized.
        /// </summary>
        None = 0,
        /// <summary>
        /// Used for basic samples.
        /// </summary>
        Basic = 1,
        /// <summary>
        /// Used for intermediate samples.
        /// </summary>
        Intermediate = 2,
        /// <summary>
        /// Used for advanced samples.
        /// </summary>
        Advanced = 3,
        /// <summary>
        /// Used for scenario samples.
        /// </summary>
        Scenario = 4
    }
}
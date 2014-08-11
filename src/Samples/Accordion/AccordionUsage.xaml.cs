// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
// All other rights reserved.

using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Windows.Controls.Samples
{
    /// <summary>
    ///     Sample page for Accordion, showing usages.
    /// </summary>
#if SILVERLIGHT
    [Sample("Accordion Usage samples", DifficultyLevel.Basic)]
#endif
    [Category("Accordion")]
    public partial class AccordionUsage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccordionUsage" /> class.
        /// </summary>
        public AccordionUsage()
        {
            InitializeComponent();

            KeyValuePair<string, string>[] data =
            {
                new KeyValuePair<string, string>("Hello", "World"),
                new KeyValuePair<string, string>("foo", "bar"),
                new KeyValuePair<string, string>("Silverlight", "Toolkit")
            };

            // initialize accordions
            accordionGeneratedContent.ItemsSource = data;
            accordionDefaultHeaderTemplate.ItemsSource = data;
            accordionCLRTypes.ItemsSource = data;
            accordionAccordionItem.ItemsSource = data;
        }

        /// <summary>
        ///     Handles the SelectionChanged event of the accordion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event
        ///     data.
        /// </param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Hooked up in Xaml"
            )]
        private void Accordion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (AccordionItem selectedAccordionItem in e.AddedItems)
            {
                Debug.WriteLine("AccordionItem {0} has been selected.",
                    selectedAccordionItem.Header);
            }
        }

        /// <summary>
        ///     Handles the Click event of the btnExpandAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Hooked up in Xaml"
            )]
        private void ExpandAll_Click(object sender, RoutedEventArgs e)
        {
            accordionExpandCollapse.SelectAll();
        }

        /// <summary>
        ///     Handles the Click event of the btnCollapseAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Hooked up in Xaml")]
        private void CollapseAll_Click(object sender, RoutedEventArgs e)
        {
            accordionExpandCollapse.UnselectAll();
        }

        /// <summary>
        ///     Handles the SelectionChanged event for the CLRTypes sample.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event
        ///     data.
        /// </param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Hooked up in Xaml")]
        private void ClrTypesSelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (
                KeyValuePair<string, string> keyValuePair in
                    accordionCLRTypes.SelectedItems)
            {
                Debug.WriteLine("Interesting, people like seeing details on " +
                                keyValuePair.Key);
            }
        }

        /// <summary>
        ///     Hooks up events for the MouseOver sample.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "keyValuePair",
            Justification = "TODO: THIS WILL BE REMOVED ONCE ITEM CONTAINER GENERATOR STUFF IS TAKEN CARE OF. TODO.")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Hooked up in Xaml")]
        private void SetMouseEvents(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<string, string> keyValuePair in accordionAccordionItem.Items)
            {
                var container =
                    accordionAccordionItem.ItemContainerGenerator.ContainerFromItem(keyValuePair) as AccordionItem;
                if (container != null)
                {
                    container.MouseEnter += (s, args) =>
                    {
                        if (!container.IsLocked)
                        {
                            container.IsSelected = true;
                        }
                    };

                    container.MouseLeave += (s, args) =>
                    {
                        if (!container.IsLocked)
                        {
                            container.IsSelected = false;
                        }
                    };
                }
            }
        }
    }
}
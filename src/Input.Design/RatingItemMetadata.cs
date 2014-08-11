// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
// All other rights reserved.

extern alias Silverlight;
using System.Windows.Controls.Design.Common;
using Microsoft.Windows.Design.Features;
using Microsoft.Windows.Design.Metadata;
using SSWC = Silverlight::System.Windows.Controls;

namespace System.Windows.Controls.Layout.Design
{
    /// <summary>
    ///     To register design time metadata for RatingItem.
    /// </summary>
    internal class RatingItemMetadata : AttributeTableBuilder
    {
        /// <summary>
        ///     To register design time metadata for RatingItem.
        /// </summary>
        public RatingItemMetadata()
        {
            AddCallback(
                typeof (SSWC.RatingItem),
                b => b.AddCustomAttributes(new FeatureAttribute(typeof (EmptyDefaultInitializer))));
        }
    }
}
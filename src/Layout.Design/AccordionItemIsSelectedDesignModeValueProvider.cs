// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
// All other rights reserved.

extern alias Silverlight;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls.Design.Common;
using Microsoft.Windows.Design.Metadata;
using SSWC = Silverlight::System.Windows.Controls;
#if !SILVERLIGHT

#endif

namespace System.Windows.Controls.Layout.Design
{
    /// <summary>
    ///     Design mode value provider for AccordionItem.IsDropDownOpen property.
    /// </summary>
    internal class AccordionItemIsSelectedDesignModeValueProvider
        : TrueIfSelectedDesignModeValueProvider<SSWC.AccordionItem>
    {
        /// <summary>
        ///     Identifier of the property this DMVP is for.
        /// </summary>
        private static PropertyIdentifier _propertyIdentifier;

        /// <summary>
        ///     Use the static constructor to add one property identifier to base.Identifiers.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Need the static constructor to ensure static initialization sequence.")]
        static AccordionItemIsSelectedDesignModeValueProvider()
        {
            _propertyIdentifier = new PropertyIdentifier(typeof (SSWC.AccordionItem), "IsSelected");
            Identifiers.Add(typeof (SSWC.AccordionItem), _propertyIdentifier);
        }

        /// <summary>
        ///     Default constructor to add the property for design time translation.
        /// </summary>
        public AccordionItemIsSelectedDesignModeValueProvider()
        {
#if SILVERLIGHT
            Debug.Assert(!_propertyIdentifier.IsEmpty, "Static constructor should have been called!");
#else
            Debug.Assert(!String.IsNullOrEmpty(_propertyIdentifier.Name), "Static constructor should have been called!");
#endif
            Properties.Add(_propertyIdentifier);
        }
    }
}
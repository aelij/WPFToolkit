﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
// All other rights reserved.

extern alias Silverlight;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Windows.Design.Interaction;
using Microsoft.Windows.Design.Metadata;
using Microsoft.Windows.Design.Model;
using Microsoft.Windows.Design.Policies;
#if SILVERLIGHT
using SSW = Silverlight::System.Windows;
#else
using SSW = System.Windows;
#endif

namespace System.Windows.Controls.Design.Common
{
    /// <summary>
    ///     Design mode value provider base class that always returns true for a Boolean property
    ///     of a model item when the item or any of its visual tree descendants is selected; and
    ///     returns the real value of the property when the item is de-selected. This is used to
    ///     expand and show hidden parts during of control at design time.
    /// </summary>
    /// <remarks>
    ///     * SelectionPolicy can't be applied to DesignModeValueProvider directoly,
    ///     so the AdornerProxy is needed as a proxy to apply the selection policy;
    ///     * The use of static variable InSelection is OK, because UI is singled
    ///     threaded, and ValueTranslationService.InvalidateProperty is synchronous.
    ///     * Nest SelfOrDescendantSelectedPolicy and AdornerProxy inside the
    ///     TrueIfSelectedDesignModeValueProvider because they are tightly coupled.
    ///     * No need to abstract the DMVP class further to support multiple porperties,
    ///     types, and/or custom value translation logic yet.
    /// </remarks>
    /// <typeparam name="T">
    ///     The Silverlight type whose property this DMVP is used for.
    /// </typeparam>
    internal class TrueIfSelectedDesignModeValueProvider<T> : DesignModeValueProvider
        where T : FrameworkElement
    {
        /// <summary>
        ///     Gets the static dictionary of control type and its Boolean property to be translated.
        /// </summary>
        protected static Dictionary<Type, PropertyIdentifier> Identifiers { get; private set; }

#if SILVERLIGHT
    /// <summary>
    /// Flag to TranslatePropertyValue method whether it is called by 
    /// AdornerProxy for the model item selected or de-selected.
    /// </summary>
        private static bool InSelection;
#endif

        /// <summary>
        ///     Default constructor to add the property for translation.
        /// </summary>
        static TrueIfSelectedDesignModeValueProvider()
        {
            Identifiers = new Dictionary<Type, PropertyIdentifier>();
        }

        #if SILVERLIGHT
    /// <param name="item">The model item whose property is to be translated.</param>
        public override object TranslatePropertyValue(
            ModelItem item,
            PropertyIdentifier identifier,
            object value)
#else
        /// <summary>
        ///     Translate property value at design time.
        /// </summary>
        /// <param name="identifier">The property to translate.</param>
        /// <param name="value">Value of the property.</param>
        /// <returns>Translated value of the property at design time.</returns>
        /// <param name="item">The model item whose property is to be translated.</param>
        public override object TranslatePropertyValue(ModelItem item, PropertyIdentifier identifier, object value)
#endif
        {
            Debug.Assert(
                item != null && !IsIdentifierEmpty(identifier) && typeof (bool).IsAssignableFrom(value.GetType()),
                "TranslatePropetyValue is called with invalid parameters!");
            Debug.Assert(
                typeof (T).IsAssignableFrom(item.ItemType),
                GetType() + " shouldn't be applied to this model item!");
            Debug.Assert(
                Identifiers.ContainsKey(item.ItemType) && Identifiers[item.ItemType] == identifier,
                "The passed in PropertyIdentifier isn't registered!");

            if (item != null && !IsIdentifierEmpty(identifier))
            {
                Type type = item.ItemType;
                PropertyIdentifier property;
                if (Identifiers.TryGetValue(type, out property) && property == identifier)
                {
#if SILVERLIGHT
                    return (bool)value || InSelection;
#else
                    return (bool) value;
#endif
                }
            }

#if SILVERLIGHT
            return base.TranslatePropertyValue(item, identifier, value);
#else
            return base.TranslatePropertyValue(item, identifier, value);
#endif
        }

        private static bool IsIdentifierEmpty(PropertyIdentifier identifier)
        {
#if SILVERLIGHT
            return identifier.IsEmpty;
#else
            return String.IsNullOrEmpty(identifier.Name);
#endif
        }

        #region public class SelfOrDescendantSelectedPolicy

        /// <summary>
        ///     Item policy to apply extensibility features when an model item
        ///     or any of its visual tree descendants is selected or de-selected.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses",
            Justification = "It is used as attribute.")]
        public class SelfOrDescendantSelectedPolicy : SelectionPolicy
        {
            /// <summary>
            ///     Return the policy items from the specified selection.
            /// </summary>
            /// <param name="selection">The current selection.</param>
            /// <returns>An enumeration of ModelItem objects to use for this policy.</returns>
            protected override IEnumerable<ModelItem> GetPolicyItems(Selection selection)
            {
                Debug.Assert(selection != null, "GetPolicyItems is called with invalid parameter!");

                if (selection != null)
                {
                    ModelItem primarySelection = selection.PrimarySelection;

                    if (primarySelection != null)
                    {
                        for (ModelItem item = primarySelection; item != null; item = item.Parent)
                        {
                            if (typeof (T).IsAssignableFrom(item.ItemType))
                            {
                                yield return item;
                            }
                        }
                    }
                }
            }
        }

        #endregion public class SelfOrDescendantSelectedPolicy
    }
}
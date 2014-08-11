// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Windows.Controls.DataVisualization
{
    /// <summary>
    ///     A bag of weak references to items.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    internal class WeakReferenceBag<T> : IEnumerable<T>
        where T : class
    {
        /// <summary>
        ///     Gets or sets the list of event listeners.
        /// </summary>
        private IList<WeakReference> Items { get; set; }

        /// <summary>
        ///     Initializes a new instance of the WeakEvent class.
        /// </summary>
        public WeakReferenceBag()
        {
            Items = new List<WeakReference>();
        }

        /// <summary>
        ///     Adds an item to the bag.
        /// </summary>
        /// <param name="item">The item to add to the bag.</param>
        public void Add(T item)
        {
            Debug.Assert(item != null, "listener must not be null.");
            Items.Add(new WeakReference(item));
        }

        /// <summary>
        ///     Removes an item from the bag.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove(T item)
        {
            Debug.Assert(item != null, "listener must not be null.");
            int count = 0;
            while (count < Items.Count)
            {
                object target = Items[count].Target;
                if (!Items[count].IsAlive || ReferenceEquals(target, item))
                {
                    Items.RemoveAt(count);
                }
                else
                {
                    count++;
                }
            }
        }

        /// <summary>
        ///     Returns a sequence of the elements in the bag.
        /// </summary>
        /// <returns>A sequence of the elements in the bag.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int count = 0;
            while (count < Items.Count)
            {
                object target = Items[count].Target;
                if (!Items[count].IsAlive)
                {
                    Items.RemoveAt(count);
                }
                else
                {
                    yield return (T) target;
                    count++;
                }
            }
        }

        /// <summary>
        ///     Returns a sequence of the elements in the bag.
        /// </summary>
        /// <returns>A sequence of the elements in the bag.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }
    }
}
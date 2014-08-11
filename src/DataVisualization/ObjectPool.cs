// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Collections.Generic;
using System.Diagnostics;

namespace System.Windows.Controls.DataVisualization
{
    /// <summary>
    ///     A pool of objects that can be reused.
    /// </summary>
    /// <typeparam name="T">The type of object in the pool.</typeparam>
    internal class ObjectPool<T>
    {
        /// <summary>
        ///     The default minimum number objects to keep in the pool.
        /// </summary>
        private const int DefaultMinimumObjectsInThePool = 10;

#if DEBUG
        /// <summary>
        ///     A value indicating whether the pool is being traversed.
        /// </summary>
        private bool _traversing;

#endif

        /// <summary>
        ///     A function which creates objects.
        /// </summary>
        private readonly Func<T> _createObject;

        /// <summary>
        ///     The list of objects.
        /// </summary>
        private readonly List<T> _objects;

        /// <summary>
        ///     The index of the current item in the list.
        /// </summary>
        private int _currentIndex;

        /// <summary>
        ///     The minimum number of objects to keep in the pool.
        /// </summary>
        private readonly int _minimumObjectsInThePool;

        /// <summary>
        ///     Initializes a new instance of the ObjectPool class.
        /// </summary>
        /// <param name="minimumObjectsInThePool">
        ///     The minimum number of objects
        ///     to keep in the pool.
        /// </param>
        /// <param name="createObject">
        ///     The function that creates the objects.
        /// </param>
        public ObjectPool(
            int minimumObjectsInThePool,
            Func<T> createObject)
        {
            _objects = new List<T>(minimumObjectsInThePool);
            _minimumObjectsInThePool = minimumObjectsInThePool;
            _createObject = createObject;

            Reset();
        }

        /// <summary>
        ///     Initializes a new instance of the ObjectPool class.
        /// </summary>
        /// <param name="createObject">
        ///     The function that creates the objects.
        /// </param>
        public ObjectPool(Func<T> createObject)
            : this(DefaultMinimumObjectsInThePool, createObject)
        {
        }

        /// <summary>
        ///     Performs an operation on the subsequent, already-created objects
        ///     in the pool.
        /// </summary>
        /// <param name="action">
        ///     The action to perform on the remaining objects.
        /// </param>
        public void ForEachRemaining(Action<T> action)
        {
            for (var cnt = _currentIndex; cnt < _objects.Count; cnt++)
            {
                action(_objects[cnt]);
            }
        }

        /// <summary>
        ///     Creates a new object or reuses an existing object in the pool.
        /// </summary>
        /// <returns>A new or existing object in the pool.</returns>
        public T Next()
        {
            if (_currentIndex == _objects.Count)
            {
                _objects.Add(_createObject());
            }
            T item = _objects[_currentIndex];
            _currentIndex++;
            return item;
        }

        /// <summary>
        ///     Resets the pool of objects.
        /// </summary>
        public void Reset()
        {
#if DEBUG
            _traversing = true;
#endif
            _currentIndex = 0;
        }

        /// <summary>
        ///     Finishes the object creation process.
        /// </summary>
        /// <remarks>
        ///     If there are substantially more remaining objects in the pool those
        ///     objects may be removed.
        /// </remarks>
        public void Done()
        {
#if DEBUG
            _traversing = false;
#endif
            // Remove the rest of the objects if we are using less than 
            // half of the pool.
            if (_currentIndex != 0 && _objects.Count > 0 && _currentIndex >= _minimumObjectsInThePool &&
                _currentIndex < _objects.Count/2)
            {
                _objects.RemoveRange(_currentIndex, _objects.Count - _currentIndex);
            }
        }

        /// <summary>
        ///     Removes the objects from the pool.
        /// </summary>
        public void Clear()
        {
#if DEBUG
            Debug.Assert(!_traversing, "Cannot clear an object pool while it is being traversed.");
#endif
            _objects.Clear();
        }
    }
}
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Controls.DataVisualization
{
    /// <summary>
    ///     A range of values.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <QualityBand>Preview</QualityBand>
    public struct Range<T>
    {
        /// <summary>
        ///     A flag that determines whether the range is empty or not.
        /// </summary>
        private readonly bool _hasData;

        /// <summary>
        ///     Gets a value indicating whether the range is empty or not.
        /// </summary>
        public bool HasData
        {
            get { return _hasData; }
        }

        /// <summary>
        ///     The maximum value in the range.
        /// </summary>
        private readonly T _maximum;

        /// <summary>
        ///     Gets the maximum value in the range.
        /// </summary>
        public T Maximum
        {
            get
            {
                if (!HasData)
                {
                    throw new InvalidOperationException(
                        Properties.Resources.Range_get_Maximum_CannotReadTheMaximumOfAnEmptyRange);
                }
                return _maximum;
            }
        }

        /// <summary>
        ///     The minimum value in the range.
        /// </summary>
        private readonly T _minimum;

        /// <summary>
        ///     Gets the minimum value in the range.
        /// </summary>
        public T Minimum
        {
            get
            {
                if (!HasData)
                {
                    throw new InvalidOperationException(
                        Properties.Resources.Range_get_Minimum_CannotReadTheMinimumOfAnEmptyRange);
                }
                return _minimum;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the Range class.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        public Range(T minimum, T maximum)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (minimum == null)
            {
                throw new ArgumentNullException("minimum");
            }
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (maximum == null)
            {
                throw new ArgumentNullException("maximum");
            }

            _hasData = true;
            _minimum = minimum;
            _maximum = maximum;

            int compareValue = Comparer<T>.Default.Compare(minimum, maximum);
            if (compareValue == 1)
            {
                throw new InvalidOperationException(
                    Properties.Resources.Range_ctor_MaximumValueMustBeLargerThanOrEqualToMinimumValue);
            }
        }

        /// <summary>
        ///     Compare two ranges and return a value indicating whether they are
        ///     equal.
        /// </summary>
        /// <param name="leftRange">Left-hand side range.</param>
        /// <param name="rightRange">Right-hand side range.</param>
        /// <returns>A value indicating whether the ranges are equal.</returns>
        public static bool operator ==(Range<T> leftRange, Range<T> rightRange)
        {
            if (!leftRange.HasData)
            {
                return !rightRange.HasData;
            }
            if (!rightRange.HasData)
            {
                return !leftRange.HasData;
            }

            return leftRange.Minimum.Equals(rightRange.Minimum) && leftRange.Maximum.Equals(rightRange.Maximum);
        }

        /// <summary>
        ///     Compare two ranges and return a value indicating whether they are
        ///     not equal.
        /// </summary>
        /// <param name="leftRange">Left-hand side range.</param>
        /// <param name="rightRange">Right-hand side range.</param>
        /// <returns>
        ///     A value indicating whether the ranges are not equal.
        /// </returns>
        public static bool operator !=(Range<T> leftRange, Range<T> rightRange)
        {
            return !(leftRange == rightRange);
        }

        /// <summary>
        /// Casts the range to another type.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public Range<K> As<K>()
            where K : T
        {
            if (!HasData)
            {
                return new Range<K>();
            }
            return new Range<K>((K)Minimum, (K)Maximum);
        }

        /// <summary>
        ///     Adds a range to the current range.
        /// </summary>
        /// <param name="range">A range to add to the current range.</param>
        /// <returns>
        ///     A new range that encompasses the instance range and the
        ///     range parameter.
        /// </returns>
        public Range<T> Add(Range<T> range)
        {
            if (!HasData)
            {
                return range;
            }
            if (!range.HasData)
            {
                return this;
            }
            T minimum = Comparer<T>.Default.Compare(Minimum, range.Minimum) == -1 ? Minimum : range.Minimum;
            T maximum = Comparer<T>.Default.Compare(Maximum, range.Maximum) == 1 ? Maximum : range.Maximum;
            return new Range<T>(minimum, maximum);
        }

        /// <summary>
        ///     Compares the range to another range.
        /// </summary>
        /// <param name="range">A different range.</param>
        /// <returns>A value indicating whether the ranges are equal.</returns>
        public bool Equals(Range<T> range)
        {
            return this == range;
        }

        /// <summary>
        ///     Compares the range to an object.
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns>
        ///     A value indicating whether the other object is a range,
        ///     and if so, whether that range is equal to the instance range.
        /// </returns>
        public override bool Equals(object obj)
        {
            var range = (Range<T>)obj;
            return this == range;
        }

        /// <summary>
        ///     Returns a value indicating whether a value is within a range.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Whether the value is within the range.</returns>
        public bool Contains(T value)
        {
            return Comparer<T>.Default.Compare(Minimum, value) <= 0 && Comparer<T>.Default.Compare(value, Maximum) <= 0;
        }

        /// <summary>
        ///     Returns a value indicating whether two ranges intersect.
        /// </summary>
        /// <param name="range">The range to compare against this range.</param>
        /// <returns>A value indicating whether the ranges intersect.</returns>
        public bool IntersectsWith(Range<T> range)
        {
            if (!HasData || !range.HasData)
            {
                return false;
            }

            Func<Range<T>, Range<T>, bool> rightCollidesWithLeft =
                (leftRange, rightRange) =>
                    (Comparer<T>.Default.Compare(rightRange.Minimum, leftRange.Maximum) <= 0 &&
                     Comparer<T>.Default.Compare(rightRange.Minimum, leftRange.Minimum) >= 0)
                    ||
                    (Comparer<T>.Default.Compare(leftRange.Minimum, rightRange.Maximum) <= 0 &&
                     Comparer<T>.Default.Compare(leftRange.Minimum, rightRange.Minimum) >= 0);

            return rightCollidesWithLeft(this, range) || rightCollidesWithLeft(range, this);
        }

        /// <summary>
        ///     Computes a hash code value.
        /// </summary>
        /// <returns>A hash code value.</returns>
        public override int GetHashCode()
        {
            if (!HasData)
            {
                return 0;
            }

            int num = 0x5374e861;
            num = (-1521134295 * num) + EqualityComparer<T>.Default.GetHashCode(Minimum);
            return ((-1521134295 * num) + EqualityComparer<T>.Default.GetHashCode(Maximum));
        }

        /// <summary>
        ///     Returns the string representation of the range.
        /// </summary>
        /// <returns>The string representation of the range.</returns>
        public override string ToString()
        {
            if (!HasData)
            {
                return Properties.Resources.Range_ToString_NoData;
            }
            return string.Format(CultureInfo.CurrentCulture, Properties.Resources.Range_ToString_Data, Minimum, Maximum);
        }
    }
}
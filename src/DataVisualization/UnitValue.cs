// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls.DataVisualization
{
    /// <summary>
    ///     A value in units.
    /// </summary>
    public struct UnitValue : IEquatable<UnitValue>, IComparable<UnitValue>
    {
        /// <summary>
        ///     Returns a UnitValue representing an invalid value.
        /// </summary>
        /// <returns>UnitValue instance.</returns>
        public static UnitValue NaN()
        {
            return new UnitValue { Value = double.NaN };
        }

        /// <summary>
        ///     Instantiates a new instance of the UnitValue struct.
        /// </summary>
        /// <param name="value">The value associated with the units.</param>
        /// <param name="unit">The units associated with the value.</param>
        public UnitValue(double value, Unit unit)
            : this()
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        ///     Gets the value associated with the units.
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        ///     Gets the units associated with the value.
        /// </summary>
        public Unit Unit { get; private set; }

        /// <summary>
        ///     Compares two unit values to determine if they are equal or not.
        /// </summary>
        /// <param name="obj">The object being compared.</param>
        /// <returns>
        ///     A number smaller than zero if the obj is larger than this
        ///     object.  A number equal to 0 if they are equal.  A number greater
        ///     than zero if this unit value is greater than obj.
        /// </returns>
        public int CompareTo(UnitValue obj)
        {
            var unitValue = obj;

            if (unitValue.Unit != Unit)
            {
                throw new InvalidOperationException("Cannot compare two unit values with different units.");
            }

            return Value.CompareTo(unitValue.Value);
        }

        public bool Equals(UnitValue other)
        {
            return Value.Equals(other.Value) && Unit == other.Unit;
        }

        /// <summary>
        ///     Determines if two values are equal.
        /// </summary>
        /// <param name="obj">The other value.</param>
        /// <returns>A value indicating whether values are equal.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is UnitValue && Equals((UnitValue)obj);
        }

        /// <summary>
        ///     Determines whether two unit value objects are equal.
        /// </summary>
        /// <param name="left">The left unit value.</param>
        /// <param name="right">The right unit value.</param>
        /// <returns>
        ///     A value indicating  whether two unit value objects are
        ///     equal.
        /// </returns>
        public static bool operator ==(UnitValue left, UnitValue right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Determines whether two unit value objects are not equal.
        /// </summary>
        /// <param name="left">The left unit value.</param>
        /// <param name="right">The right unit value.</param>
        /// <returns>
        ///     A value indicating whether two unit value objects are not
        ///     equal.
        /// </returns>
        public static bool operator !=(UnitValue left, UnitValue right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///     Determines whether the left value is smaller than the right.
        /// </summary>
        /// <param name="left">The left unit value.</param>
        /// <param name="right">The right unit value.</param>
        /// <returns>
        ///     A value indicating whether the left value is smaller than
        ///     the right.
        /// </returns>
        public static bool operator <(UnitValue left, UnitValue right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        ///     Determines whether the left value is larger than the right.
        /// </summary>
        /// <param name="left">The left unit value.</param>
        /// <param name="right">The right unit value.</param>
        /// <returns>
        ///     A value indicating whether the left value is larger than
        ///     the right.
        /// </returns>
        public static bool operator >(UnitValue left, UnitValue right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        ///     Returns the hash code of the unit value object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ (int)Unit;
            }
        }
    }
}
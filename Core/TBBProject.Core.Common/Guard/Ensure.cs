namespace TBBProject.Core.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public static class Ensure
    {
        [DebuggerStepThrough]
        public static Validation<T> That<T>(string name, T value) => new Validation<T>(name, value);

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> NotNull<T>(this Validation<T> validation)
            where T : class
        {
            if (ReferenceEquals(validation.Value, null))
            {
                throw new ArgumentNullException(validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T?> NotNull<T>(this Validation<T?> validation)
            where T : struct
        {
            if (!validation.Value.HasValue)
            {
                throw new ArgumentNullException(validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<string> NotNullOrEmpty(this Validation<string> validation)
        {
            if (string.IsNullOrEmpty(validation.Value))
            {
                throw new ArgumentNullException(validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<string> NotEmpty(this Validation<string> validation)
        {
            if (validation.Value.Length == 0)
            {
                throw new ArgumentException("Cannot be empty", validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> NotEmpty<T>(this Validation<T> validation)
            where T : ICollection
        {
            if (validation.Value.Count == 0)
            {
                throw new ArgumentException("Cannot be empty", validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<IEnumerable<T>> NotEmpty<T>(this Validation<IEnumerable<T>> validation)
        {
            if (validation.Value.Any() == false)
            {
                throw new ArgumentException("Cannot be empty", validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<IDictionary> NotEmpty<T>(this Validation<IDictionary> validation)
        {
            if (validation.Value.Count == 0)
            {
                throw new ArgumentException("Cannot be empty", validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T[]> NotEmpty<T>(this Validation<T[]> validation)
        {
            if (validation.Value.Length == 0)
            {
                throw new ArgumentException("Cannot be empty", validation.Name);
            }

            return validation;
        }

        public static Validation<T> NotZero<T>(this Validation<T> validation)
            where T : struct, IComparable<T>
        {
            if (validation.Value.CompareTo(default) == 0)
            {
                throw new ArgumentException("Value can not be zero");
            }

            return validation;
        }

        public static Validation<T> NotNegative<T>(this Validation<T> validation)
            where T : struct, IComparable<T>
        {
            if (validation.Value.CompareTo(default) < 0)
            {
                throw new ArgumentException("Value can not be negative");
            }

            return validation;
        }

        public static Validation<T> Positive<T>(this Validation<T> validation)
            where T : struct, IComparable<T>
        {
            if (validation.Value.CompareTo(default) < 1)
            {
                throw new ArgumentException("Value should be positive");
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> NotDefault<T>(this Validation<T> validation)
        {
            if (Equals(validation.Value, default(T)))
            {
                throw new ArgumentException("Cannot be equal to " + default(T), validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> OneOf<T>(
            this Validation<T> validation,
            params T[] values)
        {
            if (Array.IndexOf(values, validation.Value) == -1)
            {
                throw new ArgumentException("Not in the collection");
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> GreaterThan<T>(
            this Validation<T> validation, T value)
            where T : IComparable
        {
            if (validation.Value.CompareTo(value) <= 0)
            {
                throw new ArgumentOutOfRangeException(validation.Name, "Must be greater than " + value);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> GreaterThanOrEqualTo<T>(
            this Validation<T> validation, T value)
            where T : IComparable
        {
            if (validation.Value.CompareTo(value) < 0)
            {
                throw new ArgumentOutOfRangeException(validation.Name, "Must be greater than or equal to " + value);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> LessThan<T>(
            this Validation<T> validation, T value)
            where T : IComparable
        {
            if (validation.Value.CompareTo(value) >= 0)
            {
                throw new ArgumentOutOfRangeException(validation.Name, "Must be less than " + value);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> LessThanOrEqualTo<T>(
            this Validation<T> validation, T value)
            where T : IComparable
        {
            if (validation.Value.CompareTo(value) > 0)
            {
                throw new ArgumentOutOfRangeException(validation.Name, "Must be less than or equal to " + value);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> EqualTo<T>(
            this Validation<T> validation, T value)
        {
            if (!validation.Value.Equals(value))
            {
                throw new ArgumentException("Must be equal to " + value, validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> NotEqualTo<T>(this Validation<T> validation, T value)
        {
            if (validation.Value.Equals(value))
            {
                throw new ArgumentException("Cannot be equal to " + value, validation.Name);
            }

            return validation;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static Validation<T> InRange<T>(this Validation<T> validation, T lowerBound, T upperBound)
            where T : IComparable
        {
            var value = validation.Value;
            if (value.CompareTo(lowerBound) < 0 || value.CompareTo(upperBound) > 0)
            {
                throw new ArgumentOutOfRangeException(
                    validation.Name, string.Format("Must be between {0} and {1}", lowerBound, upperBound));
            }

            return validation;
        }
    }
}

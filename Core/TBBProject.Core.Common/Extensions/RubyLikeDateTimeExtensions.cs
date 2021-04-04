namespace TBBProject.Core.Common
{
    using System;

    public static class RubyLikeDateTimeExtensions
    {
        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// weeks represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="weeks">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="weeks"/>.</returns>
        public static TimeSpan Week(this int weeks)
        {
            return new TimeSpan(weeks * 7, 0, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// weeks represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="weeks">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="weeks"/>.</returns>
        public static TimeSpan Weeks(this int weeks)
        {
            return new TimeSpan(weeks * 7, 0, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// days represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="days">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="days"/>.</returns>
        public static TimeSpan Day(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// days represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="days">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="days"/>.</returns>
        public static TimeSpan Days(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// hours represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="hours">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="hours"/>.</returns>
        public static TimeSpan Hour(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// hours represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="hours">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="hours"/>.</returns>
        public static TimeSpan Hours(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// minutes represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="minutes">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="minutes"/>.</returns>
        public static TimeSpan Minute(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// minutes represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="minutes">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="minutes"/>.</returns>
        public static TimeSpan Minutes(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// seconds represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="seconds">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="seconds"/>.</returns>
        public static TimeSpan Second(this int seconds)
        {
            return new TimeSpan(0, 0, 0, seconds, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// seconds represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="seconds">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="seconds"/>.</returns>
        public static TimeSpan Seconds(this int seconds)
        {
            return new TimeSpan(0, 0, 0, seconds, 0);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// milliseconds represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="milliseconds">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="milliseconds"/>.</returns>
        public static TimeSpan Millisecond(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        /// <summary>
        /// Returns <see cref="System.TimeSpan"/> with the
        /// milliseconds represented by the current <see cref="int"/>.
        /// </summary>
        /// <param name="milliseconds">The current <see cref="int"/>.</param>
        /// <returns>A <see cref="System.TimeSpan"/> with the weeks specified by <paramref name="milliseconds"/>.</returns>
        public static TimeSpan Milliseconds(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }
    }
}

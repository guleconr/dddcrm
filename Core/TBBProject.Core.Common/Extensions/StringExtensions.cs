namespace TBBProject.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string Encode(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var encoded = WebUtility.HtmlEncode(text);

            return encoded;
        }

        public static string Decode(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var decoded = WebUtility.HtmlDecode(text);

            return decoded;
        }
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Determines whether the specified text is empty.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty(this string text) => text.Length == 0;

        /// <summary>
        ///     Determines whether the specified text is null.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this string text) => text == null;

        /// <summary>
        ///     Determines whether the specified text is null or empty. Functionally, it's the same as
        ///     <see cref="string.IsNullOrEmpty" /> but this looks better as it's an extension method.
        /// </summary>
        /// <param name="text">String.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string text) => string.IsNullOrEmpty(text);

        /// <summary>
        ///     Safely parses Integers.
        /// </summary>
        /// <returns>
        ///     <see cref="int" />
        /// </returns>
        /// <param name="value">String.</param>
        public static int SafeIntParse(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return int.MinValue;
            }

            if (int.TryParse(value, out var result))
            {
                return result;
            }

            return int.MinValue;
        }

        /// <summary>
        ///     Safely parses Long .
        /// </summary>
        /// <returns>
        ///     <see cref="long" />
        /// </returns>
        /// <param name="value">String.</param>
        public static long SafeLongParse(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return long.MinValue;
            }

            if (long.TryParse(value, out var result))
            {
                return result;
            }

            return long.MinValue;
        }

        /// <summary>
        ///     Safely parses boolean.
        /// </summary>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        /// <param name="value">String.</param>
        public static bool SafeBoolParse(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (bool.TryParse(value, out var result))
            {
                return result;
            }

            return false;
        }

        /// <summary>
        ///     Surrounds the strings with single quotes
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <returns>IEnumerable<string></returns>
        public static IEnumerable<string> Quote(this IEnumerable<string> strings)
        {
            if (strings == null)
            {
                yield break;
            }

            foreach (var s in strings)
            {
                yield return string.Format("'{0}'", s);
            }
        }

        public static string JoinWith(this IEnumerable<string> strings, char prefix)
        {
            if (strings.IsNullOrEmpty())
            {
                throw new ArgumentException();
            }

            var sb = new StringBuilder();
            foreach (var str in strings)
            {
                sb.Append(str).Append(prefix);
            }

            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        /// <summary>
        ///     Gets the bytes.
        /// </summary>
        /// <returns>The bytes.</returns>
        /// <param name="str">String.</param>
        public static byte[] GetBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string RemoveHtml(this string inputString)
        {
            try
            {
                // Will this simple expression replace all tags???
                var tagsExpression = new Regex(@"</?.+?>");
                return tagsExpression.Replace(inputString, " ");
            }
            catch
            {
                return inputString;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static string GetDateMeaning(this DateTime dateTime)
        {
            var now = DateTime.Now;
            var time = now.Subtract(dateTime);

            var seconds = (int)time.TotalSeconds;

            if (seconds < 60)
            {
                if (seconds > 0)
                {
                    return seconds.ToString() + " " + "sec.";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                var minutes = (int)time.TotalMinutes;

                if (minutes < 60)
                {
                    if (minutes > 0)
                    {
                        return minutes.ToString() + " " + "min.";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    var hours = (int)time.TotalHours;

                    if (hours < 24)
                    {
                        if (hours > 0)
                        {
                            return hours.ToString() + " " + "stunden";
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        var val = new StringBuilder();

                        var days = (int)time.TotalDays;
                        val.Append(days);
                        if (days == 1)
                        {                            
                            val.Append(" tag");
                        }

                        if(days > 1)
                        {
                            val.Append(" tage");
                        }
                        //else
                        //{
                        //    if (dateTime.Year == now.Year)
                        //    {
                        //        val.Append(dateTime.Day);
                        //        val.Append(" ");
                        //        val.Append(dateTime.ToString("MMMM"));
                        //    }
                        //    else
                        //    {
                        //        val.Append(dateTime.Day);
                        //        val.Append(" ");
                        //        val.Append(dateTime.ToString("MMMM"));
                        //        val.Append(" ");
                        //        val.Append(dateTime.Year);
                        //    }
                        //}

                        //val.Append(", ");
                        //val.Append(string.Format("{0:00}", dateTime.Hour));
                        //val.Append(":");
                        //val.Append(string.Format("{0:00}", dateTime.Minute));

                        return val.ToString();
                    }
                }
            }
        }

        public static string ToArrayToStringComma(this string[] array)
        {
            var result = "";
            foreach (var item in array)
            {
                result += "," + item;
            }

            if (result.Contains(","))
            {
                result = result.Remove(0, 1);
            }

            return result;
        }
    }
}
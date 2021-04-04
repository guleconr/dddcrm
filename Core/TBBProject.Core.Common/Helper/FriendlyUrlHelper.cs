using System;
using System.Collections.Generic;
using System.Text;

namespace TBBProject.Core.Common.Helper
{
    public static class FriendlyUrlHelper
    {
        public static string GetFriendlyTitle(string title, bool remapToAscii = true, int maxlength = 80)
        {
            if (title == null)
            {
                return string.Empty;
            }

            var length = title.Length;
            var prevdash = false;
            var stringBuilder = new StringBuilder(length);
            char c;

            for (var i = 0; i < length; ++i)
            {
                c = title[i];
                if (( c >= 'a' && c <= 'z' ) || ( c >= '0' && c <= '9' ))
                {
                    stringBuilder.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lower-case
                    stringBuilder.Append((char)( c | 32 ));
                    prevdash = false;
                }
                else if (( c == ' ' ) || ( c == ',' ) || ( c == '.' ) || ( c == '/' ) ||
                    ( c == '\\' ) || ( c == '-' ) || ( c == '_' ) || ( c == '=' ))
                {
                    if (!prevdash && ( stringBuilder.Length > 0 ))
                    {
                        stringBuilder.Append('-');
                        prevdash = true;
                    }
                }
                else if (c >= 128)
                {
                    var previousLength = stringBuilder.Length;

                    if (remapToAscii)
                    {
                        stringBuilder.Append(RemapInternationalCharToAscii(c));
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }

                    if (previousLength != stringBuilder.Length)
                    {
                        prevdash = false;
                    }
                }

                if (stringBuilder.Length >= maxlength)
                {
                    break;
                }
            }

            if (prevdash || stringBuilder.Length > maxlength)
            {
                return stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
            }
            else
            {
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Remaps the international character to their equivalent ASCII characters. See
        /// http://meta.stackexchange.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696.
        /// </summary>
        /// <param name="character">The character to remap to its ASCII equivalent.</param>
        /// <returns>The remapped character.</returns>
        public static string RemapInternationalCharToAscii(char character)
        {
            var s = character.ToString().ToLowerInvariant();
            if ("àåáâäãåąā".Contains(s))
            {
                return "a";
            }
            else if ("èéêěëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if ("ŕř".Contains(s))
            {
                return "r";
            }
            else if ("ĺľł".Contains(s))
            {
                return "l";
            }
            else if ("úů".Contains(s))
            {
                return "u";
            }
            else if ("đď".Contains(s))
            {
                return "d";
            }
            else if (character == 'ť')
            {
                return "t";
            }
            else if (character == 'ž')
            {
                return "z";
            }
            else if (character == 'ß')
            {
                return "ss";
            }
            else if (character == 'Þ')
            {
                return "th";
            }
            else if (character == 'ĥ')
            {
                return "h";
            }
            else if (character == 'ĵ')
            {
                return "j";
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public static class FriendlyUrlHelperExtensions
    {
        public static bool ContainsAccented(this string accentedStr, string contains)
        {
            return accentedStr.Contains(accentedStr.GetFriendlyTitle());
        }

        public static string GetFriendlyTitle(this string accentedStr)
        {
            return FriendlyUrlHelper.GetFriendlyTitle(accentedStr);
        }
    }
}

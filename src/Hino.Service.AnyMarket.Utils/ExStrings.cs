using NetSwissTools.Exceptions;
using NetSwissTools.Utils;
using System.Globalization;
using System.Text;

namespace Hino.Service.AnyMarket.Utils
{
    public static class ExStrings
    {
        public static string RemoveAllAccents(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static string ClearSpecialChars(this string text)
        {
            try
            {
                if (text.IsEmpty())
                {
                    return string.Empty;
                }

                string text2 = text.RemoveAllAccents();
                string[] oldChars = new string[64]
                {
                    "#39",
                    "---",
                    "--",
                    "-",
                    "'",
                    "#",
                    Environment.NewLine,
                    "\n",
                    "\r",
                    ",",
                    ".",
                    "?",
                    "&",
                    ":",
                    "/",
                    "!",
                    ";",
                    "'",
                    "\"",
                    "#",
                    "@",
                    "\u00a8",
                    "%",
                    "&",
                    "*",
                    "(",
                    ")",
                    "-",
                    "_",
                    "+",
                    "=",
                    "}",
                    "{",
                    "^",
                    "~",
                    "\\",
                    ".",
                    ",",
                    ">",
                    "<",
                    ";",
                    ":",
                    "/",
                    "?",
                    "°",
                    "£",
                    "¢",
                    "¬",
                    "³",
                    "²",
                    "¹",
                    "\u00b4",
                    "º",
                    "]",
                    "[",
                    "§",
                    "°",
                    "‘",
                    "’",
                    "”",
                    "“",
                    "+",
                    "ƒ",
                    "‡"
                };
                text2 = text2.ReplaceAny(oldChars, " ");
                return text2.Trim();
            }
            catch (Exception innerException)
            {
                throw new NetToolException("Error cleaning the string.", innerException);
            }
        }
    }
}

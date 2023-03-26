using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFormatterWinApp
{
    class Formatter
    {
        public static string AddQuote(string text)
        {
            var textLines = text.Split('\n');
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < textLines.Length; i++)
            {
                sb.Append("> ");
                sb.Append(textLines[i]);
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public static string AddQuoteRange(string text, Range targetIndexRange)
        {
            var textLines = text.Split('\n').ToList();
            if (textLines.Count < targetIndexRange.Start.Value + 1)
            {
                return text;
            }
            if (textLines.Count < targetIndexRange.End.Value + 1)
            {
                return text;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < targetIndexRange.Start.Value; i++)
            {
                sb.Append(textLines[i]);
                sb.Append("\n");
            }
            for (int i = targetIndexRange.Start.Value; i < targetIndexRange.End.Value + 1; i++)
            {
                sb.Append("> ");
                sb.Append(textLines[i]);
                sb.Append("\n");
            }
            for (int i = targetIndexRange.End.Value + 1; i < textLines.Count; i++)
            {
                sb.Append(textLines[i]);
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public static string RemoveLineBreakToEndOfLine(string text)
        {

            var regex = new Regex("(\\r\\n)+$");
            var results = regex.Matches(text);
            if (results.Count <= 0)
            {
                return text;
            }
            return text.Substring(0, results[0].Index);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFormatterWinApp
{
    internal class Detector
    {
        public static FormatMethod Detect(string text)
        {

            var regex = new Regex("\\n---------- Forwarded message ---------\\n");
            var results = regex.Matches(text);
            if (results.Count > 0)
            {
                return FormatMethod.Email;
            }
            return FormatMethod.Normal;
        }
    }
}

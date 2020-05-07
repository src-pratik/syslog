using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Basics.Syslog
{
    public static class Expressions
    {
        public static Regex InvalidCharacters = new Regex(@"[\x00-\x1F]", RegexOptions.Compiled);
        public static Regex FullFormat = new Regex(@"^<(\d{1,3})>([A-Za-z]{3} [ \d]\d \d\d:\d\d:\d\d) (.*$)", RegexOptions.Compiled);
        public static Regex PriFormat = new Regex(@"^<(\d{1,3})>(.*$)", RegexOptions.Compiled);
    }

    public static class Formats
    {
        public const string DateFomat1 = @"MMM d HH:mm:ss";
        public const string DateFormat2 = @"MMM  d HH:mm:ss";
        public static IFormatProvider StandarCulture = new CultureInfo("en-US");
    }

}

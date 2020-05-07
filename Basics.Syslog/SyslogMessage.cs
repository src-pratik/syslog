using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace Basics.Syslog
{
    //Reference https://tools.ietf.org/html/rfc5424
    public class SyslogMessage
    {
        public FacilityCode FacilityCode { get; set; } = FacilityCode.UserLevel;
        public SeverityCode SeverityCode { get; set; } = SeverityCode.Notice;
        public DateTime DateTime { get; set; } = DateTime.Now; //Defaulting to current date if unable to parse from message
        public string Hostname { get; set; }
        public string Message { get; set; }
        public string RawString { get; set; }

        public override string ToString()
        {
            return $"{FacilityCode} {SeverityCode} {DateTime.ToString(Formats.DateFomat1)} {Hostname} {Message}";
        }

        public SyslogMessage() { }

        public static SyslogMessage Parse(IPEndPoint endpoint, string rawString)
        {
            var cleanedString = Expressions.InvalidCharacters.Replace(rawString, " ");
            SyslogMessage syslogMessage;

            if (TryParseFullFormat(cleanedString, out syslogMessage) == false)
                if (TryParsePriFormat(cleanedString, out syslogMessage) == false)
                {
                    syslogMessage = new SyslogMessage();
                    syslogMessage.Message = cleanedString;
                }

            syslogMessage.RawString = rawString;
            syslogMessage.Hostname = endpoint.Address.ToString();

            return syslogMessage;
        }

        private static bool TryParseFullFormat(string messageString, out SyslogMessage syslogMessage)
        {
            syslogMessage = null;
            Match _match = Expressions.FullFormat.Match(messageString);

            if ((_match != null && _match.Success && _match.Groups.Count == 4) == false)
                return false;

            syslogMessage = new SyslogMessage();
            var code = int.Parse(_match.Groups[1].ToString());
            var datetime = _match.Groups[2].ToString();
            DateTime msgDateTime;

            if (!DateTime.TryParseExact(datetime, Formats.DateFomat1, Formats.StandarCulture, DateTimeStyles.None, out msgDateTime))
                DateTime.TryParseExact(datetime, Formats.DateFormat2, Formats.StandarCulture, DateTimeStyles.None, out msgDateTime);

            syslogMessage.FacilityCode = code.ToFacilityCode();
            syslogMessage.SeverityCode = code.ToSeverityCode();
            syslogMessage.DateTime = msgDateTime;
            syslogMessage.Message = _match.Groups[3].ToString();

            return true;
        }

        private static bool TryParsePriFormat(string messageString, out SyslogMessage syslogMessage)
        {
            syslogMessage = null;
            Match _match = Expressions.PriFormat.Match(messageString);

            if ((_match != null && _match.Success && _match.Groups.Count == 3) == false)
                return false;

            syslogMessage = new SyslogMessage();
            var code = int.Parse(_match.Groups[1].ToString());

            syslogMessage.FacilityCode = code.ToFacilityCode();
            syslogMessage.SeverityCode = code.ToSeverityCode();
            syslogMessage.Message = _match.Groups[2].ToString();

            return true;
        }

    }
}

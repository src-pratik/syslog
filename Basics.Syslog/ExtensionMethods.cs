using System;

namespace Basics.Syslog
{
    public static class ExtensionMethods
    {
        public static FacilityCode ToFacilityCode(this int code)
        {
            int _codeValue = code / 8;
            if (Enum.IsDefined(typeof(FacilityCode), _codeValue) == false)
                throw new ArgumentException($"Unknown {nameof(FacilityCode)} {code}.");

            return (FacilityCode)_codeValue;
        }

        public static SeverityCode ToSeverityCode(this int code)
        {
            int _codeValue = code % 8;
            if (Enum.IsDefined(typeof(SeverityCode), _codeValue) == false)
                throw new ArgumentException($"Unknown {nameof(SeverityCode)} {code}.");

            return (SeverityCode)_codeValue;
        }
    }
}

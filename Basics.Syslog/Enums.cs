namespace Basics.Syslog
{
    public enum FacilityCode
    {
        Kernel,
        UserLevel,
        MailSystem,
        SystemDaemon,
        Security1,
        SyslogdInternal,
        LinePrinter,
        NetworkNews,
        UUCP,
        Clock1,
        Security2,
        FTP,
        NTP,
        LogAudit,
        LogAlert,
        Clock2,
        Local0, Local1, Local2, Local3, Local4, Local5, Local6, Local7
    }

    public enum SeverityCode
    {
        Emergency,
        Alert, 
        Critical, 
        Error, 
        Warning,
        Notice, 
        Informational, 
        Debug
    }

}

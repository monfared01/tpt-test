namespace MainTest.Framework.Utility;

public interface IPersianDateTime
{
    string? ToPersianDateTime(DateTime? dateTime);
    DateTime? ToGregorianDateTime(string persianDateTime);
}

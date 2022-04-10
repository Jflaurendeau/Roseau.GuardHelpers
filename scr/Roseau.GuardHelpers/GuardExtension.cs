namespace Roseau.GuardHelpers;

public static class GuardExtension
{
    public static void IsLessThanOrEqualTo(DateOnly firstDate, string firstDateName, DateOnly secondDate, string secondDateName)
    {
        if (firstDate > secondDate)
            throw new ArgumentOutOfRangeException(firstDateName, $"The {firstDateName} ({firstDate}) must be a prior or equal date than the {secondDateName} ({secondDate}).");
    }
    public static void DatesAreInAscendingOrder(DateOnly[] dates, string datesName)
    {
        if (dates.Length < 2)
            return;
        for (int i = 1; i < dates.Length; i++)
        {
            IsLessThanOrEqualTo(dates[i - 1], $"Index {i - 1} of {datesName}", dates[i], $"Index {i} of {datesName}");
        }
    }
}

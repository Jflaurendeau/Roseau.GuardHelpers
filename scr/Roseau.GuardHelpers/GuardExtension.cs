using Microsoft.Toolkit.Diagnostics;
using System.Collections;
using System.Linq;

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
    public static void HaveSameLength(IEnumerable firstEnumerable, string firstEnumerableName, IEnumerable secondEnumerable, string secondEnumerableName)
    {
        if (firstEnumerable.Count(firstEnumerableName) != secondEnumerable.Count(secondEnumerableName))
            throw new ArgumentOutOfRangeException(firstEnumerableName, $"The {firstEnumerableName} and {secondEnumerableName} must have the same length or count.");
    }
    private static int Count(this IEnumerable enumerable, string enumerableName)
    {
        Guard.IsNotNull(enumerable, nameof(enumerable));
        if (enumerable is ICollection collection)
            return collection.Count;
        
        int count = 0;
        var iEnumerator = enumerable.GetEnumerator();
        CustomUsingForAgurmentException(iEnumerator, () =>
        {
            checked
            {
                while (iEnumerator.MoveNext())
                    count++;
            }
        }, enumerableName);
        return count;
    }
    private static void CustomUsingForAgurmentException(object obj, Action action, string variableName)
    {
        try
        {
            action();
        }
        catch (OverflowException)
        {
            throw new ArgumentOutOfRangeException(variableName, $"The {variableName} has a number of item that exceeds the range of {typeof(int)} type.");
        }
        finally
        {
            if (obj is IDisposable disposable)
                disposable.Dispose();
        }
    }

    public class CustomUsingForAgurment : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class Blabla : IEnumerator
    {
        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}

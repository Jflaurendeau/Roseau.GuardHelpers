using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Roseau.GuardHelpers.UnitTests
{
    [TestClass]
    public class GuardExtensionTests
    {
        [TestMethod]
        public void IsLessThanOrEqualTo_BothDatesAreEquals_NoThrow()
        {
            // Arrange
            DateOnly firstDate = new(2020, 1, 1);

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(
                () => GuardExtension.IsLessThanOrEqualTo(firstDate, nameof(firstDate), firstDate, nameof(firstDate)));
        }
        [TestMethod]
        public void IsLessThanOrEqualTo_FirstDateBeforeSecondDate_NoThrow()
        {
            // Arrange
            DateOnly firstDate = new(2020, 1, 1);
            DateOnly secondDate = firstDate.AddDays(1);

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(
                () => GuardExtension.IsLessThanOrEqualTo(firstDate, nameof(firstDate), secondDate, nameof(secondDate)));
        }
        [TestMethod]
        public void IsLessThanOrEqualTo_FirstDateLaterThanSecondDate_Throws()
        {
            // Arrange
            DateOnly firstDate = new(2020, 1, 1);
            DateOnly secondDate = firstDate.AddDays(-1);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => GuardExtension.IsLessThanOrEqualTo(firstDate, nameof(firstDate), secondDate, nameof(secondDate)));
        }
        [TestMethod]
        public void DatesAreInAscendingOrder_EveryDaysAreInChronologicSequence_NoThrow()
        {
            // Arrange
            DateOnly[] dates = new DateOnly[]
            {
                new DateOnly(2020, 1, 1),
                new DateOnly(2020, 1, 2),
                new DateOnly(2020, 1, 3),
                new DateOnly(2020, 1, 4)
            };

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(
                () => GuardExtension.DatesAreInAscendingOrder(dates, nameof(dates)));
        }
        [TestMethod]
        public void DatesAreInAscendingOrder_OneDateOnly_NoThrow()
        {
            // Arrange
            DateOnly[] dates = new DateOnly[]
            {
                new DateOnly(2020, 1, 1),
            };

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(
                () => GuardExtension.DatesAreInAscendingOrder(dates, nameof(dates)));
        }
        [TestMethod]
        public void DatesAreInAscendingOrder_EmptyArray_NoThrow()
        {
            // Arrange
            DateOnly[] dates = new DateOnly[2];

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(
                () => GuardExtension.DatesAreInAscendingOrder(dates, nameof(dates)));
        }
        [TestMethod]
        public void DatesAreInAscendingOrder_AtLeastOneDateIsNotInChronological_Throws()
        {
            // Arrange
            DateOnly[] dates = new DateOnly[]
            {
                new DateOnly(2020, 1, 1),
                new DateOnly(2020, 1, 2),
                new DateOnly(2020, 1, 4),
                new DateOnly(2020, 1, 3)
            };

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => GuardExtension.DatesAreInAscendingOrder(dates, nameof(dates)));
        }
    }

    public static class AssertionExtension
    {
        public static void DoesNotThrow<T>(this Assert _, Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (T)
            {
                Assert.Fail($"The {typeof(T).Name} was not supposed to be thrown here.");
            }
        }
    }
}
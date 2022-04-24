using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Linq;

namespace Roseau.GuardHelpers.UnitTests
{
    [TestClass]
    public class GuardExtensionTests
    {
        [TestMethod]
        [TestCategory("IsLessThanOrEqualTo")]
        public void IsLessThanOrEqualTo_BothDatesAreEquals_NoThrow()
        {
            // Arrange
            DateOnly firstDate = new(2020, 1, 1);

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(
                () => GuardExtension.IsLessThanOrEqualTo(firstDate, nameof(firstDate), firstDate, nameof(firstDate)));
        }
        [TestMethod]
        [TestCategory("IsLessThanOrEqualTo")]
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
        [TestCategory("IsLessThanOrEqualTo")]
        public void IsLessThanOrEqualTo_FirstDateLaterThanSecondDate_Throws()
        {
            // Arrange
            DateOnly firstDate = new(2020, 1, 1);
            DateOnly secondDate = firstDate.AddDays(-1);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => GuardExtension.IsLessThanOrEqualTo(firstDate, nameof(firstDate), secondDate, nameof(secondDate)));
        }
        [TestMethod]
        [TestCategory("DatesAreInAscendingOrder")]
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
        [TestCategory("DatesAreInAscendingOrder")]
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
        [TestCategory("DatesAreInAscendingOrder")]
        public void DatesAreInAscendingOrder_EmptyArray_NoThrow()
        {
            // Arrange
            DateOnly[] dates = new DateOnly[2];

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(
                () => GuardExtension.DatesAreInAscendingOrder(dates, nameof(dates)));
        }
        [TestMethod]
        [TestCategory("DatesAreInAscendingOrder")]
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
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_FirstArrayIsNull_Throws()
        {
            // Arrange
            FibonacciFinitEnumerator[]? firstArray = null;
            IEnumerable? secondArray = new FibonacciFinitEnumerator[]
            {
                new FibonacciFinitEnumerator(),
                new FibonacciFinitEnumerator(),
                new FibonacciFinitEnumerator(),
                new FibonacciFinitEnumerator()
            };

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => GuardExtension.HaveSameLength(firstArray!, nameof(firstArray), secondArray!, nameof(secondArray)));
        }
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_SecondArrayIsNull_Throws()
        {
            // Arrange
            FibonacciFinitEnumerator[] firstArray = new FibonacciFinitEnumerator[]
            {
                new FibonacciFinitEnumerator(),
                new FibonacciFinitEnumerator(),
                new FibonacciFinitEnumerator(),
                new FibonacciFinitEnumerator()
            };
            IEnumerable? secondArray = null;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => GuardExtension.HaveSameLength(firstArray, nameof(firstArray), secondArray!, nameof(secondArray)));
        }
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_BothArrayAreNull_Throws()
        {
            // Arrange
            IEnumerable? firstArray = null;
            IEnumerable? secondArray = null;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => GuardExtension.HaveSameLength(firstArray!, nameof(firstArray), secondArray!, nameof(secondArray)));
        }
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_BothArrayOneIsICollectionTheSecondIsIEnumerable_AreOfDifferentLength_Throws()
        {
            // Arrange
            FibonacciInfinitEnumerator[] firstArray = new FibonacciInfinitEnumerator[]
            {
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
            };
            IEnumerable secondArray = firstArray.Take(2);

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => GuardExtension.HaveSameLength(firstArray!, nameof(firstArray), secondArray!, nameof(secondArray)));
        }
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_BothArrayBothAreICollection_AreOfDifferentLength_Throws()
        {
            // Arrange
            FibonacciInfinitEnumerator[] firstArray = new FibonacciInfinitEnumerator[]
            {
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
            };
            FibonacciInfinitEnumerator[] secondArray = new FibonacciInfinitEnumerator[]
            {
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
            };

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => GuardExtension.HaveSameLength(firstArray!, nameof(firstArray), secondArray!, nameof(secondArray)));
        }
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_BothArraysAreIEnumerable_SecondArrayHasInfinitLength_Throws()
        {
            // Arrange
            IEnumerable firstArray = new FibonacciFinitEnumerable();
            IEnumerable secondArray = new FibonacciInfinitEnumerable();

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => GuardExtension.HaveSameLength(firstArray!, nameof(firstArray), secondArray!, nameof(secondArray)));
        }
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_BothArraysAreIEnumerable_AreOfSameLength_DoesNotThrows()
        {
            // Arrange
            IEnumerable firstArray = new FibonacciFinitEnumerable();
            IEnumerable secondArray = new FibonacciFinitEnumerable();

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(() => GuardExtension.HaveSameLength(firstArray!, nameof(firstArray), secondArray!, nameof(secondArray)));
        }
        [TestMethod]
        [TestCategory("HaveSameLength")]
        public void HaveSameLength_BothArraysAreICollection_AreOfSameLength_DoesNotThrows()
        {
            // Arrange
            FibonacciInfinitEnumerator[] firstArray = new FibonacciInfinitEnumerator[]
            {
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
                new FibonacciInfinitEnumerator(),
            };
            var secondArray = firstArray;

            // Act and Assert
            Assert.That.DoesNotThrow<ArgumentOutOfRangeException>(() => GuardExtension.HaveSameLength(firstArray!, nameof(firstArray), secondArray!, nameof(secondArray)));
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
    public class FibonacciInfinitEnumerable : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new FibonacciInfinitEnumerator();
        }
    }
    public class FibonacciInfinitEnumerator : IEnumerator
    {
        int past2 = 0;
        int past1 = 0;
        int current = 0;
        public object Current => current;

        public bool MoveNext()
        {
            current = past1 + past2;
            if (current == 0) current = 1;
            past2 = past1;
            past1 = current;
            return true;
        }

        public void Reset()
        {
            current = 0;
            past1 = 0;
            past2 = 0;
        }
    }
    public class FibonacciFinitEnumerable : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new FibonacciFinitEnumerator();
        }
    }
    public class FibonacciFinitEnumerator : IEnumerator
    {
        public int past2 = 0;
        public int past1 = 0;
        public int current = 0;
        public object Current => current;

        public bool MoveNext()
        {
            current = past1 + past2;
            if (current == 0) current = 1;
            past2 = past1;
            past1 = current;
            if (current >= 122) 
                return false;
            return true;
        }

        public void Reset()
        {
            current = 0;
            past1 = 0;
            past2 = 0;
        }
    }
}
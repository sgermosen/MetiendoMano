using NUnit.Framework;
using System;
using ExpensesPredictor.Mobile.Models;

namespace ExpensesPredictor.Mobile.UnitTests
{
    [TestFixture]
    public class ExpensesTest
    {
        [Test]
        public void Frecuency_maximum_accepts_30()
        {
            //Arrange
            var numtimes = 31;
            
            //Assert
            Assert.Catch<ArgumentException>(() =>
            {
                //Act
                var frecuency = ExpenseFrecuency.TimesMontly(numtimes);
            });
        }

        [Test]
        public void Frecuency_minimun_accepts_1()
        {
            //Arrange
            var numtimes = 0;

            //Assert
            Assert.Catch<ArgumentException>(() =>
            {
                //Act
                var frecuency = ExpenseFrecuency.TimesMontly(numtimes);
            });
        }
        [Test]
        public void Frecuency_Weekly_means_4_times_in_a_month()
        {
            //Arrange
            var numtimes = 4;
            var frecuency = ExpenseFrecuency.Weekly;
            //Act
            var result = frecuency.TimesPerMonth;

            //Assert
            Assert.AreEqual(numtimes,result);
        }

        [Test]
        public void Frecuency_daily_means_30_times_in_a_month()
        {
            //Arrange
            var numtimes = 30;
            var frecuency = ExpenseFrecuency.Daily;
            //Act
            var result = frecuency.TimesPerMonth;

            //Assert
            Assert.AreEqual(numtimes, result);
        }

        [Test]
        public void Frecuency_monthly_means_1_times_in_a_month()
        {
            //Arrange
            var numtimes = 1;
            var frecuency = ExpenseFrecuency.Monthly;
            //Act
            var result = frecuency.TimesPerMonth;

            //Assert
            Assert.AreEqual(numtimes, result);
        }

        [Test]
        public void IsValid_returns_False_When_Title_isnot_Entered()
        {
            //Arrange
            var expense = new Expense
            {
                Title = String.Empty
            };
            var expected = false;
            
            //Act
            var result = expense.IsValid;

            //Assert
            Assert.AreEqual(expected,result,"IsValid should be False");
        }

        [Test]
        public void IsValid_returns_True_When_Title_is_Entered()
        {
            //Arrange
            var expense = new Expense
            {
                Title = "demo",
                Amount = 1
            };
            var expected = true;

            //Act
            var result = expense.IsValid;

            //Assert
            Assert.AreEqual(expected, result, "IsValid should be True");
        }

        [Test]
        public void IsValid_returns_False_When_NoAmount_is_Entered()
        {
            //Arrange
            var expense = new Expense
            {
                Title = "Demo",
                Amount = 0
            };
            var expected = false;

            //Act
            var result = expense.IsValid;

            //Assert
            Assert.AreEqual(expected, result, "IsValid should be False");
        }
    }
}

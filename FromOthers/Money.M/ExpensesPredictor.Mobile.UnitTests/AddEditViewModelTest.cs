using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesPredictor.Mobile.Models;
using ExpensesPredictor.Mobile.ViewModels;
using NUnit.Framework;

namespace ExpensesPredictor.Mobile.UnitTests
{
    [TestFixture]
    public class AddEditViewModelTest
    {
        [Test]
        public void Register_IsDisabled_When_Expense_is_not_Valid()
        {
            //Arrange
            var exp = new Expense();
            var vm = new AddEditViewModel(exp);
            var expected = false;
            
            //Act
            var result = vm.RegisterCommand.CanExecute(exp);
            
            //Assert
            Assert.AreEqual(expected,result);
        }

        [Test]
        public void Register_IsEnable_When_Expense_IsValid()
        {
            //Arrange
            var exp = new Expense()
            {
                Title = "Demo",
                Amount = 1,
                Frecuency = ExpenseFrecuency.Monthly
            };
            var vm = new AddEditViewModel(exp);
            var expected = true;

            //Act
            var result = vm.RegisterCommand.CanExecute(exp);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}

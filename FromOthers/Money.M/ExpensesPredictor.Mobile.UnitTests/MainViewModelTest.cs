using System.Collections.Generic;
using ExpensesPredictor.Mobile.Infrastructure.NoSQL;
using ExpensesPredictor.Mobile.Models;
using ExpensesPredictor.Mobile.ViewModels;
using FakeItEasy;
using NUnit.Framework;

namespace ExpensesPredictor.Mobile.UnitTests
{
    [TestFixture]
    public class MainViewModelTest
    {
        [Test]
        public void MainViewModel_show_expenses_list()
        {
            //Arrange
            var repo = A.Fake<IExpensesRepository>();
            A.CallTo(() => repo.FindAll())
                .ReturnsLazily(x=> new List<Expense>()
                {
                    new Expense(),new Expense(),new Expense(),new Expense()
                });

            var vm = new MainViewModel(repo);
            var total = 4;

            //Act
            vm.OnPushed();
            var count = vm.Expenses.Count;

            //Assert   
            Assert.AreEqual(total, count);
        }
    }
}

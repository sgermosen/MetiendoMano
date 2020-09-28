using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ExpensesPredictor.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void SaveButton_should_be_disabled_when_expense_is_empty()
        {
            app.WaitForElement(c=>c.Marked("NoResourceEntry-0"));
            
            //Tap AddExpense
            app.Tap(c => c.Marked("NoResourceEntry-0"));
            
            //Wait for navigation
            app.WaitForElement(c => c.Marked("BtnSave"));

            //Check if Save button is enabled
            var isSaveEnabled = app.Query(c => c.Marked("BtnSave")).First().Enabled;
            app.Screenshot("Save button disabled.");
            Assert.False(isSaveEnabled,"Save button should be disabled.");
        }

        [Test]
        public void AddExpense_refresh_list_and_total()
        {
            //Add Expense
            app.Tap(c => c.Marked("NoResourceEntry-0"));
            //Wait for navigation
            app.WaitForElement(c => c.Marked("BtnSave"));
            //Check if is required
            var requiredFields = app.Query(c => c.Text("This field is required.")).Count();
            Assert.AreEqual(requiredFields,2);
            //Enter title
            app.EnterText(c => c.Marked("TxtTitle"), "Expense1");
            //Check required fields
            requiredFields = app.Query(c => c.Text("This field is required.")).Count();
            Assert.AreEqual(1,requiredFields);
            //Enter description
            app.EnterText(c => c.Marked("TxtDescription"), "Description for Expense 1");
            //Enter amount
            app.EnterText(c => c.Marked("TxtAmount"), "99");
            //Select frecuency
            app.Tap(c => c.Marked("PckFrecuencies"));
            app.WaitForElement(c => c.Marked("select_dialog_listview"));
            app.Tap(c => c.Marked("select_dialog_listview"));
            //Save Expense
            app.Screenshot("New Expense Form");
            app.Tap(c => c.Marked("BtnSave"));

            //Wait for navigation
            app.WaitForElement(c => c.Marked("LblTotal"));

            //Validate refresh
            app.Screenshot("List and Total calculated");
            var expected = "Total estimated for this month: $396.00";
            var total= app.Query(c => c.Marked("LblTotal")).First().Text;
            Assert.AreEqual(expected,total,"MainPage should refresh list and total");
        }

        [Test]
        public void RemoveExpense_refresh_list_and_total()
        {
            //Register a expense
            app.Tap(c => c.Marked("NoResourceEntry-0"));
            app.WaitForElement(c => c.Marked("BtnSave"));

            app.EnterText(c => c.Marked("TxtTitle"), "Title");
            app.EnterText(c => c.Marked("TxtAmount"), "99");
            app.Tap(c => c.Marked("PckFrecuencies"));
            app.Tap(c => c.Marked("select_dialog_listview").Child(2));
            app.Tap(c => c.Marked("BtnSave"));
            app.WaitForElement(c => c.Marked("LstExpenses"));

            //Delete
            app.TouchAndHold(c => c.Marked("LstExpenses").Child());
            app.Screenshot("Showing context menu");
            app.Tap(c => c.Marked("NoResourceEntry-1").Index(1));
            app.Screenshot("Confirm dialog");
            app.Tap(c => c.Marked("button1"));

            //Assert
            app.Screenshot("No elements");
            var totalText = app.Query(c => c.Marked("LblTotal")).First().Text;
            Assert.True(totalText.EndsWith("$0.00"));
            var totalList = app.Query(c => c.Marked("LstExpenses").Descendant()).Count();
            Assert.AreEqual(totalList,0);
        }

    }
}


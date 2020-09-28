using System;
using System.Collections.Generic;
using ExpensesPredictor.Mobile.Infrastructure.Commands;
using ExpensesPredictor.Mobile.Infrastructure.NoSQL;
using ExpensesPredictor.Mobile.Models;
using ExpensesPredictor.Mobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.ViewModels
{
    public class AddEditViewModel : ViewModelBase
    {
        private readonly Expense _expense;
        private readonly IExpensesRepository _expensesRepository;
        private ExpenseOption _frecuency;

        protected AddEditViewModel()
        {
            _expensesRepository = new ExpensesRepository();
            RegisterCommand = new RelayCommand<Expense>(async (exp) =>
            {
                if (!exp.IsValid)return;

                if (exp.Id == Guid.Empty)
                {
                    exp.Id = Guid.NewGuid();
                    _expensesRepository.Add(exp);

                }
                else
                {
                    _expensesRepository.Update(exp);
                }
                await Application.Current.MainPage.Navigation.PopAsync(animated: true);
            }, (exp) => exp != null && exp.IsValid);
        }

        public AddEditViewModel(Expense expense):this()
        {
            if (expense == null) throw new ArgumentNullException(nameof(expense));
            _expense = expense;

            _expense.PropertyChanged += _expense_PropertyChanged;

            if(expense.Frecuency!=null)
            _frecuency = Frecuencies.Find(x => x.Frecuency.TimesPerMonth == _expense.Frecuency.TimesPerMonth);
        }

        private void _expense_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsValid":
                    RegisterCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        public RelayCommand<Expense> RegisterCommand { get; }

        public Expense Expense
        {
            get { return _expense; }
        }

        public ExpenseOption Frecuency
        {
            get { return _frecuency; }
            set
            {
                SetProperty(ref _frecuency,value);
                Expense.Frecuency = value.Frecuency;
            }
        }

        public string Title => Expense ==null || Expense.Id == Guid.Empty ? "New Expense" : "Editing Expense";

        public List<ExpenseOption> Frecuencies { get; } = ExpenseFrecuencies.Get();
    }
}

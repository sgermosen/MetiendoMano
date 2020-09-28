using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ExpensesPredictor.Mobile.Infrastructure.NoSQL;
using ExpensesPredictor.Mobile.Models;
using ExpensesPredictor.Mobile.ViewModels.Abstract;
using ExpensesPredictor.Mobile.Views;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private readonly IExpensesRepository _expensesRepository;
        private ObservableCollection<Expense> _expenses = new ObservableCollection<Expense>();
        private double _totalEstimatedForThisMonth;

        public MainViewModel(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        public MainViewModel() : this(new ExpensesRepository())
        {
            DeleteExpenseCommand = new Command<Expense>(async (exp) =>
            {
                var accepte = await
                    Application.Current.MainPage.DisplayAlert($"Delete Expense:{exp.Title}", "Are you sure?",
                        "Yes,delete it", "Cancel");
                if (!accepte) return;

                _expensesRepository.Delete(exp);

                OnPushed();
            });
        }

        public override void OnPushed()
        {
            IsLoading = true;
            var exps = _expensesRepository.FindAll();
            this.Expenses = new ObservableCollection<Expense>(exps);
            CalculateTotal();
            IsLoading = false;
        }

        public ObservableCollection<Expense> Expenses
        {
            get { return _expenses; }
            set
            {
                SetProperty(ref _expenses, value);
                OnPropertyChanged("AnyExpense");
            }
        }
        public bool AnyExpense => Expenses.Any();

        public ICommand AddExpenseCommand { get; } = new Command(async() =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddEditView());
        });

        public ICommand EditExpenseCommand { get; } = new Command<Expense>(async (exp) =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddEditView(exp));
        });

        public ICommand DeleteExpenseCommand { get; }

        public double TotalEstimatedForThisMonth
        {
            get { return _totalEstimatedForThisMonth; }
            set { SetProperty(ref _totalEstimatedForThisMonth,value); }
        }

        protected void CalculateTotal()
        {
            TotalEstimatedForThisMonth = Expense.CalculateTotalEstimatedForThisMonth(this.Expenses);
        }
    }
}

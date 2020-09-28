using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesPredictor.Mobile.Infrastructure.NoSQL.Abstract;
using ExpensesPredictor.Mobile.Models;

namespace ExpensesPredictor.Mobile.Infrastructure.NoSQL
{
    public class ExpensesRepository: RepositoryBase<Expense,Guid>, IExpensesRepository
    {
        public override string Name => "Expenses";
        protected override Func<Expense, Guid> KeyPredicate => x=> x.Id;
    }
}

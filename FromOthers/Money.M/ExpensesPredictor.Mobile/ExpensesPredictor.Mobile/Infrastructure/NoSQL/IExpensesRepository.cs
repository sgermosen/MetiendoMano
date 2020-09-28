using System;
using ExpensesPredictor.Mobile.Infrastructure.NoSQL.Abstract;
using ExpensesPredictor.Mobile.Models;

namespace ExpensesPredictor.Mobile.Infrastructure.NoSQL
{
    public interface IExpensesRepository: IRepository<Expense,Guid>
    {
    }
}

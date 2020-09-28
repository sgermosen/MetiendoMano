using System;
using System.Collections.Generic;

namespace ExpensesPredictor.Mobile.Infrastructure.NoSQL.Abstract
{
    public interface IRepository<T, TKey>
    {
        List<T> GetAll();
        List<T> FindAll();
        List<T> FindAll(Func<T, bool> predicate);
        T FindByKey(TKey key);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
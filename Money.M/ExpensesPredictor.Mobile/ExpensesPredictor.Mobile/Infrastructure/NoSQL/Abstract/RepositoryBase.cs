using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarcelloDB.Collections;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.Infrastructure.NoSQL.Abstract
{
    public abstract class RepositoryBase<T, TKey> : IRepository<T, TKey>
    {
        private readonly MarcelloDB.Session _session;
        private readonly Collection<T, TKey> _collection;

        protected RepositoryBase(IPlatformFactory platformFactory)
        {
            _session = platformFactory.GetSession();
            _collection = GetCollection();
        }

        protected RepositoryBase()
        {
            _session = DependencyService.Get<IPlatformFactory>().GetSession();
            _collection = GetCollection();
        }

        public abstract string Name { get; }
        protected abstract Func<T, TKey> KeyPredicate { get; }
        protected CollectionFile GetFileCollection()
        {
            var personsFile = _session[$"{Name}.dat"];
            return personsFile;
        }

        protected Collection<T, TKey> GetCollection()
        {
            var collection = GetFileCollection().Collection<T, TKey>(Name, KeyPredicate);
            return collection;
        }

        public List<T> GetAll()
        {
            return _collection.All.ToList();
        }

        public List<T> FindAll()
        {
            return _collection.All.ToList();
        }

        public List<T> FindAll(Func<T, bool> predicate)
        {
            return _collection.All.Where(predicate).ToList();
        }

        public T FindByKey(TKey key)
        {
            return _collection.Find(key);
        }

        public virtual void Add(T entity)
        {
             _collection.Persist(entity);
            Task.Delay(50).Wait();
        }

        public virtual void Update(T entity)
        {
            _collection.Persist(entity);
        }

        public virtual void Delete(T entity)
        {
           _collection.Destroy(KeyPredicate(entity));
        }
    }
}

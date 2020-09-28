using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public interface IRepository<T>
    {
        #region IRepository<T> Members
        /// <summary>
        /// Retorna un objeto del tipo AsQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AsQueryable();

        /// <summary>
        /// Retorna un objeto del tipo AsQueryable y acepta como parámetro las relaciones a incluir
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna un objeto del tipo AsQueryable bajo una condición que especifiques como parámetro
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna una entidad bajo una condición especificada
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna una entidad bajo una condición especificada o null sino encontrara registros
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T SingleOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna la primera entidad encontrada bajo una condición especificada
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna la primera entidad encontrada bajo una condición especificada o null sino encontrara registros
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Registra una entidad
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Registra varias entidades
        /// </summary>
        /// <param name="entity"></param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Actualiza varias entidades
        /// </summary>
        /// <param name="entity"></param>
        void Update(IEnumerable<T> entities);
        #endregion

        #region SQL Queries
        /// <summary>
        /// Ejecuta un query personalizado
        /// </summary>
        /// <param name="entity"></param>
        IQueryable<T> SelectQuery(string query, params object[] parameters);
        int ExecuteSqlCommand(string query, params object[] parameters);

        /// <summary>
        /// Ejecuta un query personalizado y espera un retorno a cambio
        /// </summary>
        /// <param name="entity"></param>
        IQueryable<I> ExecuteSqlCommand<I>(string query, params object[] parameters) where I : class;
        #endregion
    }
}

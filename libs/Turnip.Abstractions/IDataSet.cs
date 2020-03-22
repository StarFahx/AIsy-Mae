using System;

namespace Turnip
{
    public interface IDataSet<T> : IDisposable
    {
        void Add(T item);
        void DeleteWhere(Func<T, bool> predicate);
        void Update(T item);
        T[] GetAll();
    }
}
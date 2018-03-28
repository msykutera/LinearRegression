using System.Collections.Generic;

namespace Syku.LinearRegression
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        void Save(T item);
    }
}

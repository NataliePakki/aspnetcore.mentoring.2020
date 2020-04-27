using System.Collections.Generic;

namespace Shop.Core.Services
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll(bool includeAll = false);

        T Get(int id, bool includeAll = false);

        T Create(T data);

        T Update(T data);
    }
}

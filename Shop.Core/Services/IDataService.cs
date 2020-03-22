using System.Collections.Generic;

namespace Shop.Core.Services
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        T Create(T data);

        T Update(T data);
    }
}

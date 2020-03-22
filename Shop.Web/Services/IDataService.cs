using System.Collections.Generic;

namespace AspNetCoreMentoring.Services
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        T Create(T data);

        T Update(T data);
    }
}

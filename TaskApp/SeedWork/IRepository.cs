using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApp.SeedWork
{
    public interface IRepository<T>
    {
        T Find(string Id);
        List<T> List();
        void Add(T model);
        void Delete(string Id);
        void Update(T model);
        void Save();

        bool SaveResult();
    }
}

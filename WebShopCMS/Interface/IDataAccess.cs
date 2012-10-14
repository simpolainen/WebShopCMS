using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCMS.Interface
{
    interface IDataAccess<T>
    {
        void Add(T instance);
        void AddRange(ICollection<T> instances);
        ICollection<T> GetAll();
        ICollection<T> GetLimitedResult(int skip, int take);
        ICollection<T> GetByKeys(string[] keys);
        T GetById(object id);
        void Delete(T instance);
        void Update(T instance);
        void SubmitChanges();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); // generic function

        object Get(string key);

        void Add(string key, object value, int duration);

        bool IsAdd(string key); // cache de var mı?

        void Remove(string key); // cache'den çıkarır.

        void RemoveByPattern(string pattern); // fonsiyon isminde pattern bulunanları çıkarır. 

    }
}

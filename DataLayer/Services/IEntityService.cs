using Products.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Products.Services
{
    public interface IEntityService<T> where T: IMongoEntity
    {
        Task<T> GetByName(string Id);
        void Create(T entity);
        void Update(T entity);
        void Delete(string Id);

    }
}
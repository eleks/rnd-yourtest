using System;
using System.Linq;
using YourTest.REST.Models;

namespace YourTest.REST.Repository
{
    public class Repository<TModel> : IRepository<TModel>
        where TModel : ModelBase
    {
        public Repository()
        {
        }

        public IQueryable<TModel> Query()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Linq;
using YourTest.REST.Models;
using System.Collections.Generic;

namespace YourTest.REST.Repository
{
    public class InMemoryRepository<TModel> : IRepository<TModel>
        where TModel : ModelBase
    {
        public InMemoryRepository()
        {
            _data = new Dictionary<Int32, TModel>();
        }

        public IQueryable<TModel> Query() => _data.Values.AsQueryable();

        public void Insert(TModel model) => _data[model.Id] = model;

        private readonly Dictionary<Int32, TModel> _data;
    }
}

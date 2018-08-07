using System;
using System.Linq;
using YourTest.REST.Models;
using System.Collections.Generic;
using Force.DeepCloner;

namespace YourTest.REST.Repository
{
    public class InMemoryRepository<TModel> : IRepository<TModel>
        where TModel : ModelBase
    {
        public InMemoryRepository()
        {
            _data = new Dictionary<Int32, TModel>();
        }

        public IQueryable<TModel> Query() => _data.Values.Select(t => t.DeepClone()).AsQueryable();

        public void Insert(TModel model) => _data[model.Id] = model.DeepClone();

        private readonly Dictionary<Int32, TModel> _data;
    }
}

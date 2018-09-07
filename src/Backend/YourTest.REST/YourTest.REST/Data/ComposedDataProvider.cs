using System;
using YourTest.REST.Repository;
using YourTest.REST.Models;

namespace YourTest.REST.Data
{
    public class ComposedDataProvider<TModel> : IDataProvider<TModel>
        where TModel : ModelBase
    {
        private readonly IDataProvider<TModel>[] _dataProviders;

        public ComposedDataProvider(params IDataProvider<TModel>[] dataProviders) => _dataProviders = dataProviders;

        public void Seed(IRepository<TModel> repository)
        {
            foreach (var p in _dataProviders)
            {
                p.Seed(repository);
            }
        }
    }
}

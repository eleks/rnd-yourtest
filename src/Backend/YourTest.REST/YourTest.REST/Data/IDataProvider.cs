using System;
using YourTest.REST.Repository;
using YourTest.REST.Models;
namespace YourTest.REST.Data
{
    public interface IDataProvider<TModel>
        where TModel : ModelBase
    {
        void Seed(IRepository<TModel> repository);
    }
}

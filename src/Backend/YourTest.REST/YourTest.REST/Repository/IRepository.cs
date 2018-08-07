using System;
using YourTest.REST.Models;
using System.Linq;
namespace YourTest.REST.Repository
{
    public interface IRepository<TModel>
        where TModel : ModelBase
    {
        IQueryable<TModel> Query();
    }
}

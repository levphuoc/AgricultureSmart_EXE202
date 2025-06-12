using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface INewsCategoryRepository
    {
        IQueryable<NewsCategory> GetAll();
        Task<NewsCategory> GetByIdAsync(int id);
        Task AddAsync(NewsCategory entity);
        void Update(NewsCategory entity);
        void Delete(NewsCategory entity);
        Task SaveChangesAsync();
    }
}

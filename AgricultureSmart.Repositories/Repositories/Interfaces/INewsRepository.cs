using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface INewsRepository
    {
        IQueryable<News> GetAll();
        Task<News> GetByIdAsync(int id);
        Task AddAsync(News news);
        void Update(News news);
        void Delete(News news);
        Task SaveChangesAsync();
    }
}

using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class NewsCategoryRepository : INewsCategoryRepository
    {
        private readonly AgricultureSmartDbContext _context;

        public NewsCategoryRepository(AgricultureSmartDbContext context)
        {
            _context = context;
        }

        public IQueryable<NewsCategory> GetAll()
        {
            return _context.NewsCategories.AsQueryable();
        }

        public async Task<NewsCategory> GetByIdAsync(int id)
        {
            return await _context.NewsCategories.FindAsync(id);
        }

        public async Task AddAsync(NewsCategory entity)
        {
            await _context.NewsCategories.AddAsync(entity);
        }

        public void Update(NewsCategory entity)
        {
            _context.NewsCategories.Update(entity);
        }

        public void Delete(NewsCategory entity)
        {
            _context.NewsCategories.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AgricultureSmartDbContext _context;

        public NewsRepository(AgricultureSmartDbContext context)
        {
            _context = context;
        }

        public IQueryable<News> GetAll()
        {
            return _context.News.Include(n => n.Category).AsQueryable();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News.Include(n => n.Category).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task AddAsync(News news)
        {
            await _context.News.AddAsync(news);
        }

        public void Update(News news)
        {
            _context.News.Update(news);
        }

        public void Delete(News news)
        {
            _context.News.Remove(news);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

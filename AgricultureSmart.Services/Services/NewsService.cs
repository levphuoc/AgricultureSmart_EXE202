using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.NewModels;
using AgricultureSmart.Services.Models.NewModels.AgricultureSmart.Services.Models.NewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repo;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewGetAllDto>> GetAllAsync(int page, int pageSize)
        {
            var query = _repo.GetAll().OrderByDescending(n => n.PublishedAt);
            var paged = query.Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<IEnumerable<NewGetAllDto>>(paged);
        }

        public async Task<NewsListResponse> SearchAsync(string? title, string? author,
                                                int? categoryId, int page, int pageSize)
        {
            var query = _repo.GetAll();  // IQueryable<News>

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(n => n.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(n => n.Author.Contains(author));

            if (categoryId.HasValue)
                query = query.Where(n => n.CategoryId == categoryId);

            int total = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(total / (double)pageSize);

            var items = await query
                .OrderByDescending(n => n.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new NewsListResponse
            {
                Items = _mapper.Map<List<NewGetAllDto>>(items),
                TotalCount = total,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                HasPreviousPage = page > 1,
                HasNextPage = page < totalPages
            };
        }

        public async Task<NewsDto> GetByIdAsync(int id)
        {
            var news = await _repo.GetByIdAsync(id);
            return news == null ? null : _mapper.Map<NewsDto>(news);
        }

        public async Task<NewsDto> CreateAsync(NewsCreateDto dto)
        {
            var entity = _mapper.Map<News>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<NewsDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, NewsUpdateDto dto)
        {
            var news = await _repo.GetByIdAsync(id);
            if (news == null) return false;

            _mapper.Map(dto, news);
            news.UpdatedAt = DateTime.UtcNow;
            _repo.Update(news);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var news = await _repo.GetByIdAsync(id);
            if (news == null) return false;

            _repo.Delete(news);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}

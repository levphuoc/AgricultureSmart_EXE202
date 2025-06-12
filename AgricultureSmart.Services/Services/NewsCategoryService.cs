using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.NewCategoryModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly INewsCategoryRepository _repository;
        private readonly IMapper _mapper;

        public NewsCategoryService(INewsCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewsCategoryDto>> GetAllAsync(int page, int pageSize)
        {
            var query = _repository.GetAll();
            var paged = query.Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<IEnumerable<NewsCategoryDto>>(paged);
        }

        public async Task<NewsCategoryDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<NewsCategoryDto>(entity);
        }

        public async Task<NewsCategoryDto> CreateAsync(NewsCategoryCreateDto dto)
        {
            var entity = _mapper.Map<NewsCategory>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<NewsCategoryDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, NewsCategoryUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}

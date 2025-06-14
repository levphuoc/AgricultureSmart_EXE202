using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.ReviewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync(int page, int pageSize)
        {
            var reviews = await _repository.GetAllAsync(page, pageSize);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto?> GetByIdAsync(int id)
        {
            var review = await _repository.GetByIdAsync(id);
            return review == null ? null : _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> CreateAsync(ReviewCreateDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            review.CreatedAt = DateTime.UtcNow;
            review.UpdatedAt = DateTime.UtcNow;

            await _repository.AddAsync(review);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> UpdateAsync(int id, ReviewUpdateDto dto)
        {
            var review = await _repository.GetByIdAsync(id);
            if (review == null) return false;

            _mapper.Map(dto, review);
            review.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(review);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _repository.GetByIdAsync(id);
            if (review == null) return false;

            await _repository.DeleteAsync(review);
            return true;
        }
        public async Task<IEnumerable<ReviewDto>> GetByProductIdAsync(int productId)
        {
            var reviews = await _repository.GetByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        /*private async Task RecalculateRatingAsync(int productId)
        {
            double avg = await _repository.GetAverageRatingAsync(productId);
            avg = Math.Round(avg, 2);

            var product = await _repository.GetByIdAsync(productId);
            if (product == null) return;

            product.Rating = avg;
            product.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(product);
        }*/

    }
}

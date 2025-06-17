using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.ReviewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(IReviewRepository reviewRepo,
                        IProductRepository productRepo,
                        IMapper mapper, ILogger<ReviewService> logger)
        {
            _repository = reviewRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _logger = logger;
        }

        /*public async Task<IEnumerable<ReviewDto>> GetAllAsync(int page, int pageSize)
        {
            var reviews = await _repository.GetAllAsync(page, pageSize);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }*/

        public async Task<ReviewListResponse> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (reviews, totalCount) = await _repository.GetAllAsync(pageNumber, pageSize);

                return new ReviewListResponse
                {
                    Items = _mapper.Map<List<ReviewDto>>(reviews),
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated reviews");
                throw;
            }
        }

        public async Task<ReviewDto?> GetByIdAsync(int id)
        {
            var review = await _repository.GetByIdAsync(id);
            return review == null ? null : _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> CreateAsync(ReviewCreateDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            review.CreatedAt = review.UpdatedAt = DateTime.UtcNow;

            await _repository.AddAsync(review);

            await RecalculateRatingAsync(review.ProductId);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> UpdateAsync(int id, ReviewUpdateDto dto)
        {
            var review = await _repository.GetByIdAsync(id);
            if (review == null) return false;

            _mapper.Map(dto, review);
            review.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(review);
            await RecalculateRatingAsync(review.ProductId);

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

        private async Task RecalculateRatingAsync(int productId)
        {
            double avg = await _repository.GetAverageRatingAsync(productId);
            avg = Math.Round(avg, 2);

            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null) return;

            product.Rating = avg;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepo.UpdateAsync(product);
        }
    }
}

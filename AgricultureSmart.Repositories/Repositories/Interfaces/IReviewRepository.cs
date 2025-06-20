﻿using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        /*Task<IEnumerable<Review>> GetAllAsync(int page, int pageSize);*/
        Task<(IEnumerable<Review> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Review?> GetByIdAsync(int id);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Review review);
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
        Task<double> GetAverageRatingAsync(int productId);
    }
}

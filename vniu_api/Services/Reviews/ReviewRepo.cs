using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Reviews;
using vniu_api.Repositories;
using vniu_api.Repositories.Reviews;
using vniu_api.ViewModels.ReviewsViewModels;

namespace vniu_api.Services.Reviews
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewVM> CreateReviewAsync(ReviewVM reviewVM)
        {
            // check user exist
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == reviewVM.UserId);

            if (isUserExist == false)
            {
                throw new Exception("User not found. Please re-check user");
            }

            // check order line exist
            var isOrderLineExist = await _context.OrderLines.AnyAsync(pt => pt.OrderLineId == reviewVM.OrderLineId);

            if (isOrderLineExist == false)
            {
                throw new Exception("Order line not found");
            }

            // map
            var review = _mapper.Map<Review>(reviewVM);

            // add database
            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();

            // return result
            var newReviewVM = _mapper.Map<ReviewVM>(review);

            return newReviewVM;
        }

        public async Task<ReviewVM> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                // review not found
                throw new Exception("Review not found");
            }

            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();

            // map
            var reviewVM = _mapper.Map<ReviewVM>(review);

            return reviewVM;
        }

        public async Task<ReviewVM> GetReviewByIdAsync(int reviewId)
        {
            // check exist id
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                throw new Exception("Review not found");
            }

            var reviewVM = _mapper.Map<ReviewVM>(review);

            return reviewVM;
        }

        public async Task<ICollection<ReviewVM>> GetReviewByProductItemIdAsync(int productItemId)
        {
            // check product item exist
            var productItem = await _context.ProductItems.FindAsync(productItemId);

            if (productItem == null)
            {
                throw new Exception("Product item not found");
            }

            // get list review by product item id
            var reviews = await _context.Reviews
                .Where(r => r.OrderLine.ProductItemId == productItemId)
                .ToListAsync();

            var reviewsVM = _mapper.Map<ICollection<ReviewVM>>(reviews);

            return reviewsVM;
        }

        public async Task<ICollection<ReviewVM>> GetReviewByUserIdAsync(string userId)
        {
            // check exist user id
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                // user not found
                throw new Exception("User not found");
            }

            var reviews = await _context.Reviews.Where(p => p.UserId == userId).OrderByDescending(r => r.ReviewUpdateAt)
                                    .ToListAsync();

            var reviewsVM = _mapper.Map<ICollection<ReviewVM>>(reviews);

            return reviewsVM;
        }

        public async Task<ICollection<ReviewVM>> GetReviewsAsync()
        {
            var reviews = await _context.Reviews.OrderByDescending(p => p.ReviewId).ToListAsync();

            var reviewsVM = _mapper.Map<ICollection<ReviewVM>>(reviews);

            return reviewsVM;
        }

        public async Task<bool> IsReviewExistIdAsync(int reviewId)
        {
            return await _context.Reviews.AnyAsync(p => p.ReviewId == reviewId);
        }

        public async Task<ReviewVM> UpdateReviewAsync(int reviewId, ReviewVM reviewVM)
        {
            // check id different or not ?
            if (reviewVM.ReviewId != reviewId)
            {
                throw new Exception("Review Id is different");
            }

            // check exist id
            var isIdExist = await IsReviewExistIdAsync(reviewId);

            if (isIdExist == false)
            {
                throw new Exception("Review not found");
            }

            // check user
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == reviewVM.UserId);

            if (isUserExist == false)
            {
                throw new Exception("User not found");
            }

            // check order line exist
            var isOrderLineExist = await _context.OrderLines.AnyAsync(pt => pt.OrderLineId == reviewVM.OrderLineId);

            if (isOrderLineExist == false)
            {
                throw new Exception("Order line not found");
            }

            // map
            var updateReview = _mapper.Map<Review>(reviewVM);

            _context.Reviews.Update(updateReview);

            await _context.SaveChangesAsync();

            // result
            var updateReviewVM = _mapper.Map<ReviewVM>(updateReview);

            return updateReviewVM;
        }
    }
}

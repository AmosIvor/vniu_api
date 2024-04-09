using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Reviews;
using vniu_api.Models.EF.Shippings;
using vniu_api.Repositories;
using vniu_api.Repositories.Reviews;
using vniu_api.ViewModels.ReviewsViewModels;

namespace vniu_api.Services.Reviews
{
    public class ReviewImageRepo : IReviewImageRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ReviewImageRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewImageVM> CreateReviewImageAsync(ReviewImageVM reviewImageVM)
        {
            // check exist review id
            var isExistReview = await _context.Reviews.AnyAsync(r => r.ReviewId == reviewImageVM.ReviewId);

            if (isExistReview == false)
            {
                throw new Exception("Review not found");
            }

            var reviewImage = _mapper.Map<ReviewImage>(reviewImageVM);

            _context.ReviewImages.Add(reviewImage);

            await _context.SaveChangesAsync();

            var newReviewImageVM = _mapper.Map<ReviewImageVM>(reviewImage);

            return newReviewImageVM;
        }

        public async Task<ReviewImageVM> DeleteReviewImageByIdAsync(int reviewImageId)
        {
            var reviewImage = await _context.ReviewImages.FindAsync(reviewImageId);

            if (reviewImage == null)
            {
                throw new Exception("Review Image not found");
            }

            _context.ReviewImages.Remove(reviewImage);

            await _context.SaveChangesAsync();

            var reviewImageVM = _mapper.Map<ReviewImageVM>(reviewImage);

            return reviewImageVM;
        }

        public async Task<ICollection<ReviewImageVM>> DeleteReviewImageByReviewIdAsync(int reviewId)
        {
            // check exist review
            var isReviewExist = await _context.Reviews.AnyAsync(r => r.ReviewId == reviewId);

            if (isReviewExist == false)
            {
                // not found
                throw new Exception("Review not found");
            }

            // get list review images
            var reviewImagesDelete = await _context.ReviewImages.Where(ri => ri.ReviewId == reviewId).ToListAsync();

            // remove
            foreach (var reviewImageDelete in reviewImagesDelete)
            {
                _context.ReviewImages.Remove(reviewImageDelete);
            }

            await _context.SaveChangesAsync();

            var reviewImagesDeleteVM = _mapper.Map<ICollection<ReviewImageVM>>(reviewImagesDelete);

            return reviewImagesDeleteVM;
        }

        public async Task<ReviewImageVM> GetReviewImageByIdAsync(int reviewImageId)
        {
            var reviewImage = await _context.ReviewImages.FindAsync(reviewImageId);

            if (reviewImage == null)
            {
                throw new Exception("Review Image not found");
            }

            var reviewImageVM = _mapper.Map<ReviewImageVM>(reviewImage);

            return reviewImageVM;
        }


        public async Task<ICollection<ReviewImageVM>> GetReviewImagesAsync()
        {
            var reviewImages = await _context.ReviewImages.OrderBy(p => p.ReviewImageId).ToListAsync();

            var reviewImagesVM = _mapper.Map<ICollection<ReviewImageVM>>(reviewImages);

            return reviewImagesVM;
        }

        public async Task<ICollection<ReviewImageVM>> GetReviewImagesByReviewIdAsync(int reviewId)
        {
            // check exist review id
            var isReviewExist = await _context.Reviews.AnyAsync(r => r.ReviewId == reviewId);

            if (isReviewExist == false)
            {
                // not found
                throw new Exception("Review not found");
            }

            var reviewImages = await _context.ReviewImages.Where(ri => ri.ReviewId == reviewId).ToListAsync();

            // map
            var reviewImagesVM = _mapper.Map<ICollection<ReviewImageVM>>(reviewImages);

            return reviewImagesVM;
        }

        public async Task<bool> IsReviewImageExistIdAsync(int reviewImageId)
        {
            return await _context.ReviewImages.AnyAsync(p => p.ReviewImageId == reviewImageId);
        }

        public async Task<ReviewImageVM> UpdateReviewImageAsync(int reviewImageId, ReviewImageVM reviewImageVM)
        {
            if (reviewImageVM.ReviewImageId != reviewImageId)
            {
                throw new Exception("Review Image Id is diffrent");
            }

            var isExistReviewImage = await IsReviewImageExistIdAsync(reviewImageId);

            if (isExistReviewImage == false)
            {
                throw new Exception("Review Image not found");
            }

            // check exist review
            var isExistName = await _context.Reviews.AnyAsync(r => r.ReviewId == reviewImageVM.ReviewImageId);

            if (isExistName == false)
            {
                throw new Exception("Review not found");
            }

            var updateReviewImage = _mapper.Map<ReviewImage>(reviewImageVM);

            _context.ReviewImages.Update(updateReviewImage);

            await _context.SaveChangesAsync();

            var updateReviewImageVM = _mapper.Map<ReviewImageVM>(updateReviewImage);

            return updateReviewImageVM;
        }
    }
}

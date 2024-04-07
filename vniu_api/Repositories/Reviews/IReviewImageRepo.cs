
using vniu_api.ViewModels.ReviewsViewModels;

namespace vniu_api.Repositories.Reviews
{
    public interface IReviewImageRepo
    {
        Task<ICollection<ReviewImageVM>> GetReviewImagesAsync();
        Task<ICollection<ReviewImageVM>> GetReviewImagesByReviewIdAsync(int reviewId);
        Task<ReviewImageVM> GetReviewImageByIdAsync(int reviewImageId);
        Task<ReviewImageVM> CreateReviewImageAsync(ReviewImageVM reviewImageVM);
        Task<ReviewImageVM> UpdateReviewImageAsync(int reviewImageId, ReviewImageVM reviewImageVM);
        Task<ReviewImageVM> DeleteReviewImageByIdAsync(int reviewImageId);
        Task<ICollection<ReviewImageVM>> DeleteReviewImageByReviewIdAsync(int reviewId);
        Task<bool> IsReviewImageExistIdAsync(int reviewImageId);
    }
}

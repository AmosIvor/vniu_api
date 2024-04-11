
using vniu_api.ViewModels.ReviewsViewModels;

namespace vniu_api.Repositories.Reviews
{
    public interface IReviewRepo
    {
        Task<ICollection<ReviewVM>> GetReviewsAsync();
        Task<ReviewVM> GetReviewByIdAsync(int reviewId);
        Task<ICollection<ReviewVM>> GetReviewByUserIdAsync(string userId);
        Task<ICollection<ReviewVM>> GetReviewByProductItemIdAsync(int productItemId);
        Task<ReviewVM> CreateReviewAsync(ReviewVM reviewVM);
        Task<ReviewVM> UpdateReviewAsync(int reviewId, ReviewVM reviewVM);
        Task<ReviewVM> DeleteReviewAsync(int reviewId);
        Task<bool> IsReviewExistIdAsync(int reviewId);
    }
}

namespace vniu_api.ViewModels.ReviewsViewModels
{
    public class ReviewVM
    {
        public int ReviewId { get; set; }

        public int ReviewRating { get; set; }

        public string? ReviewComment { get; set; }

        public int OrderLineId { get; set; }

        public string UserId { get; set; }
    }
}

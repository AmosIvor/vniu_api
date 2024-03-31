namespace vniu_api.Models.Responses
{
    public class SuccessResponse<T>
    {
        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}

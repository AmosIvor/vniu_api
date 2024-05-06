namespace vniu_api.Repositories.Utils
{
    public interface IResponseCacheService
    {
        public Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);

        public Task<string> GetCacheResponseAsync(string cacheKey);

        public Task RemoveCacheResponseAsync(string pattern);
    }
}

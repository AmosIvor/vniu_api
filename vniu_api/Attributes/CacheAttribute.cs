using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using vniu_api.Configuration;
using vniu_api.Repositories.Utils;

namespace vniu_api.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        // it will run after authorize annotation and before action method => [Cache(1000)]
        private readonly int _timeToLiveSeconds;

        public CacheAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // check cache enabled or not ?
            var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisConfiguration>();

            if (!cacheConfiguration.Enabled)
            {
                // if cache doens't enabled => call the next middleware (controller)
                await next(); // in this step, it will run into Controller and return
                return;
            }

            // You decide using cache

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            // You shoud config cache key
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            // if cache has data => return and not run into action method (controller)
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contextResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contextResult;
                return;
            }

            // if cache doesn't contain data => set cache
            var executedContext = await next(); // run into action method
            if (executedContext.Result is OkObjectResult objectResult)
            {
                // if success => set cache
                await cacheService.SetCacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            // You should config cache key
            // Normally, cache key = endpoint
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            // request.Query.OrderBy(x => x.Key) => Like you have 3 param => For loop these 3 params
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}--{value}");
            }

            return keyBuilder.ToString();
        }
    }
}

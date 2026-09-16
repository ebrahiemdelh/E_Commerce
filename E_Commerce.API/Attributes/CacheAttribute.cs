using E_Commerce.Domain.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace E_Commerce.API.Attributes
{
    public class CacheAttribute(int durationsInSeconds = 90) : ActionFilterAttribute
    {
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var service = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var key = CreateCacheKey(context.HttpContext.Request);

            var cache = await service.GetASync(key);
            if (cache is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cache,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var executed = await next.Invoke();

            if (executed.Result is OkObjectResult { Value: not null } Ok)
            {
                await service.SetAsync(key, Ok.Value, TimeSpan.FromSeconds(durationsInSeconds));
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path).Append('?');
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append(item.Key).Append('=').Append(item.Value).Append('&');
            }
            return key.ToString();
        }
    }
}

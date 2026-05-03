using TaskManagerAPI.Services.Interface;

namespace TaskManagerAPI.Services
{
    public class HttpContextHelper : IHttpContextHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> GetUserId()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst("UserId")?.Value;
        }
    }
}

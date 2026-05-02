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

        public async Task<int> GetUserId()
        {
            object userIdObj = null;
            if(_httpContextAccessor?.HttpContext?.Items.TryGetValue("UserId", out userIdObj) == true)
            {
                return Int32.TryParse(userIdObj?.ToString(), out var userId) ? userId : 0;
            }
            return 0;
        }
    }
}

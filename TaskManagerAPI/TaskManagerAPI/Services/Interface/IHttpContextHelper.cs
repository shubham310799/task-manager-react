namespace TaskManagerAPI.Services.Interface
{
    public interface IHttpContextHelper
    {
        Task<string?> GetUserId();
    }
}

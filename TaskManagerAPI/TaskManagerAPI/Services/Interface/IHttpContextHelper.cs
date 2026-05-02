namespace TaskManagerAPI.Services.Interface
{
    public interface IHttpContextHelper
    {
        Task<int> GetUserId();
    }
}

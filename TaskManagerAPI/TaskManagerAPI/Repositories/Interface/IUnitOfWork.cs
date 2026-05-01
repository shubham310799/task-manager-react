namespace TaskManagerAPI.Repositories.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}

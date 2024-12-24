
using TodoApp.Infrastructure.Repositories.TodosRepositories;
using TodoApp.Infrastructure.Repositories.UserRepositories;

namespace TodoApp.Infrastructure
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITodosRepository TodosRepository { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}

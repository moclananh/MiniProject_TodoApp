using TodoApp.Domain.Models.EF;
using TodoApp.Infrastructure.Repositories.TodosRepositories;
using TodoApp.Infrastructure.Repositories.UserRepositories;

namespace TodoApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IUserRepository? _userRepository;
        private ITodosRepository? _todosRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_dbContext);

        public ITodosRepository TodosRepository => _todosRepository ??= new TodosRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

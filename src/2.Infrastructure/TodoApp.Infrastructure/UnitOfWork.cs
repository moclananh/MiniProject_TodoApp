﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;
using TodoApp.BuildingBlock.Exceptions;
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
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(ApplicationDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_dbContext);

        public ITodosRepository TodosRepository =>
            _todosRepository ??= new TodosRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error saving changes.", ex.Message);
            }
        }

        // Begin a transaction and manage the scope for pessimistic locking
        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred while committing changes to the database.");
            }
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
             _logger.LogError("An error occurred while committing changes to the database.");
        }

        // Dispose the database context and transaction when done
        public void Dispose()
        {
            _dbContext?.Dispose();
            _logger.LogInformation("Disposed UnitOfWork and Database context.");
        }
    }
}

using Blazor.Domain.Common;
using Blazor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Application.Unit_of_work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync() > 0;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // log message and enteries
            }
            catch (DbUpdateException ex)
            {
                // log message and enteries
            }
            catch (Exception ex)
            {
                // Log here.
            }
            return false;
        }
        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}

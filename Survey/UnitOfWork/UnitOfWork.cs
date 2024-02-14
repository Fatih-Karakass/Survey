using Survey.DataAccsess;

namespace Survey.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _appDbContext;

		public UnitOfWork(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public void saveChanges()
		{
			_appDbContext.SaveChanges();
		}

		public async Task SaveChangesAsync()
		{
			await _appDbContext.SaveChangesAsync();
		}
	}
}

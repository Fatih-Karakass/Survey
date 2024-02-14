namespace Survey.UnitOfWork
{
	public interface IUnitOfWork
	{
		void saveChanges();
		Task SaveChangesAsync();

	}
}

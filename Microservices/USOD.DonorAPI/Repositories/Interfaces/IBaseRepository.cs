namespace USOD.DonorAPI.Repositories.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		Task Create(T entity);

		Task Create(List<T> entity);

		Task Delete(T entity);

		Task Delete(List<T> entity);

		IQueryable<T> Get();

		Task<T> Update(T entity);

		Task<List<T>> Update(List<T> entity);
	}
}

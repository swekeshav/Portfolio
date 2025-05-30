namespace Portfolio.Web.Data;

public interface IRepository<T>
{
	Task Add(T item);
	void Save(IEnumerable<T> items);
	Task<IEnumerable<T>> GetAll();
}

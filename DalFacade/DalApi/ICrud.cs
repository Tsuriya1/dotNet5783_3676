using DalFacade.DO;
//using System.ComponentModel;

public interface ICrud<T>
{
	public int add(T obj);
	public void delete(int ID);
	public void update(T obj);
	public T get(int ID);
	public T getObject(Func<T?, bool>? func);
	public IEnumerable<T?> get(Func<T?, bool>? func = null);
}
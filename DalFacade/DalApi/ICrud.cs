using DalFacade.DO;
//using System.ComponentModel;

public interface ICrud<T>
{
	public int add(T obj);
	public void delete(int ID);
	public void update(T obj);
	public T get(int ID);
    public IEnumerable<T> get();
}
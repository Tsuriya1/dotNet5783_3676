using DalFacade.DO;
//using System.ComponentModel;

namespace DalApi;

public interface ICrud<T>
{
	public int Add(T obj);
	public void Delete(int ID);
	public void Update(T obj);
	public T Get(int ID);
    public IEnumerable<T> get();
}
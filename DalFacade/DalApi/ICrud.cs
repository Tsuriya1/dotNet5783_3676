using DO;
using System.ComponentModel;

namespace DalApi;

public interface ICrud<T>
{
	public static void Add(T obj);
	public static T Delete(T obj);
	public static void Update(T obj);
	public static T Get(T obj);
}

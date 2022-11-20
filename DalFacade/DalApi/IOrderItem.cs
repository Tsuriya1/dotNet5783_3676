using DalFacade.DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public IEnumerable<OrderItem> get(OrderItem item);

}
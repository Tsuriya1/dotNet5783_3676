using DalFacade.DO;
using DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public IEnumerable<OrderItem> get(OrderItem item);

}
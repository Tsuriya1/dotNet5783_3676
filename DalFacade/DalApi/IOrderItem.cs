using DalFacade.DO;

public interface IOrderItem : ICrud<OrderItem>
{
    public IEnumerable<OrderItem?> get(DalFacade.DO.Order order);
    public DalFacade.DO.OrderItem get(int productId, int orderId);


}
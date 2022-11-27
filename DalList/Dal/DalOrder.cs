namespace Dal;
using DalApi;
using DO;

internal class DalOrder : IOrder
{
    public int add(DalFacade.DO.Order order)
    {
        order.ID = Dal.DataSource.Config.getOrderId();
        DataSource.ordersList.Add(order);
        //DataSource.ordersList[Dal.DataSource.Config.orderInx] = order;
        //Dal.DataSource.Config.orderInx++;
        return order.ID;
    }

    public DalFacade.DO.Order get(int ID)
    {
        int len = DataSource.ordersList.Count;
        for (int i = 0; i < len; i++)
        {
            if(DataSource.ordersList[i].ID == ID)
            {
                return Dal.DataSource.ordersList[i];
            } 
        }
        throw new Exception("order not found");
    }

    public List<DalFacade.DO.Order> get()
    {
        List<DalFacade.DO.Order> orders = new List<DalFacade.DO.Order>();
        for (int i = 0; i < DataSource.ordersList.Count; i++)
        {
            orders.Add(Dal.DataSource.ordersList[i]);
        }
        return orders;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < DataSource.ordersList.Count; i++)
        {
            if (DataSource.ordersList[i].ID == ID)
            {
                DataSource.ordersList.Remove(DataSource.ordersList[i]);
                return;
            }
        }
        throw new Exception("order not found");

    }

    public void update(DalFacade.DO.Order order)
    {
        for (int i = 0; i < DataSource.ordersList.Count; i++)
        {
            if (Dal.DataSource.ordersList[i].ID == order.ID)
            {
                Dal.DataSource.ordersList[i] = order;
                return;
            }
        }
        throw new Exception("order not found");
    }
}


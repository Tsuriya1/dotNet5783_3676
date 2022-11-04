namespace Dal;

public class DalOrder
{
    public int add(DalFacade.DO.Order order)
    {
        order.ID = Dal.DataSource.Config.getOrderId();
        Dal.DataSource.ordersList[Dal.DataSource.Config.orderInx] = order;
        Dal.DataSource.Config.orderInx++;
        return order.ID;
    }

    public DalFacade.DO.Order get(int ID)
    {

        for (int i = 0; i < Dal.DataSource.Config.orderInx; i++)
        {
            if(Dal.DataSource.ordersList[i].ID == ID)
            {
                return Dal.DataSource.ordersList[i];
            } 
        }
        throw new Exception("order not found");
    }

    public DalFacade.DO.Order[] get()
    {
        DalFacade.DO.Order[] orders = new DalFacade.DO.Order[Dal.DataSource.Config.orderInx];
        for (int i = 0; i < Dal.DataSource.Config.orderInx; i++)
        {
            orders[i] = Dal.DataSource.ordersList[i];
        }
        return orders;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < Dal.DataSource.Config.orderInx; i++)
        {
            if (Dal.DataSource.ordersList[i].ID == ID)
            {
                for(int j = i+1; j< Dal.DataSource.Config.orderInx; j++)
                {
                    Dal.DataSource.ordersList[j - 1] = Dal.DataSource.ordersList[j];
                }
                Dal.DataSource.Config.orderInx--;
                return;
            }
        }
        throw new Exception("order not found");

    }

    public void update(DalFacade.DO.Order order)
    {
        for (int i = 0; i < Dal.DataSource.Config.orderInx; i++)
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


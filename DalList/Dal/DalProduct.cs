namespace Dal;

public class DalProduct
{
    public int add(DalFacade.DO.Product product)
    {
        for (int i=0;i< Dal.DataSource.Config.productInx; i++)
        {
            if (Dal.DataSource.productList[i].ID == product.ID)
            {
                throw new Exception("product ID already exists");
            }
        }
        Dal.DataSource.productList[Dal.DataSource.Config.productInx] = product;
        Dal.DataSource.Config.productInx++;
        return product.ID;
    }

    public DalFacade.DO.Product get(int ID)
    {

        for (int i = 0; i < Dal.DataSource.Config.productInx; i++)
        {
            if (Dal.DataSource.productList[i].ID == ID)
            {
                return Dal.DataSource.productList[i];
            }
        }
        throw new Exception("product not found");
    }

    public DalFacade.DO.Product[] get()
    {
        DalFacade.DO.Product[] products = new DalFacade.DO.Product[Dal.DataSource.Config.productInx];
        for (int i = 0; i < Dal.DataSource.Config.productInx; i++)
        {
            products[i] = Dal.DataSource.productList[i];
        }
        return products;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < Dal.DataSource.Config.productInx; i++)
        {
            if (Dal.DataSource.productList[i].ID == ID)
            {
                for (int j = i + 1; j < Dal.DataSource.Config.productInx; j++)
                {
                    Dal.DataSource.productList[j - 1] = Dal.DataSource.productList[j];
                }
                Dal.DataSource.Config.productInx--;
                return;
            }
        }
        throw new Exception("product not found");

    }

    public void update(DalFacade.DO.Product product)
    {
        for (int i = 0; i < Dal.DataSource.Config.productInx; i++)
        {
            if (Dal.DataSource.productList[i].ID == product.ID)
            {
                Dal.DataSource.productList[i] = product;
                return;
            }
        }
        throw new Exception("product not found");
    }
}
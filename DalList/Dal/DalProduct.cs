namespace Dal;

public class DalProduct
{
    public int add(DalFacade.DO.Product product)
    {
        for (int i=0;i< Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].ID == product.ID)
            {
                throw new Exception("product ID already exists");
            }
        }
        Dal.DataSource.productList.Add(product);
        return product.ID;
    }

    public DalFacade.DO.Product get(int ID)
    {

        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].ID == ID)
            {
                return Dal.DataSource.productList[i];
            }
        }
        throw new Exception("product not found");
    }

    public List<DalFacade.DO.Product> get()
    {
        List<DalFacade.DO.Product> products = new List<DalFacade.DO.Product>();
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            products[i] = Dal.DataSource.productList[i];
        }
        return products;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].ID == ID)
            {
                DataSource.productList.Remove(DataSource.productList[i]);
                return;
            }
        }
        throw new Exception("product not found");

    }

    public void update(DalFacade.DO.Product product)
    {
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].ID == product.ID)
            {
                DataSource.productList.Insert(i, product);
                return;
            }
        }
        throw new Exception("product not found");
    }
}
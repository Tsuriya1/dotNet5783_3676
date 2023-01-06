using System.Collections.Generic;
using DalFacade.DO;
namespace Dal;

internal class DalProduct : Iproduct
{
    public int add(DalFacade.DO.Product product)
    {
        for (int i=0;i< Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].Value.ID == product.ID)
            {
                throw new DalFacade.DO.DuplicateException ("product ID already exists");
            }
        }
        Dal.DataSource.productList.Add(product);
        return product.ID;
    }

    public DalFacade.DO.Product get(int ID)
    {

        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].HasValue)
            {
                if (Dal.DataSource.productList[i].Value.ID == ID)
                {
                    return Dal.DataSource.productList[i].Value;
                }
            }
            
        }
        throw new DalFacade.DO.NotFoundException ("product not found");
    }
    public Product getObject(Func<Product, bool>? func)
    {
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].HasValue)
            {
                if (func(Dal.DataSource.productList[i].Value))
                {
                    return Dal.DataSource.productList[i].Value;
                }
            }

        }
        throw new DalFacade.DO.NotFoundException("product not found");
    }
    public IEnumerable<Product> get(Func<Product, bool>? func = null)
    {
        List<DalFacade.DO.Product> products = new List<DalFacade.DO.Product>();
        _ = DataSource.productList.Count;
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (!Dal.DataSource.productList[i].HasValue)
            {
                continue;
            }
            if(func != null)
            {
                if (func(Dal.DataSource.productList[i].Value))
                {
                    products.Add(Dal.DataSource.productList[i].Value);
                }
            }
            else
            {
                products.Add(Dal.DataSource.productList[i].Value);
            }

        }
        return products;

    }

    public void delete(int ID)
    {
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].Value.ID == ID)
            {
                DataSource.productList.Remove(DataSource.productList[i]);
                return;
            }
        }
        throw new DalFacade.DO.NotFoundException("product not found");

    }

    public void update(DalFacade.DO.Product product)
    {
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].Value.ID == product.ID)
            {
                DataSource.productList.Insert(i, product);
                return;
            }
        }
        throw new DalFacade.DO.NotFoundException("product not found");
    }

    
}
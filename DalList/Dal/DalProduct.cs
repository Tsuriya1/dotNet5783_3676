using System.Collections;
using System.Collections.Generic;
using DalFacade.DO;
namespace Dal;

internal class DalProduct : Iproduct
{
    public int add(DalFacade.DO.Product product)
    {
        if (DataSource.productList.Count(x=>x.HasValue && x.Value.ID==product.ID)>0)
        {
            throw new DalFacade.DO.DuplicateException("product ID already exists");
        }



        /*for (int i=0;i< Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].Value.ID == product.ID)
            {
                throw new DalFacade.DO.DuplicateException ("product ID already exists");
            }
        }
*/        Dal.DataSource.productList.Add(product);
        return product.ID;
    }

    public DalFacade.DO.Product get(int ID)
    {

        var newList = Dal.DataSource.productList.Where(x=>x.HasValue && x.Value.ID==ID);
        if (newList.Count()>0)
        {
            return newList.First().Value;
        }
/*
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].HasValue)
            {
                if (Dal.DataSource.productList[i].Value.ID == ID)
                {
                    return Dal.DataSource.productList[i].Value;
                }
            }
            
        }*/
        throw new DalFacade.DO.NotFoundException ("product not found");
    }
    public Product getObject(Func<Product, bool>? func)
    {
        if (func == null)
        {
            throw new DalFacade.DO.NotFoundException("no filter parameter is given");
        }
        var newList = Dal.DataSource.productList.Where(x =>x.HasValue && func(x.Value));
        if (newList != null && newList.Count() > 0)
        {
            return newList.First().Value;
        }
/*
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].HasValue)
            {
                if (func(Dal.DataSource.productList[i].Value))
                {
                    return Dal.DataSource.productList[i].Value;
                }
            }

        }*/
        throw new DalFacade.DO.NotFoundException("product not found");
    }
    public IEnumerable<Product> get(Func<Product, bool>? func = null)
    {
        IEnumerable<Product> products;
        if (func == null)
        {
            return Dal.DataSource.productList.Where(x => x.HasValue).Select(x => x.Value);
        }
        products = Dal.DataSource.productList.Where(x => x.HasValue && func(x.Value)).Select(x => x.Value);


/*        List<DalFacade.DO.Product> products = new List<DalFacade.DO.Product>();
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

        }*/
        return products;

    }

    public void delete(int ID)
    {
        int count = Dal.DataSource.productList.RemoveAll(x => (x.HasValue && x.Value.ID == ID));
        if (count == 0)
        {
            throw new DalFacade.DO.NotFoundException("product not found");
        }
/*
        for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].Value.ID == ID)
            {
                DataSource.productList.Remove(DataSource.productList[i]);
                return;
            }
        }*/
        //throw new DalFacade.DO.NotFoundException("product not found");

    }

    public void update(DalFacade.DO.Product product)
    {
        var products = from prod in DataSource.productList
                   where ((prod.HasValue) && (prod.Value.ID == product.ID))
                   select prod.Value;
        if (products != null && products.Count() > 0)
        {
            delete(product.ID);
            Dal.DataSource.productList.Add(products.First());
            return;
        }

        /*for (int i = 0; i < Dal.DataSource.productList.Count; i++)
        {
            if (Dal.DataSource.productList[i].Value.ID == product.ID)
            {
                delete(product.ID);
                DataSource.productList.Insert(i, product);
                return;
            }
        }*/
        throw new DalFacade.DO.NotFoundException("product not found");
    }

    
}
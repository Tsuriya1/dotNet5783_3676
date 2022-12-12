using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using Dal;
using DalFacade.DO;
using BlApi;
using DalFacade.DalApi;

namespace Bllmplementation
{
    internal class Product : BlApi.Iproduct
    {
        private IDal Dal = new DalList();
        private BO.Category ConvertCategory(DalFacade.DO.Product products)
        {

            if (products.Category == DalFacade.DO.Category.Family)
                if (products.Category == 0)
                {
                    return BO.Category.Family;
                }
            if (products.Category == DalFacade.DO.Category.Sport)
            {
                return BO.Category.Sport;
            }
            if (products.Category == DalFacade.DO.Category.Mini)
            {
                return BO.Category.Mini;
            }
            if (products.Category == DalFacade.DO.Category.Motorcycle)
            {
                return BO.Category.Motorcycle;
            }
            if (products.Category == DalFacade.DO.Category.Exclusive)
            {
                return BO.Category.Exclusive;

            }
            return BO.Category.Sport;
        }
        public List<ProductForList> GetProducts()
        {
            IEnumerable<DalFacade.DO.Product> products = Dal.Product.get();
            List<ProductForList> forLists = new List<ProductForList>();
            int len = products.Count();
            for (int i = 0; i < len; i++)
            {
                ProductForList a = new ProductForList();
                a.ID = products.ElementAt(i).ID;
                a.Name = products.ElementAt(i).Name;
                a.Price = products.ElementAt(i).Price;
                a.Category = ConvertCategory(products.ElementAt(i));
                forLists.Add(a);
            }
            return forLists;
        }
        public BO.Product getProductsDetails(int ID)
        {
            if (ID > 0)
            {
                DalFacade.DO.Product product;
                try
                {
                    product = Dal.Product.get(ID);
                }
                catch (Exception e)
                {
                    throw new Exception("product not found");
                }
                BO.Product newProduct = new BO.Product();
                newProduct.ID = product.ID;
                newProduct.Name = product.Name;
                newProduct.Price = product.Price;
                newProduct.InStock = product.InStock;
                newProduct.Category = ConvertCategory(product);
                return newProduct;
            }
            else
            {
                throw new Exception("Product ID < 0");
            }
        }
        public ProductItem getProductsDetails(int ID, BO.Cart cart)
        {
            if (ID > 0)
            {
                DalFacade.DO.Product product;
                try
                {
                    product = Dal.Product.get(ID);
                }
                catch (Exception e)
                {
                    throw new Exception("product not found");
                }
                ProductItem newProduct = new ProductItem();
                newProduct.Price = product.Price;
                newProduct.Name = product.Name;
                newProduct.ID = product.ID;
                // check this:
                newProduct.Amount = product.InStock;
                if (product.InStock > 0)
                {
                    newProduct.InStock = true;
                }
                newProduct.Category = ConvertCategory(product);
                return newProduct;
            }
            throw new Exception("Product ID < 0");
        }
        public void addProduct(BO.Product product)
        {
            DalFacade.DO.Product newProduct = new DalFacade.DO.Product();
            if (product.ID >= 100000 && product.Name != null && product.Price > 0 && product.InStock >= 0)
            {
                newProduct.ID = product.ID;
                newProduct.Name = product.Name;
                newProduct.Price = product.Price;
                newProduct.InStock = product.InStock;
                newProduct.Category = (DalFacade.DO.Category)(int)product.Category;
                try
                {
                    Dal.Product.add(newProduct);
                }
                catch (Exception e) { }
            }
            else
            {
                throw new Exception("one or more of details is invalid");
            }
        }
        public void removeProduct(int productId)
        {
            DalFacade.DO.OrderItem toDelete = new DalFacade.DO.OrderItem();
            try
            {
                toDelete = Dal.OrderItem.get(productId);
            }
            catch (Exception e) { Dal.Product.delete(productId); }
        }
        public void updateData(BO.Product product)
        {
            DalFacade.DO.Product toUpdate = new DalFacade.DO.Product();
            if (product.ID >= 100000 && product.Name != null && product.Price > 0 && product.InStock >= 0)
            {
                toUpdate.ID = product.ID;
                toUpdate.Name = product.Name;
                toUpdate.Price = product.Price;
                toUpdate.InStock = product.InStock;
                toUpdate.Category = (DalFacade.DO.Category)(int)product.Category;
                try
                {
                    Dal.Product.update(toUpdate);
                }
                catch (Exception e) { }
            }
            else
            {
                throw new Exception("one or more of details is invalid");
            }
        }

    }
}

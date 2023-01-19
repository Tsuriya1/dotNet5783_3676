using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
//using Dal;


namespace BlImplementation
{
    internal class Product : BlApi.Iproduct
    {
        IDal? Dal = DalApi.Factory.Get();
        //private IDal Dal = new DalList();
        private BO.Category? ConvertCategory(DalFacade.DO.Product product)
        {
            if (product.Category.HasValue)
            {
                if (product.Category == DalFacade.DO.Category.Family)
                {
                    return BO.Category.Family;

                }
                if (product.Category == DalFacade.DO.Category.Sport)
                {
                    return BO.Category.Sport;
                }
                if (product.Category == DalFacade.DO.Category.Mini)
                {
                    return BO.Category.Mini;
                }
                if (product.Category == DalFacade.DO.Category.Motorcycle)
                {
                    return BO.Category.Motorcycle;
                }
                if (product.Category == DalFacade.DO.Category.Exclusive)
                {
                    return BO.Category.Exclusive;

                }
                return BO.Category.Sport;
            }
            return null;   
        }


        private BO.ProductForList? convert2ProductForList(DalFacade.DO.Product product)
        {
            ProductForList a = new ProductForList();
            a.ID = product.ID;
            a.Name = product.Name;
            a.Price = product.Price;
            a.Category = ConvertCategory(product);
            return a;
        }


        IEnumerable<ProductForList?> BlApi.Iproduct.GetProducts()
        {
            IEnumerable<DalFacade.DO.Product> products = Dal.Product.get();
            var a =  products.Select(x=>convert2ProductForList(x));
            /*List<ProductForList?> forLists = new List<ProductForList?>();
            int len = products.Count();
            for (int i = 0; i < len; i++)
            {
                ProductForList a = new ProductForList();
                a.ID = products.ElementAt(i).ID;
                a.Name = products.ElementAt(i).Name;
                a.Price = products.ElementAt(i).Price;
                a.Category = ConvertCategory(products.ElementAt(i));
                forLists.Add(a);
            }*/
            return a;
        }


        public IEnumerable<ProductForList?> GetProductsByCategory(Category category)
        {
            
            IEnumerable<DalFacade.DO.Product> products1 = Dal.Product.get();
            var productGroups =
            from w in products1
            group w by w.Category into g
            select new { category = g.Key, products = g };

            //productGroups.
            var filtered = productGroups.Where(x => ConvertCategory(x.products.ElementAt(0)) == category);
            if (!filtered.Any())
            {
                return new List<ProductForList?>();
            }
            return filtered.First().products.Select(x=>convert2ProductForList(x));
            /*List<ProductForList?> forLists = new List<ProductForList?>();
            int len = products1.Count();
            for (int i = 0; i < len; i++)
            {
                if(ConvertCategory(products1.ElementAt(i)) != category)
                {
                    continue;
                } 
                ProductForList a = new ProductForList();
                a.ID = products1.ElementAt(i).ID;
                a.Name = products1.ElementAt(i).Name;
                a.Price = products1.ElementAt(i).Price;
                a.Category = ConvertCategory(products1.ElementAt(i));
                forLists.Add(a);
            }
            return forLists;*/
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
                catch (DalFacade.DO.NotFoundException e)
                {
                    throw new NotFoundError ("product not found",e);
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
                throw new NotValidValue ("Product ID < 0");
            }
        }

        private ProductItem convertProduct2productItem(DalFacade.DO.Product product)
        {
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

        public ProductItem getProductsDetails(int ID, BO.Cart cart)
        {
            if (ID > 0)
            {
                DalFacade.DO.Product product;
                try
                {
                    product = Dal.Product.get(ID);
                }
                catch (DalFacade.DO.NotFoundException e)  
                {
                    throw new NotFoundError ("product not found",e);
                }
                return convertProduct2productItem(product);
            
            }
            throw new NotValidValue ("Product ID < 0");
        }

        public IEnumerable<ProductItem> getCatalog()
        {
            IEnumerable<DalFacade.DO.Product> products = Dal.Product.get();
            IEnumerable<ProductItem> newCatalog = products.Select(x => convertProduct2productItem(x)); 
            return newCatalog;

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
                catch (DalFacade.DO.NotFoundException e)
                {
                    throw new BO.NotValidValue("product Id already exist", e);
                }
            }
            else
            {
                throw new NotValidValue ("one or more of details is invalid");
            }
        }
        public void removeProduct(int productId)
        {
            DalFacade.DO.OrderItem toDelete = new DalFacade.DO.OrderItem();
            try
            {
                toDelete = Dal.OrderItem.get(productId);
            }
            catch (DalFacade.DO.NotFoundException e)  
            {
                throw new NotFoundError("product not found",e);
            }
            Dal.Product.delete(productId);
        }
        public void updateData(BO.Product product)
        {
            DalFacade.DO.Product toUpdate = new DalFacade.DO.Product();
            if (product.ID >= 100000 && product.Name != null)
            {
                if(product.Price > 0 && product.InStock >= 0)
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
                    catch (DalFacade.DO.NotFoundException e) 
                    {
                        throw new NotFoundError("product not found update failed", e);
                    }
                }
                else
                {
                    throw new BO.StockError ("the product is out of stock");
                }
            }
            else
            {
                throw new NotValidValue("one or more of details is invalid");
            }
        }

        
    }
}

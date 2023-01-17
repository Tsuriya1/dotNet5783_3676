using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
//using Dal;
using DalFacade.DO;

namespace BlImplementation
{
    internal class Cart : BlApi.Icart
    {
        private IDal? Dal = DalApi.Factory.Get();



        BO.Cart Icart.deleteItemFromCart(BO.Cart cart, int itemId)
        {
            return delete_item(cart, itemId);
        }

        private BO.Cart delete_item(BO.Cart cart, int itemId)
        {
            if (cart.Items == null)
            {
                return cart;
            }
            cart.Items.RemoveAll(x =>
            {
                if (x.HasValue && x.Value.ID == itemId)
                {
                    return true;
                }
                return false;
            });
            return cart;
        }

        BO.Cart Icart.AddProduct(BO.Cart cart, int Id)
        {
            DalFacade.DO.Product product;
            try
            {
                product = Dal.Product.get(Id);
            }
            catch(DalFacade.DO.NotFoundException e)
            {
                throw new BO.NotFoundError("product not found", e);
            }
            
            if (product.InStock < 1)
            {
                throw new BO.StockError("the product is out of stock");
            }
            int maxId = -1;
            //          for (int i = 0; i < cart.Items.Count; i++)
            if (cart.Items != null)
            {
                maxId = cart.Items.Max(x =>
                {
                    if (!x.HasValue)
                    {
                        return -1;
                    }
                    return x.Value.ID;
                });

                var filtered = cart.Items.Where(item => 
                {
                    if (item.HasValue)
                    {
                        return item.Value.ProductId == Id;

                    }
                    else
                    {
                        return false;
                    }
                });

                if (filtered.Any())
                {
                    BO.OrderItem item = filtered.FirstOrDefault().Value; 
                    BO.OrderItem a = new BO.OrderItem();
                    a.ID = item.ID;
                    a.ProductId = item.ProductId;
                    a.Name = item.Name;
                    a.Price = item.Price;
                    a.Amount = 1;
                    a.Amount += item.Amount;
                    a.TotalPrice = a.Price * a.Amount;
                    a.TotalPrice = item.TotalPrice + a.TotalPrice;
                    
                    delete_item(cart,item.ID);
                    cart.Items.Add(a);
                    cart.TotalPrice += a.TotalPrice;
                    return cart;
                }


            }
            


            /*foreach (BO.OrderItem item in cart.Items)
            {
                *//*if (item.ID > maxId)
                {
                    maxId = item.ID;
                }*//*
                if (item.ProductId == Id)
                {
                    BO.OrderItem a = new BO.OrderItem();
                    a.ID = item.ID;
                    a.ProductId = item.ProductId;
                    a.Name = item.Name;
                    a.Price = item.Price;
                    a.Amount = 1;
                    a.Amount += item.Amount;
                    a.TotalPrice = a.Price * a.Amount;
                    a.TotalPrice = item.TotalPrice + a.TotalPrice;
                    cart.Items.Remove(item);
                    cart.Items.Add(a);
                    cart.TotalPrice += a.TotalPrice;
                    return cart;
                }
            }*/

            BO.OrderItem ToAdd = new BO.OrderItem();
            ToAdd.Name = product.Name;
            ToAdd.ProductId = product.ID;
            ToAdd.Price = product.Price;
            ToAdd.Amount = 1;
            ToAdd.TotalPrice = product.Price;
            ToAdd.ID = maxId + 1;
            cart.Items.Add(ToAdd);
            cart.TotalPrice += ToAdd.Price;
            return cart;
        }
        BO.Cart Icart.Update (BO.Cart cart, int ProductId, int newAmount)
        {
            DalFacade.DO.Product product;
            try
            {
                product = Dal.Product.get(ProductId);
            }
            catch (DalFacade.DO.NotFoundException e)
            {
                throw new NotFoundError ("product not found",e);
            }
            if (product.InStock < newAmount)
            {
                throw new StockError ("The asked amount is out of stock");
            }

            var update_product = from BO.OrderItem item in cart.Items where item.ProductId == ProductId select item;
            if (update_product != null && update_product.Count() > 0)
            {
                if ((update_product.First().Amount != newAmount) && (newAmount > 0))
                {

                    BO.OrderItem a = new BO.OrderItem();
                    a.Price = product.Price;
                    a.ProductId = product.ID;
                    a.ID = update_product.First().ID;
                    a.Name = product.Name;
                    a.Amount = newAmount;
                    a.TotalPrice = a.Amount * a.Price;
                    cart.TotalPrice -= update_product.First().TotalPrice;
                    cart.Items.Remove(update_product.First());
                    cart.Items.Add(a);
                    cart.TotalPrice += a.TotalPrice;
                    return cart;
                }
                else if  (newAmount == 0)
                {
                    cart.TotalPrice -= update_product.First().TotalPrice;
                    cart.Items.Remove(update_product.First());
                    return cart;
                }
            }

            
            throw new NotFoundError ("order item not found");


        }


        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        private void cartValidation(BO.Cart cart)
        {
            if (cart.CustomerName == null)
            {
                throw new NotValidValue("not a valid Customer Name");
            }
            if(cart.CustomerAddress == null)
            {
                throw new NotValidValue("not a valid Customer address");
            }
            if (!IsValidEmail(cart.CustomerEmail))
            {
                throw new NotValidValue("not a valid Customer Email address");
            }
        }
        private void checkCart(BO.OrderItem item)
        {
            DalFacade.DO.Product product;
            try
            {
                product = Dal.Product.get(item.ProductId);
            }
            catch (DalFacade.DO.NotFoundException e)
            {
                throw new BO.NotFoundError("Item not found",e);
            }
            if (item.Amount <= 0)
            {
                throw new BO.StockError("items amount is less than zero");
            }
            if (product.InStock < item.Amount)
            {
                throw new BO.StockError("Not enough in stock");
            }
        }

        bool add_order_item(BO.OrderItem? item,int id)
        {
            DalFacade.DO.OrderItem orderItem = new DalFacade.DO.OrderItem();

            if (item.HasValue)
            {
                return false;

            }
            orderItem.ProductId = item.Value.ProductId;
            orderItem.Amount = item.Value.Amount;
            orderItem.Price = item.Value.Price;
            orderItem.OrderId = id;
            orderItem.ID = 0;
            Dal.OrderItem.add(orderItem);

            DalFacade.DO.Product product = Dal.Product.get(item.Value.ProductId);

            product.InStock = product.InStock - orderItem.Amount;
            if (product.InStock == 0)
            {
                Dal.Product.delete(product.ID);
                return true;
            }
            Dal.Product.update(product);
            return true;
        }

        void Icart.Confirm(BO.Cart cart)
        {
            cartValidation(cart);
            foreach (BO.OrderItem item in cart.Items)
            {
                checkCart(item);
            }
            DalFacade.DO.Order order = new DalFacade.DO.Order();
            order.OrderDate = DateTime.Now;
            order.CustumerAdress = cart.CustomerAddress;
            order.CustumerAdress = cart.CustomerEmail;
            order.CustumerName = cart.CustomerName;
            int id = Dal.Order.add(order);
            DalFacade.DO.OrderItem orderItem = new DalFacade.DO.OrderItem();

            cart.Items.Select(x => add_order_item(x,id));
            
        }

        

        
    }
}
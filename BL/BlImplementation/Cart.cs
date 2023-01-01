using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalFacade.DO;

namespace BlImplementation
{
    internal class Cart : Icart
    {
        private IDal Dal = new DalList();

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
            for (int i = 0; i < cart.Items.Count; i++)
            {
                if (!cart.Items[i].HasValue)
                {
                    continue;
                }
                if (cart.Items[i].Value.ID > maxId)
                {
                    maxId = cart.Items[i].Value.ID;
                }
                if (cart.Items[i].Value.ProductId == Id)
                {
                    BO.OrderItem a = new BO.OrderItem();
                    a.ID = cart.Items[i].Value.ID;
                    a.ProductId = cart.Items[i].Value.ProductId;
                    a.Name = cart.Items[i].Value.Name;
                    a.Price = cart.Items[i].Value.Price;
                    a.Amount = 1;
                    a.Amount += cart.Items[i].Value.Amount;
                    a.TotalPrice = a.Price * a.Amount;
                    a.TotalPrice = cart.Items[i].Value.TotalPrice + a.TotalPrice;
                    cart.Items.Remove(cart.Items[i]);
                    cart.Items.Add(a);
                    cart.TotalPrice += a.TotalPrice;
                    return cart;
                }
            }


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
            for (int i = 0; i < cart.Items.Count; i++)
            {
                if(cart.Items[i] == null)
                {
                    continue;
                }
                int more = newAmount - cart.Items[i].Value.Amount;
                // the product is found in cart, the amount is bigger than 0 and different from curr amount:
                if ((product.ID == cart.Items[i].Value.ID) && (cart.Items[i].Value.Amount != newAmount) && (newAmount > 0))
                {

                    BO.OrderItem a = new BO.OrderItem();
                    a.Price = product.Price;
                    a.ProductId = product.ID;
                    a.ID = cart.Items[i].Value.ID;
                    a.Name = product.Name;
                    a.Amount = newAmount;
                    a.TotalPrice = a.Amount * a.Price;
                    cart.TotalPrice -= cart.Items[i].Value.TotalPrice;
                    cart.Items.RemoveAt(i);
                    cart.Items.Add(a);
                    cart.TotalPrice += a.TotalPrice;
                    return cart;
                }
                else if ((product.ID == cart.Items[i].Value.ID) && (newAmount == 0))
                {
                    cart.TotalPrice -= cart.Items[i].Value.TotalPrice;
                    cart.Items.RemoveAt(i);

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

        void Icart.Confirm(BO.Cart cart)
        {
            cartValidation(cart);
            for (int i = 0; i < cart.Items.Count; i++)
            {
                if (cart.Items[i].HasValue)
                {
                    checkCart(cart.Items[i].Value);

                }
            }
            DalFacade.DO.Order order = new DalFacade.DO.Order();
            order.OrderDate = DateTime.Now;
            order.CustumerAdress = cart.CustomerAddress;
            order.CustumerAdress = cart.CustomerEmail;
            order.CustumerName = cart.CustomerName;
            int id = Dal.Order.add(order);
            DalFacade.DO.OrderItem orderItem = new DalFacade.DO.OrderItem();
            for (int i =0;i< cart.Items.Count; i++)
            {
                if (!cart.Items[i].HasValue)
                {
                    continue;

                }
                orderItem.ProductId = cart.Items[i].Value.ProductId;
                orderItem.Amount = cart.Items[i].Value.Amount;
                orderItem.Price = cart.Items[i].Value.Price;
                orderItem.OrderId = id;
                orderItem.ID = 0;
                Dal.OrderItem.add(orderItem);
                
                DalFacade.DO.Product product = Dal.Product.get(cart.Items[i].Value.ProductId);

                product.InStock = product.InStock - orderItem.Amount;
                if (product.InStock == 0)
                {
                    Dal.Product.delete(product.ID);
                    return;
                }
                Dal.Product.update(product);
            }
        }
    }

}
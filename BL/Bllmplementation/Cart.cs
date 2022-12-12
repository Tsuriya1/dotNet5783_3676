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

namespace Bllmplementation
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
                //TODO throw exeption
                if (product.InStock < 1)
                {
                    //not in stock
                    return cart;
                }

                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i].ProductId == Id)
                    {
                        BO.OrderItem a = new BO.OrderItem();
                        a.ID = cart.Items[i].ID;
                        a.ProductId = cart.Items[i].ProductId;
                        a.Name = cart.Items[i].Name;
                        a.Price = cart.Items[i].Price;                       
                        a.Amount = 1;
                        a.Amount += cart.Items[i].Amount;
                        a.TotalPrice = a.Price * a.Amount;
                        a.TotalPrice=cart.Items[i].TotalPrice+a.TotalPrice;
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
                ToAdd.ID=0;
                cart.Items.Add(ToAdd);
                cart.TotalPrice+=ToAdd.Price;
                return cart;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return cart;
            }
        }
        BO.Cart Icart.Update (BO.Cart cart, int ProductId, int newAmount)
        {
            DalFacade.DO.Product product;
            try
            {
                product = Dal.Product.get(ProductId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return cart;
            }
            if (product.InStock < newAmount)
            {
                throw new Exception("The asked amount is out of stock");
            }
            for (int i = 0; i < cart.Items.Count; i++)
            {
                int more = newAmount - cart.Items[i].Amount;
                // the product is found in cart, the amount is bigger than 0 and different from curr amount:
                if ((product.ID == cart.Items[i].ID) && (cart.Items[i].Amount != newAmount) && (newAmount > 0))
                {

                    BO.OrderItem a = new BO.OrderItem();
                    a.Price = product.Price;
                    a.ProductId = product.ID;
                    a.ID = cart.Items[i].ID;
                    a.Name = product.Name;
                    a.Amount = newAmount;
                    a.TotalPrice = a.Amount * a.Price;
                    cart.TotalPrice -= cart.Items[i].TotalPrice;
                    cart.Items.RemoveAt(i);
                    cart.Items.Add(a);
                    cart.TotalPrice += a.TotalPrice;
                    return cart;
                }
                else if ((product.ID == cart.Items[i].ID) && (newAmount == 0))
                {
                    cart.TotalPrice -= cart.Items[i].TotalPrice;
                    cart.Items.RemoveAt(i);

                    return cart;
                }
            }
            throw new Exception("order item not found");


        }
        void Confirm(BO.Cart cart)
        {
              
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
                throw new Exception("not a valid Customer Name");
            }
            if(cart.CustomerAddress == null)
            {
                throw new Exception("not a valid Customer address");
            }
            if (!IsValidEmail(cart.CustomerEmail))
            {
                throw new Exception("not a valid Customer Email address");
            }
        }
        private void checkCart(BO.OrderItem item)
        {
            DalFacade.DO.Product product;
           
            product = Dal.Product.get(item.ProductId);
            if (item.Amount <= 0)
            {
                throw new Exception("items amount is less than zero");
            }
            if (product.InStock < item.Amount)
            {
                throw new Exception("Not enough in stock");
            }

        }

        void Icart.Confirm(BO.Cart cart)
        {
            cartValidation(cart);
            for (int i = 0; i < cart.Items.Count; i++)
            {
                checkCart(cart.Items[i]);
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
                orderItem.ProductId = cart.Items[i].ProductId;
                orderItem.Amount = cart.Items[i].Amount;
                orderItem.Price = cart.Items[i].Price;
                orderItem.OrderId = id;
                orderItem.ID = 0;
                Dal.OrderItem.add(orderItem);
                
                DalFacade.DO.Product product = Dal.Product.get(cart.Items[i].ProductId);

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
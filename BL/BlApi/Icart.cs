using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface Icart
    {
        
       public Cart AddProduct(Cart cart, int Id);
       public Cart Update(Cart cart, int ProductId, int newAmount);
       public void Confirm(Cart cart);


    }
}

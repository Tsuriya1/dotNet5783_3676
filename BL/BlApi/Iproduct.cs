using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    internal interface Iproduct
    {
        public List<ProductForList> GetProducts ();
        public Product getProductsDetails(int ID);
        public ProductItem getProductsDetails(int ID,Cart cart);
        public void addProduct(Product product);
        public void removeProduct(Product product);
        public void updateData(Product product);

    }
}

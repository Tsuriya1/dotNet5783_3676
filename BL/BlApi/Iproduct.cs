﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface Iproduct
    {
        public IEnumerable<ProductForList?> GetProductsByCategory(Category category); 
        public IEnumerable<ProductForList?> GetProducts ();
        public Product getProductsDetails(int ID);
        public ProductItem getProductsDetails(int ID,Cart cart);
        public void addProduct(Product product);
        public void removeProduct(int productId);
        public void updateData(Product product);
        public IEnumerable<ProductItem> getCatalog();
    }
}

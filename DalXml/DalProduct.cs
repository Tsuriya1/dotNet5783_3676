using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Dal
{

    internal class DalProduct : Iproduct
    {
        const string entity_name = @"Products";
        // addes a new product to the xml tree
        private XElement create_product_xelement(DalFacade.DO.Product product)
        {
            return new XElement("Product",
                                   new XElement("ID", product.ID),
                                   new XElement("Name", product.Name),
                                   new XElement("Price", product.Price),
                                   new XElement("Category", product.Category),
                                   new XElement("InStock", product.InStock)
                                   );
        }
        // exports it as a product (from XElement)
        private DalFacade.DO.Product convert_xelement_to_product(XElement prod)
        {

            DalFacade.DO.Product product = new DalFacade.DO.Product()
            {
                ID = prod.ToIntNullable("ID").Value,
                Name = prod.Element("Name").Value,
                Category = prod.ToEnumNullable<DalFacade.DO.Category>(("Category")),
                Price = prod.ToDoubleNullable("Price").Value,
                InStock = prod.ToIntNullable("InStock").Value
            };
            return product;
        }


        // add to the tree
        public int add(DalFacade.DO.Product product)
        {
            XElement root = XMLTools.LoadListFromXMLElement(entity_name);
            int count = (from prod in root.Elements()
             where prod.ToIntNullable("ID").Value == product.ID
             select prod).Count();
            
            if (count > 0)
                // already exists
            {
                throw new DalFacade.DO.DuplicateException("product ID already exists");
            }
            
            root.Add(create_product_xelement(product));
            //save changes
            XMLTools.SaveListToXMLElement(root,entity_name);
            return product.ID;
        }

        // get by ID number
        public DalFacade.DO.Product get(int ID)
        {

            XElement root = XMLTools.LoadListFromXMLElement(entity_name);
            var newList = (from prod in root.Elements()
                         where prod.ToIntNullable("ID").Value == ID
                         select prod);
            if (newList.Count() > 0)
            {
                return convert_xelement_to_product(newList.First());
            }
            
            throw new DalFacade.DO.NotFoundException("product not found");
        }
        public Product getObject(Func<Product, bool>? func)
        {
            if (func == null)
            {
                throw new DalFacade.DO.NotFoundException("no filter parameter is given");
            }
            XElement root = XMLTools.LoadListFromXMLElement(entity_name);
            var newList = (from prod in root.Elements()
                           where func(convert_xelement_to_product(prod))
                           select prod);
            //var newList = Dal.DataSource.productList.Where(x => x.HasValue && func(x.Value));
            if (newList != null && newList.Count() > 0)
            {
                return convert_xelement_to_product(newList.First());
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
            XElement root = XMLTools.LoadListFromXMLElement(entity_name);
            // no parameter is given
            if (func == null)
            {
                products = (from prod in root.Elements()
                            select convert_xelement_to_product(prod));


                return products;
            }
            //products = Dal.DataSource.productList.Where(x => x.HasValue && func(x.Value)).Select(x => x.Value);
            products = (from prod in root.Elements()
                           where func(convert_xelement_to_product(prod))
                           select convert_xelement_to_product(prod));

            return products;

        }

        public void delete(int ID)
        {
            XElement root = XMLTools.LoadListFromXMLElement(entity_name);
            var product = (from prod in root.Elements()
                         where prod.ToIntNullable("ID").Value == ID
                         select prod);
            //int count = Dal.DataSource.productList.RemoveAll(x => (x.HasValue && x.Value.ID == ID));
            if (!product.Any())
            {
                throw new DalFacade.DO.NotFoundException("product not found");
            }
            product.First().Remove();
            XMLTools.SaveListToXMLElement(root, entity_name);


        }

        public void update(DalFacade.DO.Product product)
        {
            XElement root = XMLTools.LoadListFromXMLElement(entity_name);
            var products = (from prod in root.Elements()
                           where prod.ToIntNullable("ID").Value == product.ID
                           select prod);
/*
            var products = from prod in DataSource.productList
                           where ((prod.HasValue) && (prod.Value.ID == product.ID))
                           select prod.Value;*/
            if (products != null && products.Count() > 0)
            {
                delete(product.ID);
                root = XMLTools.LoadListFromXMLElement(entity_name);
                root.Add(create_product_xelement(product));
                XMLTools.SaveListToXMLElement(root, entity_name);

                // Dal.DataSource.productList.Add(product);
                return;
            }

            
            throw new DalFacade.DO.NotFoundException("product not found");
        }


    }
}

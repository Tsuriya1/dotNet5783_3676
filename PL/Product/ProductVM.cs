using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using DalFacade.DO;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace PL.Product
{
    public class ProductVM : INotifyPropertyChanged

    {
        private BlApi.IBl? bl = BlApi.Factory.get();
        public CollectionViewSource productsCollectionFilter;
        private BO.Category category;
        public System.Array Categories => Enum.GetValues(typeof(BO.Category));
        private bool allCat = false;

        private ObservableCollection<ProductForList> products;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ProductForList> Products
        {
            get { return products; }
            set { Set(ref products, value);}
        }

        public BO.Category Category
        {
            get => category;
            set
            {
                Set(ref category, value);
                ProductsCollectionFilter.View.Refresh();
            }
        }


        public ProductVM()
        {
            allCat = true;
            products = new ObservableCollection<BO.ProductForList>(bl.Product.GetProducts().Where(x => x.HasValue).Select(x => x.Value));
            productsCollectionFilter = new();
            productsCollectionFilter.Source = products;
            productsCollectionFilter.Filter += new FilterEventHandler(categoryFilter);
            allCat = false;

        }

        public void categoryFilter(object sender, FilterEventArgs e)
        {


            if (allCat)
            {
                e.Accepted = true;
                return;
            }


            else if (e.Item is BO.ProductForList product)
                e.Accepted = product.Category == Category;
            else
                e.Accepted = true;
        }





        public CollectionViewSource ProductsCollectionFilter
        {
            get { return productsCollectionFilter; }
            set { Set(ref productsCollectionFilter, value); }
        }

        private void Set<T>(ref T prop, T val, [CallerMemberName] string? name = "")
        {
            if (!prop.Equals(val))
            {
                prop = val;
                PropertyChanged?.Invoke(this, new(name));
            }
        }

       

        /*private void ProductListReview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void add_Button_Click(object sender, RoutedEventArgs e)
        {
            new Product.AddProductWindow().Show();
            Close();

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category choice = (BO.Category)CategorySelector.SelectedItem;
            IEnumerable<ProductForList?> list = bl.Product.GetProductsByCategory(choice);
            List<ProductForList> listWithoutNull = new List<ProductForList>();
            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).HasValue)
                {
                    listWithoutNull.Add(list.ElementAt(i).Value);
                }
            }
            ProductListReview.ItemsSource = listWithoutNull;

        }

        private void ProductListReview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductListReview.SelectedItems.Count == 1)
            {//display the text of selected item

                BO.ProductForList selectedProduct = (BO.ProductForList)ProductListReview.SelectedItems[0];
                int id = selectedProduct.ID;
                new Product.UpdateAndActionsWindow(id).Show();
                Close();
                //ProductListReview.ItemsSource = bl.Product.GetProducts();


            }

        }

    }*/
    }
}


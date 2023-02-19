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

// to be able binding data with xaml files and windows its necessary having an instance of the struct we asking to bind.
// all Vm classes contains as properties the data we are binding
namespace PL.Product
{


public class RelayCommand<T>:ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<T> excecute;
        private object update_prod;


        public RelayCommand(Action<T> func)
        {
            this.excecute = func;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter == null)
            {
                return;
            }
            excecute((T)parameter);
        }
    }





    public class ProductVM : INotifyPropertyChanged

    {
        private BlApi.IBl? bl = BlApi.Factory.get();
        public CollectionViewSource productsCollectionFilter;
        public CollectionViewSource productsItemCollectionFilter;
        private BO.Category category;
        private BO.Category category_update;

        public System.Array Categories => Enum.GetValues(typeof(BO.Category));
        public System.Array Categories_update=> convert_cat();

        private bool allCat = false;
        public ICommand update => new RelayCommand<int>(update_prod);
        public ICommand viewItem => new RelayCommand<int>(viewItemFunc);


        private ObservableCollection<ProductForList> products;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ProductForList> Products
        {
            get { return products; }
            set { Set(ref products, value); }
        }

        private Array convert_cat()
        {
            List<BO.Category> temp = new List<BO.Category>();
            foreach (var x in Categories)
            {
                if ((BO.Category)x == BO.Category.All_Types)
                {
                    continue;
                }
                temp.Add((BO.Category)x);
            }
            return temp.ToArray();
        }

        public BO.Category Category
        {
            get => category;
            set
            {
                Set(ref category, value);
                ProductsCollectionFilter.View.Refresh();
                ProductsItemCollectionFilter.View.Refresh();
            }
        }

        public BO.Category Category_update
        {
            get => category_update;
            set
            {
                Set(ref category_update, value);
            }
        }


        private void update_prod(int ID)
        {
            new Product.UpdateAndActionsWindow(ID,this).Show();
        }

        private void viewItemFunc(int itemID)
        {
            productItem = bl.Product.getProductsDetails(itemID,myCart);
            new Product.ProductItemWindow(this).Show();
        }
        
        public ProductVM()
        {
            
            products = new ObservableCollection<BO.ProductForList>(bl.Product.GetProducts().Where(x => x.HasValue).Select(x => x.Value));
            productsCollectionFilter = new();
            productsCollectionFilter.Source = products;
            productsItemCollectionFilter = new();
            productsItemCollectionFilter.Source = new ObservableCollection<BO.ProductItem>(bl.Product.getCatalog());
            category = BO.Category.All_Types;
            productsItemCollectionFilter.Filter+= new FilterEventHandler(categoryFilter);
            productsCollectionFilter.Filter += new FilterEventHandler(categoryFilter);
        }

        public void categoryFilter(object sender, FilterEventArgs e)
        {
            BO.Category categoryToCheck; 
            if (e.Item is BO.ProductForList product)
            {
                categoryToCheck = product.Category.Value;
            }else if(e.Item is BO.ProductItem item)
            {
                categoryToCheck = item.Category.Value;
            }
            else
            {
                e.Accepted = true;
                return;
            }

            if (category == BO.Category.All_Types)
            {
                e.Accepted = true;
                return;
            }
            if (categoryToCheck == category)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
            return;


        }



        public CollectionViewSource ProductsItemCollectionFilter
        {
            get { return productsItemCollectionFilter; }
            set { Set(ref productsItemCollectionFilter, value); }
        }

        public CollectionViewSource ProductsCollectionFilter
        {
            get { return productsCollectionFilter; }
            set { Set(ref productsCollectionFilter, value); }
        }

        private string? name;
        public string? Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { Set(ref price, value); }
        }

        private int in_stock;
        public int In_stock
        {
            get { return in_stock; }
            set { Set(ref in_stock, value); }
        }
        
        private int id;
        public int ID
        {
            get { return id; }
            set { Set(ref id, value); }
        }

        private ProductItem productItem;
        public ProductItem ProductItem
        {
            get { return productItem; }
            set { Set(ref productItem, value); }
        }

        private BO.Cart myCart;
        public BO.Cart Mycart
        {
            get { return myCart; }
            set { Set(ref myCart, value); }
        }

        private void Set<T>(ref T prop, T val, [CallerMemberName] string? name = "")
        {
            if(prop == null)
            {
                prop = val;
                PropertyChanged?.Invoke(this, new(name));
                return;
            }
            if (!prop.Equals(val))
            {
                prop = val;
                PropertyChanged?.Invoke(this, new(name));
            }
        }

    }
}


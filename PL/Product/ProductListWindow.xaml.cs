//using BlApi;
using BO;
using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.get();
        private ProductVM productVM;
        public ProductListWindow()
        {
            InitializeComponent();
            productVM = new ProductVM();
            this.DataContext = productVM;
            
        }
        private void add_Button_Click(object sender, RoutedEventArgs e) { 
            new Product.AddProductWindow(productVM).Show();
        }
    }
}

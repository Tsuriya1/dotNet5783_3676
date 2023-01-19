using PL.Order;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        ProductVM vm; 
        private BlApi.IBl? bl = BlApi.Factory.get();

        public ProductItemWindow(ProductVM vm1)
        {
            InitializeComponent();
            vm = vm1;
            DataContext = vm;
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.AddProduct(vm.Mycart, vm.ProductItem.ID);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }

}

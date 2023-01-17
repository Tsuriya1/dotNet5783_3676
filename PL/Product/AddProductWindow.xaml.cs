//using BlApi;
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
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
       
        private BlApi.IBl? bl = BlApi.Factory.get();
        //private IBl bl = new Bl();
        ProductVM vm;
        public AddProductWindow(ProductVM productVM)
        {
            InitializeComponent();
            this.vm = productVM;
            this.DataContext = this.vm;
            this.vm.ID = 0;
            this.vm.Name = "";
            this.vm.Price = 0;
            this.vm.In_stock = 0;
        }
        
        private void Add_Button_Click(object sender, RoutedEventArgs e) 
        {
            BO.Product product = new BO.Product();
            product.Category = this.vm.Category_update;
            product.ID = this.vm.ID;
            product.Name = this.vm.Name;
            product.Price = this.vm.Price;
            product.InStock = this.vm.In_stock;
            try
            {
                bl.Product.addProduct(product);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            ObservableCollection<BO.ProductForList> products = new ObservableCollection<BO.ProductForList>(bl.Product.GetProducts().Where(x => x.HasValue).Select(x => x.Value));
            vm.ProductsCollectionFilter.Source = products;
            Close();    

        }
    }
}

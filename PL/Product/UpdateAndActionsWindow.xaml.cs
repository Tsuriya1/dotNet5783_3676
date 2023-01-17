using BlApi;
using BO;
using Dal;
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
    /// Interaction logic for UpdateAndActionsWindow.xaml
    /// </summary>

    public partial class UpdateAndActionsWindow : Window
    {
        private IDal Dal = DalApi.Factory.Get();
        BO.Product product = new BO.Product();
        ProductVM vm ;

    

        

        private IBl bl = BlApi.Factory.get();
        public UpdateAndActionsWindow()
        {
            InitializeComponent();
        }

        public UpdateAndActionsWindow(int ProductId,ProductVM productVM)
        {
            InitializeComponent();
            this.vm = productVM;
            this.DataContext = this.vm;
            try
            {
                product = bl.Product.getProductsDetails(ProductId);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            this.vm.Category_update = product.Category.Value;
            this.vm.ID = product.ID;
            this.vm.Name = product.Name;
            this.vm.Price = product.Price;
            this.vm.In_stock = product.InStock;
            

        }


        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            product.Category = this.vm.Category_update;
            product.ID = this.vm.ID;
            product.Name = this.vm.Name;
            product.Price = this.vm.Price;
            product.InStock = this.vm.In_stock;
            try
            {
                bl.Product.updateData(product);

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

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

        private int convertIntPos(string num)
        {
            int ret;
            bool success = int.TryParse(num, out ret);
            if (!success)
            {
                return -1;
            }
            return ret;
        }
        private double convertDoublePos(string num)
        {
            double ret;
            bool success = double.TryParse(num, out ret);
            if (!success)
            {
                Console.WriteLine("not a number");
                return -1;
            }
            return ret;
        }

        

        private IBl bl = BlApi.Factory.get();
        public UpdateAndActionsWindow()
        {
            InitializeComponent();
        }

        public UpdateAndActionsWindow(int ProductId)
        {
            InitializeComponent();
            try
            {
                product = bl.Product.getProductsDetails(ProductId);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                new Product.ProductListWindow().Show();
                Close();
            }
            
            Id.Text = product.ID.ToString();
            Name.Text = product.Name;
            Price.Text = product.Price.ToString();
            In_Stock.Text = product.InStock.ToString();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategorySelector.SelectedItem = product.Category;

        }

        
        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            product.Category = (BO.Category)CategorySelector.SelectedItem;
            product.ID = convertIntPos(Id.Text);
            product.Name = Name.Text;
            product.Price = convertDoublePos(Price.Text);
            product.InStock = convertIntPos(In_Stock.Text);
            try
            {
                bl.Product.updateData(product);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            new Product.ProductListWindow().Show();
            Close();
        }
    }
}

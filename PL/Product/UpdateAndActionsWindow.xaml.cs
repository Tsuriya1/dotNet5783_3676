using BlApi;
using BO;
using Dal;
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
    /// Interaction logic for UpdateAndActionsWindow.xaml
    /// </summary>
    public partial class UpdateAndActionsWindow : Window
    {
        private IDal Dal = new DalList();
        BO.Product product = new BO.Product();

        private IBl bl = new Bl();
        public UpdateAndActionsWindow()
        {
            InitializeComponent();
        }
        public UpdateAndActionsWindow(int ProductId)
        {
            InitializeComponent();
            product = bl.Product.getProductsDetails(ProductId);
            Id.Text = product.ID.ToString();

        }
        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

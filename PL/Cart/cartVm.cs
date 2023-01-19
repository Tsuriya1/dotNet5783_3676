using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace PL.Cart
{
    public class RelayCommand<T> : ICommand
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




    public class cartVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<BO.OrderItem?> orderItems;
        public ObservableCollection<BO.OrderItem?> OrderItems
        {
            get { return orderItems; }
            set { Set(ref orderItems, value); }
        }
        
        public cartVm(BO.Cart cart)
        {
            Cart = cart;
            if (cart.Items == null)
            {

                this.orderItems = new ObservableCollection<BO.OrderItem?>();
            }
            else
            {
                this.orderItems = new ObservableCollection<BO.OrderItem?>(cart.Items);
            }
        }

        private BO.Cart cart;
        public BO.Cart Cart
        {
            get { return cart; }
            set { Set(ref cart, value); }
        }

        private string? customerName;
        public string? CustomerName
        {
            get { return customerName; }
            set { Set(ref customerName, value); }
        }

        private string? customerAddress;
        public string? CustomerAddress
        {
            get { return customerAddress; }
            set { Set(ref customerAddress, value); }
        }
        private string? customerEmail;
        public string? CustomerEmail
        {
            get { return customerEmail; }
            set { Set(ref customerEmail, value); }
        }
        

        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { Set(ref productId, value); }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { Set(ref amount, value); }
        }
        private double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set { Set(ref totalPrice, value); }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { Set(ref price, value); }
        }
        private BO.OrderItem? selectedItem;
        public BO.OrderItem? SelectedItem
        {
            get { return selectedItem; }
            set { Set(ref selectedItem, value); }
        }
        private void Set<T>(ref T prop, T val, [CallerMemberName] string? name = "")
        {
            if (prop == null)
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

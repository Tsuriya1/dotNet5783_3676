using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PL.Order
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

    public class OrderVM : INotifyPropertyChanged
    {
        //properties:

        public ObservableCollection<BO.OrderForList> orders;
        private BlApi.IBl? bl = BlApi.Factory.get();

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand update => new RelayCommand<int>(update_order);

        private BO.OrderForList order;
        public BO.OrderForList Order
        {
            get { return order; }
            set { Set(ref order, value); }
        }
        private string stringItem;
        public string StringItem
        {
            get { return stringItem; }
            set { Set(ref stringItem, value); }
        }

        private BO.Order bo_order;
        public BO.Order BO_Order
        {
            get { return bo_order; }
            set { Set(ref bo_order, value); }
        }

        private BO.OrderStatus status;
        public BO.OrderStatus Status
        {
            get { return status; }
            set { Set(ref status, value); }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set { Set(ref id, value); }
        }
        
        private ObservableCollection<Tuple<DateTime?, string?>?> orderTrackingDesciption;
        public ObservableCollection<Tuple<DateTime?, string?>?> OrderTrackingDesciption
        {
            get { return orderTrackingDesciption; }
            set { Set(ref orderTrackingDesciption, value); }
        }

        public ObservableCollection<BO.OrderForList> Orders
        {
                get { return orders; }
                set { Set(ref orders, value); }
        }


        // ctor
        public OrderVM()
        {
            IEnumerable<BO.OrderForList?> temp_list;
            try
            {
                temp_list = bl.Order.GetOrder();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                temp_list = new List<BO.OrderForList?>();
            }
            orders = new(bl.Order.GetOrder().Where(x=>x.HasValue).Select(x=>x.Value));
             
        }
        private void update_order(int ID)
        {
            new Order.UpdateAndActionsWindow(ID, this).Show();
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

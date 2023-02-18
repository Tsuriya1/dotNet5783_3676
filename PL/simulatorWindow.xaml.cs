using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for simulatorWindow.xaml
    /// </summary>
    public partial class simulatorWindow : Window
    {
        BackgroundWorker clock_worker;
        private int update_Percentage = 1;
        private int clock_Percentage = 0;
        private bool sim_windows_open = true;
        private Stopwatch simulator_watch = new Stopwatch();


        public simulatorWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            
            clock_worker = new BackgroundWorker();
            clock_worker.DoWork += Worker_DoWork;
            clock_worker.ProgressChanged += Worker_ProgressChanged;
            //clock_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            clock_worker.WorkerReportsProgress = true;
            clock_worker.WorkerSupportsCancellation = true;
            clock_worker.RunWorkerAsync();
            Simulator.Simulator.activate();

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.registerUpdateDate(update_handler);
            Simulator.Simulator.registerSimCompleted(task_complete_sim_handler);
            simulator_watch.Start();
            while (sim_windows_open)
            {
                clock_worker.ReportProgress(clock_Percentage, null);
                Thread.Sleep(1000);
            }

        }

        private void do_update(Tuple<BO.Order?, DateTime, int>?  info)
        {
            if (info == null || info.Item1==null)
            {
                ID = 0;
                CurrentStatus = null;
                NextStatus = null;
                StartingTime = null;
                EndTime = null;
                return;
            }

            ID = info.Item1.Value.ID;
            CurrentStatus = info.Item1.Value.Status;
            EndTime = info.Item2;
            if (CurrentStatus == BO.OrderStatus.Confirmed)
            {
                StartingTime = info.Item1.Value.OrderDate;

                NextStatus = BO.OrderStatus.sent;
            }
            else if (CurrentStatus == BO.OrderStatus.sent)
            {
                StartingTime = info.Item1.Value.ShipDate;
                NextStatus = BO.OrderStatus.provided;
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            int progress = e.ProgressPercentage;
            if (progress == update_Percentage)
            {
                Tuple<BO.Order?, DateTime, int>? info = e.UserState as Tuple<BO.Order?, DateTime, int>;
                do_update(info);
            }
            else if(progress == clock_Percentage)
            {
                clock = simulator_watch.Elapsed.ToString("c");
            }
        }


        private void update_handler(BO.Order? order, DateTime date, int delay)
        {
            Tuple< BO.Order?, DateTime, int> info = new Tuple<BO.Order?, DateTime, int>(order, date, delay);
            clock_worker.ReportProgress(update_Percentage, info);
        }

        private void task_complete_handler()
        {
            
        }

        private void task_complete_sim_handler()
        {
            clock_worker.CancelAsync();
        }

        public int? ID
        {
            get { return (int?)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int?), typeof(simulatorWindow), new PropertyMetadata(null));





        public BO.OrderStatus? CurrentStatus
        {
            get { return (BO.OrderStatus?)GetValue(CurrentStatusProperty); }
            set { SetValue(CurrentStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentStatusProperty =
            DependencyProperty.Register("CurrentStatus", typeof(BO.OrderStatus?), typeof(simulatorWindow), new PropertyMetadata(null));




        public DateTime? StartingTime
        {
            get { return (DateTime?)GetValue(StartingTimeProperty); }
            set { SetValue(StartingTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartingTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingTimeProperty =
            DependencyProperty.Register("StartingTime", typeof(DateTime?), typeof(simulatorWindow), new PropertyMetadata(null));



        public DateTime? EndTime
        {
            get { return (DateTime?)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(DateTime?), typeof(simulatorWindow), new PropertyMetadata(null));



        public string clock
        {
            get { return (string)GetValue(clockProperty); }
            set { SetValue(clockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for clock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty clockProperty =
            DependencyProperty.Register("clock", typeof(string), typeof(simulatorWindow), new PropertyMetadata(""));



        public BO.OrderStatus? NextStatus
        {
            get { return (BO.OrderStatus?)GetValue(NextStatusProperty); }
            set { SetValue(NextStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NextStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextStatusProperty =
            DependencyProperty.Register("NextStatus", typeof(BO.OrderStatus?), typeof(simulatorWindow), new PropertyMetadata(null));

        private void Stop_sim_click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.stop_simulation();

            Simulator.Simulator.unRegisterUpdateDate(update_handler);
            //Simulator.Simulator.unRegisterreportSimCompleted(task_complete_sim_handler);
            if (sim_windows_open==true)
            {
                simulator_watch.Stop();
                sim_windows_open = false;

            }
            Close();
        }
    }





}

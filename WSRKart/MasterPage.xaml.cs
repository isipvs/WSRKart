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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для Master.xaml
    /// </summary>
    public partial class MasterPage : Page
    {

        public MasterPage()
        {
            InitializeComponent();

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Tick;
            dt.Start();
        }

        private DateTime dtNewYear = new DateTime(2022, 12, 31, 0, 0, 0);

        private void Tick(object sender, EventArgs e) 
        {
            DateTime dtNow = DateTime.Now;
            int m = dtNewYear.Month - dtNow.Month;
            DateTime dtNow2 = dtNow.AddMonths(m);
            TimeSpan _time = dtNewYear.Subtract(dtNow2);
            string info = string.Format( "{0} месяцев {1} дней {2} часов {3} минут {4} секунд", m, _time.Days, _time.Hours, _time.Minutes, _time.Seconds);
            MasterTimer.Content = info;
        }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MasterPage());
        }
    }
}

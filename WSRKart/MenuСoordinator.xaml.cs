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

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для СoordinatorMenu.xaml
    /// </summary>
    public partial class СoordinatorMenu : Page
    {
        public СoordinatorMenu()
        {
            InitializeComponent();
        }

        private void OnRacerc(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RacerResult());
        }

        private void OnSponsor(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SponsorList());
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuRacer());
        }
    }
}

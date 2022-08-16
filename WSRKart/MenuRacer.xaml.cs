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
    /// Логика взаимодействия для MenuRacer.xaml
    /// </summary>
    public partial class MenuRacer : Page
    {
        public MenuRacer()
        {
            InitializeComponent();
        }

        private void OnRegRace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegRace());
        }

        private void OnResultRace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RacerResult());
        }

        private void OnRegProfRace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AutoRacer(true));
        }

        private void OnRacerSponsor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RacerSponsor());
        }

        private void OnСontact_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Телефон: +7 999 999 99 99 \nEmail: yugkyug@kartskills.org ", "Контакты", MessageBoxButton.OK);
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}

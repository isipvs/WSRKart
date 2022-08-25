using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WSRKart.DataSetTableAdapters;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для RegRace.xaml
    /// </summary>
    public partial class RegRace : Page
    {
        private int summ = 0;
        DataSet dataSet = new DataSet();
        Charity_Adapter ch_adapter = new Charity_Adapter();

        public RegRace()
        {
            InitializeComponent();
            ch_adapter.Fill(dataSet.Charity);
            NFund.ItemsSource = dataSet.Charity.DefaultView;
            NFund.DisplayMemberPath = "Charity_Name";
            NFund.SelectedValuePath = "ID_Сharity";
            NFund.SelectedIndex = 0;
        }

        private void Reg(object sender, RoutedEventArgs e)
        {
            try
            {

                Control[] cl = new Control[] { NFund, Donation};

                foreach (Control c in cl)
                {
                    UITools.ChekReq(c);
                }

                CheckBox[] ch = new CheckBox[] { NRaceFist, NRaceSecond, NRaceThird};

                bool b = false;

                foreach(CheckBox c in ch)
                {
                    b = ((Boolean)c.IsChecked);
                    if (b)
                        break;
                }
                if (!b)
                    throw new Exception("Необходимо выбрать, хоть одну трассу");

                if(int.Parse(Donation.Text) <= 0)
                    throw new Exception("Необходимо задать взнос");

                Registration_Adapter adapter = new Registration_Adapter();
                
                adapter.Insert_Registration((int)App.Current.Properties["ID_Racer"], DateTime.Now, 1, summ, (int)NFund.SelectedValue, decimal.Parse(Donation.Text) );

                NavigationService.Navigate(new MenuRacer());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        int lastsumm = 0;

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton ch = (ToggleButton)sender;

            if (ch.Tag == null)
                return;

            int v = int.Parse(ch.Tag.ToString());

            if (ch is RadioButton)
            {
                summ -= lastsumm;
                lastsumm = v;
            }

            if ((Boolean)ch.IsChecked)
                summ += v;
            else
                summ -= v;

            RefVzn.Content = "$ " + summ;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void NUM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            UITools.Num_PreviewTextInput(e);
        }
    }
}

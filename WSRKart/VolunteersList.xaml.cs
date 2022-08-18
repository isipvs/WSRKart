using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using WSRKart.DataSetTableAdapters;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для VolunteersList.xaml
    /// </summary>
    public partial class VolunteersList : Page
    {
        DataSet dataSet = new DataSet();
        Volunteer_Adapter adapter;

        public VolunteersList()
        {
            InitializeComponent();
            adapter = new Volunteer_Adapter();

/*
            CbVolunteer.ItemsSource = dataSet.Volunteer.DefaultView;
            CbVolunteer.DisplayMemberPath = "Event_Name";
            CbVolunteer.SelectedValuePath = "ID_Volunteer";
            CbVolunteer.SelectedIndex = 0;
*/
            VolunteersGrid.ItemsSource = dataSet.Volunteer.DefaultView;
            VolunteersGrid.SelectionMode = DataGridSelectionMode.Single;
            VolunteersGrid.SelectedValuePath = "ID_Volunteer";
            VolunteersGrid.CanUserAddRows = false;
            VolunteersGrid.CanUserDeleteRows = false;

            OnRefresh(null, null);
        }

        private void OnAddVolunteers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddVolunteers());
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            adapter.Fill(dataSet.Volunteer);
        }
    }
}

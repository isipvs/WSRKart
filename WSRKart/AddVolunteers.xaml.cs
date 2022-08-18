using Microsoft.Win32;
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
    /// Логика взаимодействия для AddVolunteers.xaml
    /// </summary>
    public partial class AddVolunteers : Page
    {
        public AddVolunteers()
        {
            InitializeComponent();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if( openFileDialog.ShowDialog() == true )
                    txtFileName.Text = openFileDialog.FileName;

            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void LoadFile(string FileName)
        {
            Volunteer_Adapter a = new Volunteer_Adapter();

            SqlCommand sqlBulk = new SqlCommand();
            sqlBulk.CommandText = "BULK INSERT Volunteer FROM '" + FileName + "' WITH( FORMAT = 'CSV', FIRSTROW = 2)";
            sqlBulk.CommandType = System.Data.CommandType.Text;
            sqlBulk.Connection = a.Connection;

            if( sqlBulk.Connection.State != System.Data.ConnectionState.Open)
                sqlBulk.Connection.Open();

            sqlBulk.ExecuteNonQuery();

            sqlBulk.Connection.Close();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadFile(txtFileName.Text);
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}

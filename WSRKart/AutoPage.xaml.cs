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
    /// Логика взаимодействия для AutoRacer.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            try
            {
                User_Adapter ua = new User_Adapter();

                SqlCommand sqlSelect = new SqlCommand();
                sqlSelect.CommandText = "select ID_User, ID_Role, ( select ID_Racer from racer r where r.ID_User = u.ID_user ) from [user] u where email = @email and password=@password";
                sqlSelect.CommandType = System.Data.CommandType.Text;
                sqlSelect.Parameters.AddWithValue("email", Email.Text);
                sqlSelect.Parameters.AddWithValue("password", Password.Password.ToString());
                sqlSelect.Connection = ua.Connection;

                if (sqlSelect.Connection.State != System.Data.ConnectionState.Open)
                    sqlSelect.Connection.Open();

                int[] ret = null;
                int userId = 0, racerId=0;
                char roleId = 'x';
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);
                            roleId = reader.GetString(1)[0];
                            racerId= reader.GetInt32(2);
                        }
                    }

                    sqlSelect.Connection.Close();
                }

                if (userId == 0)
                    throw new Exception("Не верный лоигн или пароль");

                App.Current.Properties["ID_User" ] = userId;
                App.Current.Properties["ID_Role" ] = roleId;
                App.Current.Properties["ID_Racer"] = racerId;

                if ( roleId == 'R' )
                    NavigationService.Navigate(new MenuRacer());
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

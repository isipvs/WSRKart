using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;
using WSRKart.DataSetTableAdapters;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {

        private int userId;
        private User_Adapter adapter = new User_Adapter();

        public AddUser(int id)
        {
            InitializeComponent();
            userId = id;

            if (userId != 0)
            {
                DataSet ds = new DataSet();
                adapter.FillBy_ID(ds.User, userId);

                email.Text    = ds.User.Rows[0]["Email"].ToString();
                pswd.Password = ds.User.Rows[0]["Password"].ToString();
                f_name.Text   = ds.User.Rows[0]["First_Name"].ToString();
                l_name.Text   = ds.User.Rows[0]["Last_Name"].ToString();
                char r = ds.User.Rows[0]["ID_Role"].ToString()[0];
                if (r == 'R')
                {
                    cbRole.Items.Add("Racer");
                    cbRole.SelectedIndex = cbRole.Items.Count - 1;
                    cbRole.IsEnabled = false;
                }
                else
                    cbRole.SelectedValue = ds.User.Rows[0]["ID_Role"].ToString();
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckRequiredFields();

                if( userId == 0 )
                    adapter.Insert_User ( email.Text,
                                          pswd.Password,
                                          f_name.Text,
                                          l_name.Text,
                                          (string)cbRole.SelectedValue );
                else
                    adapter.Update_User(email.Text,
                                          pswd.Password,
                                          f_name.Text,
                                          l_name.Text,
                                          cbRole.IsEnabled ? (string)cbRole.SelectedValue : "R",
                                          userId
                                          );

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void CheckRequiredFields()
        {
            Control[] cl = new Control[] { email, l_name, f_name, pswd, pswd2, cbRole};
            foreach (Control c in cl)
                UITools.ChekReq(c);

            UITools.CheckPairPassword(pswd, pswd2);

            UITools.CheckEmail(email.Text);
        }

    }
}

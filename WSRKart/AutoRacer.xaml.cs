using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WSRKart.DataSetTableAdapters;

namespace WSRKart
{
    /// <summary>
    /// Логика взаимодействия для Racer.xaml
    /// </summary>
    public partial class AutoRacer : Page
    {
        DataSet dataSet;
        Racer_Adapter adapter;

        private bool doEdit;
        public AutoRacer(bool edit)
        {
            InitializeComponent();

            doEdit = edit;

            dataSet = new DataSet();
            adapter = new Racer_Adapter();
            Gender_Adapter gender_Adapter = new Gender_Adapter();
            Country_Adapter country_Adapter = new Country_Adapter();

            gender_Adapter.Fill(dataSet.Gender);
            genderList.ItemsSource = dataSet.Gender.DefaultView;
            genderList.DisplayMemberPath = "Gender_Name";
            genderList.SelectedValuePath = "ID_Gender";
            genderList.SelectedIndex = 0;

            country_Adapter.Fill(dataSet.Country);
            country.ItemsSource = dataSet.Country.DefaultView;
            country.DisplayMemberPath = "Country_Name";
            country.SelectedValuePath = "ID_Country";
            country.SelectedIndex = 77;


            if (doEdit)
            {
                adapter.FillById( dataSet.Racer, (int)App.Current.Properties["ID_Racer"]);

                System.Data.DataRow dataRow = dataSet.Racer.Rows[0];

                email.Text     = dataRow["email" ].ToString();
                l_name.Text    = dataRow["last_name"].ToString();
                f_name.Text    = dataRow["first_name"].ToString();
                pswd.Password  = dataRow["Password"].ToString();
                pswd2.Password = dataRow["Password"].ToString();
                genderList.SelectedValue = dataRow["Gender"].ToString();
                drozd.SelectedDate = (DateTime)dataRow["DateOfBirth"];
                country.SelectedValue = dataRow["ID_Country"].ToString();
            }

            if(edit)
            {
                //NPage.Content = "Редактирование профиля";
                NReg.Content = "Сохранить";
            }

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void OnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckRequiredFields();
                CheckDate();
                CheckPairPassword(pswd, pswd2);
                CheckEmail();

                if (doEdit)
                {
                    adapter.Update_Racer(
                        (int)App.Current.Properties["ID_Racer"],
                        email.Text, l_name.Text, f_name.Text, pswd.Password,
                        (string)genderList.SelectedValue,
                        (DateTime)drozd.SelectedDate,
                        (string)country.SelectedValue
                        );

                    MessageBox.Show("Успешное обновление", "Редактирование", MessageBoxButton.OK);

                    NavigationService.GoBack();
                }
                else
                {
                    int ID_User;
                    int ID_Racer;

                    adapter.Create_Racer(
                        email.Text, l_name.Text, f_name.Text, pswd.Password,
                        (string)genderList.SelectedValue,
                        (DateTime)drozd.SelectedDate,
                        (string)country.SelectedValue,
                        out ID_User,
                        out ID_Racer
                        );

                    App.Current.Properties["ID_Racer"] = ID_Racer;
                    App.Current.Properties["ID_User"] = ID_User;
                    App.Current.Properties["ID_Role"] = 'R';

                    MessageBox.Show("Успешная регистрация", "Регситрация", MessageBoxButton.OK);
                    NavigationService.Navigate(new RegRace());
                }

            }
            catch (Exception ex)
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
        private void OnShowImg(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images files (*.png)|*.png|Images files (*.jpg)|*.jpg|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    FileName.Text = openFileDialog.FileName;

                Uri fileUri = new Uri(openFileDialog.FileName, UriKind.Absolute);

                Img.Source = new BitmapImage(fileUri);
                
                
                /*
                using (FileStream fs = new FileStream(photoTB.Text, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                System.Windows.Controls.Image ImageContainer = new System.Windows.Controls.Image();
                ImageSource image = new BitmapImage(new Uri(photoTB.Text, UriKind.Absolute));
                ImageContainer.Source = image;
                avatarIMG.Source = ImageContainer.Source;
                */
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message, null, MessageBoxButton.OK, MessageBoxImage.Error); }
        }


        static string[] EmailServers = { "@yandex.ru","@mail.ru","@gmail.ru","@inbox.ru","@ok.ru","@rambler.ru","@yahoo.ru","@mpt.ru","@yandex.com","@mail.com","@gmail.com","@inbox.com","@ok.com",
                                         "@rambler.com","@yahoo.com","@mpt.com"};
        private void CheckEmail()
        {
            string addr = email.Text;

            if( !new EmailAddressAttribute().IsValid(addr) )
                throw new Exception("Не корректный email");


            bool ok = false;
            foreach (string s in EmailServers)
            {
                ok = addr.EndsWith(s);
                if (ok)
                    break;
            }


            if (!ok)
                throw new Exception("Не корректное имя сервера в email");


            //MailAddress ma = new MailAddress(addr);
        }

        private void CheckRequiredFields()
        {
            Control[] cl = new Control[] { email, l_name, f_name, pswd, pswd2, genderList, drozd, country };
            foreach (Control c in cl)
                UITools.ChekReq(c);
        }

        private void CheckPairPassword(PasswordBox pb1, PasswordBox pb2)
        {
            if(pb1.Password != pb2.Password)
                throw new Exception("Пароли не одинаковы");

            Regex regex = new Regex("^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{8,}$");
            if (!regex.IsMatch(pb1.Password))
                throw new Exception("Пароль не удовлетворяет требованиям. В пароле необходимы: символы латинского алфавита в верхнем и нижнем регистре, спец символы и цифры");

        }

        private void CheckDate()
        {
            DateTime d1 = (DateTime)drozd.SelectedDate;

            if( DateTime.Now.Year - d1.Year < 18)
 
                throw new Exception("Нельзая зарегестрироваться, так как младше 18  лет");
         
        }

    }
}

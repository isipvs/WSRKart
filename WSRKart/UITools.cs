using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;

namespace WSRKart
{
    public class UITools
    {
        public static void ChekReq(Control ctrl)
        {
            if (ctrl == null)
                return;

            bool doThrow = false;

            if (ctrl is TextBox)
            {
                TextBox txBox = (TextBox)ctrl;
                doThrow = (txBox.Text == null || txBox.Text.Length == 0);

            }
            else if (ctrl is ComboBox)
            {
                ComboBox cbx = (ComboBox)ctrl;
                doThrow = cbx.SelectedValue == null;
            }
            else if (ctrl is DatePicker)
            {
                DatePicker dtp = (DatePicker)ctrl;
                doThrow = dtp.SelectedDate == null;
            }

            else if (ctrl is PasswordBox)
            {
                PasswordBox psb = (PasswordBox)ctrl;
                doThrow = (psb.Password == null || psb.Password.Length == 0);
            }

            if (doThrow)
            {
                ctrl.Focus();
                Label labeledBy = (Label)AutomationProperties.GetLabeledBy(ctrl);
                if (labeledBy != null)
                    throw new Exception("Поле " + labeledBy.Content + " обязательно к заполнению");
                else
                    throw new Exception("Не все обязательные поля заполнены");
            }
        }

        public static void CheckPairPassword(PasswordBox pb1, PasswordBox pb2)
        {
            if (pb1.Password != pb2.Password)
                throw new Exception("Пароли не одинаковы");

            Regex regex = new Regex("^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{8,}$");
            if (!regex.IsMatch(pb1.Password))
                throw new Exception("Пароль не удовлетворяет требованиям. В пароле необходимы: символы латинского алфавита в верхнем и нижнем регистре, спец символы и цифры");

        }

        static string[] EmailServers = { "@yandex.ru","@mail.ru","@gmail.ru","@inbox.ru","@ok.ru","@rambler.ru","@yahoo.ru","@mpt.ru","@yandex.com","@mail.com","@gmail.com","@inbox.com","@ok.com",
                                         "@rambler.com","@yahoo.com","@mpt.com"};

        public static void CheckEmail(string addr)
        {
            if (!new EmailAddressAttribute().IsValid(addr))
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

        }

        public static void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            var column = dataGrid.Columns[columnIndex];

            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            // Add the new sort description
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));

            // Apply sort
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            // Refresh items to display sort
            dataGrid.Items.Refresh();
        }

        public static void Num_PreviewTextInput(TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }


        public static void Char_PreviewTextInput(TextCompositionEventArgs e)
        {
            Char c = e.Text[0];

            if (!Char.IsLetter(c) && c != ' ' && c != '-')
            {
                e.Handled = true;
            }
        }

    }
}

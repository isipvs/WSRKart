using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;

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
    }
}

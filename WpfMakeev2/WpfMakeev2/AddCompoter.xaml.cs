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
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;

namespace WpfMakeev2
{
    /// <summary>
    /// Логика взаимодействия для AddCompoter.xaml
    /// </summary>
    public partial class AddCompoter : Window
    {
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection cn = new OleDbConnection();
        public AddCompoter()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\gitproject\companyproject\WpfMakeev2\WpfMakeev2\bin\Debug\sysadmin.accdb";
            cmd.Connection = cn;
            if (txtComputer.Text != "")
            {                
                string q = "INSERT INTO computers (compname,status,dep,organization,FIO) VALUES ('" + txtComputer.Text.ToString() + "','" + cmbStatus.Text.ToString() + "','" + cmbDep.Text.ToString() + "','"+ cmbOrg.Text.ToString() + "','"+ txtFIO.Text.ToString()+ "')";
                execsql(q);
                //string q1 = "select computerID as 'Номер',compname as 'Имя компьютера',status as 'Статус',dep as 'Отдел организации',organization as 'Организация',fio as 'ФИО Сотрудника' from computers";
                //execsql(q1);
                this.Close();               
            }
        }

        private void execsql(String q)
        {
            try
            {
                cn.Open();
                cmd.CommandText = q;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception e)
            {
                cn.Close();
                MessageBox.Show(e.Message.ToString());
            }
        }
    }
}

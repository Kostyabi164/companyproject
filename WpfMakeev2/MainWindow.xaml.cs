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
using System.Data.OleDb;
using System.Data;

namespace WpfMakeev2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection cn = new OleDbConnection();        
        string selectitem = "0";
        string selectitem3;
        public static string selectitem4;        
        string namecomputer;
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select number as 'Номер',dateopen as 'Дата открытия',description as 'Описание проблемы',FIOIT as 'Ответственный ИТ',dateclose as 'Дата закрытия проблемы' from problems",con);            
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "problems");
            datagrid1.ItemsSource = ds.Tables["problems"].DefaultView;
            con.Close();            
        }

        private void OpenProblem_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow();
            taskWindow.Show();
        }

        private void ButtonCloseTicket_Click(object sender, RoutedEventArgs e)
        {
            if (selectitem != "0")
            {                
                cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb";
                cmd.Connection = cn;                
                string q = "UPDATE problems SET dateclose='"+ System.DateTime.Now.ToString() + "' WHERE number=" + selectitem.ToString();                
                execsql(q);
                MessageBox.Show("Проблема № " + selectitem + " закрыта " + System.DateTime.Now.ToString());
                Button_Click(this,null);
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

        private void TabItem_Initialized(object sender, EventArgs e)
        {            
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select number as 'Номер',dateopen as 'Дата открытия',description as 'Описание проблемы',fioit as 'Ответственный ИТ',dateclose as 'Дата закрытия проблемы' from problems", con);            
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "problems");
            datagrid1.ItemsSource = ds.Tables["problems"].DefaultView;
            con.Close();
        }

        public void tabComputers_Initialized(object sender, EventArgs e)
        {            
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select computerID as 'Номер',compname as 'Имя компьютера',status as 'Статус',dep as 'Отдел организации',organization as 'Организация',fio as 'ФИО Сотрудника' from computers", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "computers");
            datagrid4.ItemsSource = ds.Tables["computers"].DefaultView;
            con.Close();            
        }

        private void btnAddComputer_Click(object sender, RoutedEventArgs e)
        {
            AddCompoter addCompoter = new AddCompoter();
            addCompoter.Show();
        }

        private void tabLicense_Initialized(object sender, EventArgs e)
        {            
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select licenseID as 'Номер',postavchik as 'Поставщик софта',proizvoditel as 'Производитель программы',po as 'Наименование программы',countpo as 'Количество лицензий' from license", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds, "license");
            datagrid3.ItemsSource = ds.Tables["license"].DefaultView;
            con.Close();
        }

        private void datagrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = datagrid4.SelectedValue as DataRowView;
                selectitem4 = row[0].ToString();
                namecomputer = row[5].ToString();

            }
            catch
            { }
        }

        private void btnDelComp_Click(object sender, RoutedEventArgs e)
        {            
            if (selectitem4 != "")
            {
                cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb";
                cmd.Connection = cn;
                string q = "DELETE from computers WHERE computerID=" + selectitem4.ToString();
                execsql(q);
                tabComputers_Initialized(this, null);
                MessageBox.Show("Компьютер пользователя " + namecomputer + " успешно удален");
            }
        }

        private void datagrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {            
            try
            {
                DataRowView row = datagrid1.SelectedValue as DataRowView;
                selectitem = row[0].ToString();
            }
            catch
            { }
        }

        private void btnAddLicense_Click(object sender, RoutedEventArgs e)
        {   
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb";
            cmd.Connection = cn;
            if (txtpostavchik.Text != "")
                {                
                    string q = "INSERT INTO license (postavchik,proizvoditel,po,countpo) VALUES ('" + txtpostavchik.Text.ToString() + "','" + txtproizvoditel.Text.ToString() + "','" + txtPO.Text.ToString() + "','" + txtcount.Text.ToString() + "')";                    
                    execsql(q);
                    tabLicense_Initialized(this, null);
                }            
        }

        private void datagrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = datagrid3.SelectedValue as DataRowView;
                selectitem3 = row[0].ToString();
            }
            catch
            { }
        }

        private void btnRemoveLicense_Click(object sender, RoutedEventArgs e)
        {
            cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sysadmin.accdb";
            cmd.Connection = cn;
            string q = "DELETE from license WHERE licenseID=" + selectitem3.ToString();
            execsql(q);
            tabLicense_Initialized(this, null);
            MessageBox.Show("Лицензия успешно удалена");
        }

        private void btnRemont_Click(object sender, RoutedEventArgs e)
        {
            if (selectitem4 != null)
            {
                Remont addremont = new Remont();
                addremont.Show();
            }
        }

        private void btnRefreshComputer_Click(object sender, RoutedEventArgs e)
        {
            tabComputers_Initialized(this, null);
        }
    }
}

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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Data.SqlClient;



namespace MahApps.Metro.Application2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Label:
            LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your credentials" , new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme });
            if (result != null)
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
                con.Open();
                string q = "select * from Login where User_Name='" + result.Username + "' and Password='" + result.Password + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    if (dr.HasRows == true)
                    {
                        MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
                    }
                    
                }
                if(dr.HasRows == false) 
                {
                    MessageDialogResult messageResult = await this.ShowMessageAsync("wrong Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
                    goto Label;
                }
                
            }
            else if (result == null)
            {
                this.Close();
            }
            
            
        }
    }
}

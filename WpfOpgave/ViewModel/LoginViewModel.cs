using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WpfOpgave.Commands;

namespace WpfOpgave.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.SetProperty(ref this.email, value, nameof(this.Email));
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.SetProperty(ref this.password, value, nameof(this.Password));
            }
        }

        public ICommand LoginCommand => new RelayCommand(
            vm => this.LoginUser(vm as MainViewModel),
            _ => true);

        private string email;
        private string password;


        public void LoginUser(MainViewModel vm)
        {
            if (vm == null)
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(@"Server=.\SQLSERVER; Initial Catalog=WpfOpgave; Integrated Security=True;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("LoginUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@password", Password);
                    if (command.ExecuteReader().Read())
                    {
                        vm.ViewModel = new HomeViewModel();
                    }
                    else
                    {
                        MessageBox.Show("Der skete en fejl");
                    }
                }
            }
        }
    }
}

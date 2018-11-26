using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WpfOpgave.Commands;

namespace WpfOpgave.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                this.SetProperty(ref this.firstName, value, nameof(this.FirstName));
            }
        }

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

        public ICommand RegisterCommand => new RelayCommand(
            vm => this.RegisterUser(vm as MainViewModel),
            _ => true);

        private string firstName;
        private string email;
        private string password;

        public void RegisterUser(MainViewModel vm)
        {
            if (vm == null)
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(@"Server=.\SQLSERVER; Initial Catalog=WpfOpgave; Integrated Security=True;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CreateUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@firstName", FirstName);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@password",Password);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Brugeren er nu oprettet");
                        vm.ViewModel = new LoginViewModel();
                    }
                    else
                    {
                        MessageBox.Show("Emailen er allerede i brug");
                    }
                }
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using WpfOpgave.Commands;
using WpfOpgave.Session;

namespace WpfOpgave.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public string FirstName
        {
            get
            {
                return this.firstname;
            }
            set
            {
                this.SetProperty(ref firstname, value, nameof(FirstName));
            }
        }

        private string firstname;

        public HomeViewModel()
        {
            using (SqlConnection connection = new SqlConnection(@"Server=.\SQLSERVER; Initial Catalog=WpfOpgave; Integrated Security=True;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SelectUserInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", SessionManager.UserId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.FirstName = reader.GetString(2);
                        }
                    }
                }
            }
        }
    }
}

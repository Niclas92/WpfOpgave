using System.Windows.Input;
using WpfOpgave.Commands;

namespace WpfOpgave.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public BaseViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }

            set
            {
                this.SetProperty(ref this.viewModel, value, nameof(this.ViewModel));
            }
        }

        //public ICommand DisplayLoginView
        //{
        //    get
        //    {
        //        return new RelayCommand(_ => ViewModel = new RegisterViewModel(),
        //        _ => true);
        //    }
        //}

        public ICommand DisplayRegisterView => new RelayCommand(
            _ => ViewModel = new RegisterViewModel(),
            _ => true);

        public ICommand DisplayLoginView => new RelayCommand(
            _ => ViewModel = new LoginViewModel(),
            _ => true);

        private BaseViewModel viewModel;
    }
}

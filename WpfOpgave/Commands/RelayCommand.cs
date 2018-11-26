using System;
using System.Windows.Input;

namespace WpfOpgave.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<object> methodToExecute;
        private Func<object, bool> canExecuteEvaluator;

        public RelayCommand(Action<object> methodToExecute, Func<object, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                return this.canExecuteEvaluator(parameter);
            }
        }
        public void Execute(object parameter)
        {
            this.methodToExecute?.Invoke(parameter);
        }
    }
}

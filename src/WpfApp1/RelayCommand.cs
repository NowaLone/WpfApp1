using System;
using System.Windows.Input;

namespace WpfApp1
{
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        #endregion Fields

        #region Constructors

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        #endregion Constructors

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion Events

        #region Methods

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        #endregion Methods
    }
}
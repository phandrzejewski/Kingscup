using System;
using System.Windows.Input;

namespace Kingscup.Commands
{
    public class DelegateCommand : ICommand
    {
        #region fields

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        #endregion

        #region constructors

        public DelegateCommand(Action<object> method) : this(method, null)
        {
        }

        public DelegateCommand(Action<object> method, Predicate<object> canExecute)
        {
            _execute = method;
            _canExecute = canExecute;
        }

        #endregion

        #region methods

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
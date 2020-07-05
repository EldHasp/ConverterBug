using System;
using System.Windows;
using System.Windows.Input;

namespace ConverterPhantomFramework
{

    #region Delegates to WPF Command Methods
    public delegate void ExecuteHandler(object parameter);
    public delegate bool CanExecuteHandler(object parameter);
    #endregion

    #region Command Class - RelayCommand
    /// <summary>A class implementing the ICommand interface for creating WPF commands.</summary>
    public class RelayCommand : ICommand
    {
        private readonly CanExecuteHandler _canExecute;
        private readonly ExecuteHandler _onExecute;
        private readonly EventHandler _requerySuggested;

        public event EventHandler CanExecuteChanged;

        /// <summary>Command constructor.</summary>
        /// <param name="execute">Command Executable Method.</param>
        /// <param name="canExecute">Method allowing the execution of a command.</param>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler canExecute = null)
        {
            _onExecute = execute;
            _canExecute = canExecute;

            _requerySuggested = (o, e) => Invalidate();
            CommandManager.RequerySuggested += _requerySuggested;
        }

        /// <summary>The public method of invoking a command status check event.</summary>
        public void Invalidate()
            => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }));

        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute.Invoke(parameter);

        public void Execute(object parameter) => _onExecute?.Invoke(parameter);
    }
    #endregion
}


using System;
using System.Windows.Input;

namespace RelayCommand
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand{T}" />.
        /// </summary>
        /// <param name="execute">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <remarks><seealso cref="CanExecute" /> will always return true.</remarks>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// </summary>
        /// <param name="execute">The execution logic w/o param.</param>
        /// <param name="canExecute">The execution status logic w/o param.</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = _ => execute.Invoke();
            _canExecute = _ => canExecute?.Invoke() ?? true;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T) parameter) ?? true;
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this object can be
        ///     set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter)
        {
            _execute((T) parameter);
        }
        /// <summary>
        /// Method to invoke CanExecuteChanged
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action action, Func<bool> canExecute = null) : base(action, canExecute)
        {
        }

        public RelayCommand(Action<object> action, Predicate<object> canExecute = null) : base(action, canExecute)
        {
        }
    }
}
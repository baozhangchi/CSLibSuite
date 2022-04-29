using System;
using System.Windows.Input;

namespace WPFUtils
{
    public abstract class CommandBase : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        protected CommandBase(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_canExecute(parameter))
            {
                _execute(parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }

    public sealed class Command : CommandBase
    {
        public Command(Action execute, Func<bool> canExecute) : base(o => execute(), o => canExecute())
        {
        }

        public Command(Action execute) : this(execute, () => true)
        {

        }
    }

    public sealed class Command<T> : CommandBase
    {
        public Command(Action<T> execute, Func<T, bool> canExecute) : base(o => execute((T)o), o => canExecute((T)o))
        {

        }

        public Command(Action<T> execute) : this(execute, o => true)
        {

        }
    }
}

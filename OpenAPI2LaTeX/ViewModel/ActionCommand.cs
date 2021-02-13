using System;
using System.Windows.Input;

namespace OpenAPI2LaTeX.ViewModel
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> m_executeHandler;
        private readonly Func<object, bool> m_canExecuteHandler;

        public ActionCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute cannot be null");
            m_executeHandler = execute;
            m_canExecuteHandler = canExecute;
        }

        public ActionCommand(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute cannot be null");
            m_executeHandler = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            m_executeHandler(parameter);
        }
        public bool CanExecute(object parameter)
        {
            if (m_canExecuteHandler == null)
                return true;
            return m_canExecuteHandler(parameter);
        }
    }
}

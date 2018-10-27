﻿using System;
using System.Windows.Input;

namespace BusinessLogic.Base
{
    public class RelayCommand : ICommand
    {
        private readonly Action _targetExecuteMethod;
        private readonly Func<bool> _targetCanExecuteMethod;

        public RelayCommand(Action executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod != null)
            {
                return _targetCanExecuteMethod();
            }
            return _targetExecuteMethod != null;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            _targetExecuteMethod?.Invoke();
        }

        #endregion ICommand Members
    }
}

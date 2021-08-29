using System;
using System.Windows.Input;

namespace EasyRooms.ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _targetExecuteMethod;
        private readonly Func<bool>? _targetCanExecuteMethod;
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action targetExecuteMethod)
        {
            _targetExecuteMethod = targetExecuteMethod;
        }

        public RelayCommand(Action targetExecuteMethod, Func<bool> targetCanExecuteMethod)
        {
            _targetExecuteMethod = targetExecuteMethod;
            _targetCanExecuteMethod = targetCanExecuteMethod;
        }

        public void RaiseCanExecuteChanged() 
            => CanExecuteChanged!(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) 
            => _targetCanExecuteMethod is null ? true : _targetCanExecuteMethod();

        public void Execute(object? parameter) 
            => _targetExecuteMethod();
    }
}

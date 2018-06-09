using System;
using System.Windows.Input;
using GenRevision.ViewModels;

namespace GenRevision.Commands
{
    public class ShowCommand : ICommand
    {
        private readonly IMainViewModel _vm;

        public ShowCommand(IMainViewModel viewModel)
        {
            _vm = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public event EventHandler CanExecuteChanged = delegate
        {
        };

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _vm.OnShowWindow();
    }
}

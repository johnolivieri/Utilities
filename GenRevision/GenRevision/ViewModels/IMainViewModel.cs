using System;
using System.ComponentModel;

namespace GenRevision.ViewModels
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        void OnShowWindow();
    }
}

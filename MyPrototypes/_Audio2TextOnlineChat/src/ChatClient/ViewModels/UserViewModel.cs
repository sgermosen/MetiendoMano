using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    public class UserViewModel : BindableBase
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int MessageCount { get; set; }
        public bool IsAdmin { get; set; }
        public ICommand UserSelectedCommand { get; internal set; }
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.Properties;

namespace Stachowski.WinDict.ViewModels.ObjectsViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public IUser BackingObject;
        private IUserRepository _userRep = AssemblyBinder.AssemblyBinder.UserRepository;

        public UserViewModel(IUser user)
        {
            BackingObject = user;
        }

        public string Nick
        {
            get { return BackingObject.Nick; }
            set 
            { 
                BackingObject.Nick = value;
                _userRep.RenameUser(BackingObject, value);
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Nick;
        }
    }
}

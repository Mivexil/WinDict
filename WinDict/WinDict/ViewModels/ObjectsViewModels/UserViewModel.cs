using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.Interfaces;
using WinDict.Annotations;

namespace WinDict.ViewModels.ObjectsViewModels
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

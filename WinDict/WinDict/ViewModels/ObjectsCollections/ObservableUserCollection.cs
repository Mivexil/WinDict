using System.Collections.Generic;
using System.Collections.ObjectModel;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.ViewModels.ObjectsCollections
{
    public class ObservableUserCollection : ObservableCollection<UserViewModel>
    {
        public ObservableUserCollection() : base()
        {
        }

        public ObservableUserCollection(List<UserViewModel> l) : base(l)
        {
        }

        public ObservableUserCollection(IEnumerable<UserViewModel> ie) : base(ie)
        {
        }

        private IUserRepository _userRep = AssemblyBinder.AssemblyBinder.UserRepository;
        public void Add(string name)
        {
            _userRep.AddUser(name);
            Add(new UserViewModel(_userRep.GetByNick(name)));
        }

        public new void Remove(UserViewModel vm)
        {
            _userRep.DeleteUser(vm.BackingObject);
            base.Remove(vm);
        }
    }
}

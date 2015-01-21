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
    public class LanguageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ILanguage BackingObject;
        private ILanguageRepository _langRep = AssemblyBinder.AssemblyBinder.LanguageRepository;

        public LanguageViewModel(ILanguage language)
        {
            BackingObject = language;
        }

        public string Name
        {
            get { return BackingObject.Name; }
            set
            {
                BackingObject.Name = value;
                _langRep.RenameLanguage(BackingObject, value);
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

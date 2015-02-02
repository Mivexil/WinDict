using System.ComponentModel;
using System.Runtime.CompilerServices;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.Properties;

namespace Stachowski.WinDict.ViewModels.ObjectsViewModels
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

        public override bool Equals(object obj)
        {
            return (obj is LanguageViewModel && ((LanguageViewModel) obj).Name == this.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}

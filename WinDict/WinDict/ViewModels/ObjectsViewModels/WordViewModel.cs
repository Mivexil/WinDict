using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Stachowski.WinDict.Interfaces;
using WinDict.Annotations;
using WinDict.ViewModels.General;

namespace WinDict.ViewModels.ObjectsViewModels
{
    public class WordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public IWord BackingObject;
        private IWordRepository _wordRep = AssemblyBinder.AssemblyBinder.WordRepository;
        private IWordStatisticsRepository _wsRep = AssemblyBinder.AssemblyBinder.StatisticsRepository;

        public WordViewModel(IWord word)
        {
            BackingObject = word;
        }

        public Bitmap Picture
        {
            get { return BackingObject.Picture; }
            set
            {
                BackingObject.Picture = value;
                _wordRep.UpdatePicture(BackingObject, value);
                OnPropertyChanged();
            }
        }

        public string Theme
        {
            get { return BackingObject.Theme; }
            set
            {
                BackingObject.Theme = value;
                _wordRep.ChangeTheme(BackingObject, value);
                OnPropertyChanged();
            }
        }

        private ObservableDictionary<LanguageViewModel, string> _definitions;
        public ObservableDictionary<LanguageViewModel, string> Definitions
        {
            get
            {
                if (_definitions != null) return _definitions;
                _definitions = new ObservableDefinitionDictionary(BackingObject);
                return _definitions;
            }
            set
            {
                _wordRep.ChangeDefinition(BackingObject, value.ToDictionary(x => x.Key.BackingObject, x => x.Value));
                _definitions = value;
                OnPropertyChanged();
            }
        }

        public void AddTry(LanguageViewModel from, LanguageViewModel to, UserViewModel user, bool success)
        {
            var stats =
                new StatisticsViewModel(_wsRep.GetStatistics(user.BackingObject, BackingObject, from.BackingObject,
                    to.BackingObject));
            stats.AddTry(success);
        }
    }
}

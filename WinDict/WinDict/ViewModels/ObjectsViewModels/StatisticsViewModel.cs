using System.ComponentModel;
using System.Runtime.CompilerServices;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.Properties;

namespace Stachowski.WinDict.ViewModels.ObjectsViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private IWordStatisticsRepository _wsRep = AssemblyBinder.AssemblyBinder.StatisticsRepository;
        public IWordStatistics BackingObject;

        public StatisticsViewModel(IWordStatistics _stats)
        {
            BackingObject = _stats;
        }

        public void AddTry(bool success)
        {
            _wsRep.AddTry(BackingObject.Word, BackingObject.User, BackingObject.FromLanguage, BackingObject.ToLanguage,
                success);
        }

        public int Tries
        {
            get { return BackingObject.Tries; }
        }

        public int Successes
        {
            get { return BackingObject.Successes; }
        }

        private LanguageViewModel _fromLanguage;
        public LanguageViewModel FromLanguage
        {
            get { return _fromLanguage ?? (_fromLanguage = new LanguageViewModel(BackingObject.FromLanguage)); }
        }

        private LanguageViewModel _toLanguage;
        public LanguageViewModel ToLanguage
        {
            get { return _toLanguage ?? (_toLanguage = new LanguageViewModel(BackingObject.ToLanguage)); }
        }

        private UserViewModel _user;
        public UserViewModel User
        {
            get { return _user ?? (_user = new UserViewModel(BackingObject.User)); }
        }

        private WordViewModel _word;
        public WordViewModel Word
        {
            get { return _word ?? (_word = new WordViewModel(BackingObject.Word)); }
        }
    }
}

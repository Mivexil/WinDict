using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.Interfaces;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.General;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;
using Stachowski.WinDict.Views;

namespace Stachowski.WinDict.ViewModels
{
    public class LearnTestViewModel : DependencyObject
    {

        public static Random rng = new Random();

        public LearnTestViewModel()
        {
            _textConfirmedClickedCommand = new RelayCommand(x =>
            {
                var wnd = (Test)x;
                if (wnd.FlashCard.Flipped)
                {
                    SelectedWord = WordList.ToList().ElementAt(rng.Next(WordList.Count));
                    wnd.FlashCard.Flipped = false;
                    wnd.RaiseEvent(new WordGuessedEventArgs(Test.WordGuessedEvent, null));
                }
                else
                {
                    SelectedWord.AddTry(PrimaryLanguage, SecondaryLanguage, CurrentUser,
                        TypedWord.ToLower() == SelectedWord.Definitions[SecondaryLanguage].ToLower());
                    wnd.FlashCard.Flipped = true;
                    wnd.RaiseEvent(new WordGuessedEventArgs(Test.WordGuessedEvent, TypedWord.ToLower() == SelectedWord.Definitions[SecondaryLanguage].ToLower()));
                }
            });
        }

        public static readonly DependencyProperty CurrentUserProperty =
             DependencyProperty.Register("CurrentUser", typeof(UserViewModel), typeof(LearnTestViewModel));

        public UserViewModel CurrentUser
        {
            get { return (UserViewModel)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        public static readonly DependencyProperty PrimaryLanguageProperty =
            DependencyProperty.Register("PrimaryLanguage", typeof (LanguageViewModel), typeof (LearnTestViewModel));
        
        public LanguageViewModel PrimaryLanguage
        {
            get { return (LanguageViewModel) GetValue(PrimaryLanguageProperty); }
            set { SetValue(PrimaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty SecondaryLanguageProperty =
            DependencyProperty.Register("SecondaryLanguage", typeof(LanguageViewModel), typeof(LearnTestViewModel));

        public LanguageViewModel SecondaryLanguage
        {
            get { return (LanguageViewModel)GetValue(SecondaryLanguageProperty); }
            set { SetValue(SecondaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty WordStatisticsProperty = DependencyProperty.Register(
            "WordStatistics", typeof (StatisticsViewModel), typeof (LearnTestViewModel));

        public StatisticsViewModel WordStatistics
        {
            get { return (StatisticsViewModel) GetValue(WordStatisticsProperty); }
            set { SetValue(WordStatisticsProperty, value); }
        }

        public static readonly DependencyProperty SelectedWordProperty = DependencyProperty.Register("SelectedWord",
            typeof (WordViewModel), typeof (LearnTestViewModel), new PropertyMetadata(OnSelectedWordChanged));

        private static void OnSelectedWordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LearnTestViewModel)d).WordStatistics = ((WordViewModel) e.NewValue).GetStats(((LearnTestViewModel) d).PrimaryLanguage,
                ((LearnTestViewModel) d).SecondaryLanguage, ((LearnTestViewModel) d).CurrentUser);
        }

        public WordViewModel SelectedWord
        {
            get { return (WordViewModel) GetValue(SelectedWordProperty); }
            set { SetValue(SelectedWordProperty, value); }
        }

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register("Theme", typeof (string),
            typeof (LearnTestViewModel), new PropertyMetadata(OnThemeChanged));

        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LearnTestViewModel) d).WordList = GetWordsForTheme((string) e.NewValue, ((LearnTestViewModel) d).CurrentUser,
                ((LearnTestViewModel) d).PrimaryLanguage, ((LearnTestViewModel) d).SecondaryLanguage);
            ((LearnTestViewModel) d).SelectedWord =
                ((LearnTestViewModel) d).WordList.ToList()
                    .ElementAt(LearnTestViewModel.rng.Next(((LearnTestViewModel) d).WordList.Count));
        }

        private static IWordRepository _wordRep = AssemblyBinder.AssemblyBinder.WordRepository;
        private static IWordStatisticsRepository _wsRep = AssemblyBinder.AssemblyBinder.StatisticsRepository;

        private static ObservableCollection<WordViewModel> GetWordsForTheme(string theme, UserViewModel user, LanguageViewModel primaryLanguage, LanguageViewModel secondaryLanguage)
        {
            if (theme == "All" || String.IsNullOrEmpty(theme))
            {
                return new ObservableCollection<WordViewModel>(_wordRep.GetAll().Select(x => new WordViewModel(x)));
            }
            else if (theme == "Easy")
            {
                var words =
                    _wordRep.GetAll()
                        .Where(
                            x =>
                            {
                                var stats = _wsRep.GetStatistics(user.BackingObject, x, primaryLanguage.BackingObject,
                                    secondaryLanguage.BackingObject);
                                return (stats.Successes >= (stats.Tries - stats.Successes)*3);
                            }).Select(x => new WordViewModel(x));
                return new ObservableCollection<WordViewModel>(words);
            }
            else if (theme == "Difficult")
            {
                var words =
                    _wordRep.GetAll()
                        .Where(
                            x =>
                            {
                                var stats = _wsRep.GetStatistics(user.BackingObject, x, primaryLanguage.BackingObject,
                                    secondaryLanguage.BackingObject);
                                return (stats.Successes*3 < (stats.Tries - stats.Successes));
                            }).Select(x => new WordViewModel(x));
                return new ObservableCollection<WordViewModel>(words);
            }
            else
                return
                    new ObservableCollection<WordViewModel>(
                        _wordRep.GetAllWithTheme(theme).Select(x => new WordViewModel(x)));
        }

        public string Theme
        {
            get { return (string) GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public static readonly DependencyProperty WordListProperty = DependencyProperty.Register("WordList",
            typeof (ObservableCollection<WordViewModel>), typeof (LearnTestViewModel));

        public ObservableCollection<WordViewModel> WordList
        {
            get { return (ObservableCollection<WordViewModel>) GetValue(WordListProperty); }
            set { SetValue(WordListProperty, value); }
        }

        public static readonly DependencyProperty TypedWordProperty = DependencyProperty.Register("TypedWord",
            typeof (string), typeof (LearnTestViewModel));

        public string TypedWord
        {
            get { return (string) GetValue(TypedWordProperty); }
            set { SetValue(TypedWordProperty, value); }
        }

        private ICommand _textConfirmedClickedCommand;

        public ICommand TextConfirmedClicked
        {
            get { return _textConfirmedClickedCommand; }
        }
    }
}

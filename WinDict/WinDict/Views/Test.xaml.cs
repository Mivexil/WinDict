using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Stachowski.WinDict.ViewModels;
using Stachowski.WinDict.ViewModels.EventArgs;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.Views
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {

        public static readonly RoutedEvent WordGuessedEvent = EventManager.RegisterRoutedEvent("WordGuessed",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Test));

        public event RoutedEventHandler WordGuessed
        {
            add { AddHandler(WordGuessedEvent, value); }
            remove { RemoveHandler(WordGuessedEvent, value); }
        }

        public string Theme
        {
            get { return ((LearnTestViewModel)DataContext).Theme; }
            set { ((LearnTestViewModel)DataContext).Theme = value; }
        }

        public LanguageViewModel PrimaryLanguage
        {
            get { return ((LearnTestViewModel)DataContext).PrimaryLanguage; }
            set { ((LearnTestViewModel)DataContext).PrimaryLanguage = value; }
        }

        public LanguageViewModel SecondaryLanguage
        {
            get { return ((LearnTestViewModel)DataContext).SecondaryLanguage; }
            set { ((LearnTestViewModel)DataContext).SecondaryLanguage = value; }
        }

        public ObservableCollection<WordViewModel> WordList
        {
            get { return ((LearnTestViewModel)DataContext).WordList; }
            set { ((LearnTestViewModel)DataContext).WordList = value; }
        }

        public UserViewModel CurrentUser
        {
            get { return ((LearnTestViewModel)DataContext).CurrentUser; }
            set { ((LearnTestViewModel)DataContext).CurrentUser = value; }
        }

        public Test()
        {
            InitializeComponent();
            WordGuessed += (s, e) =>
            {
                switch (((WordGuessedEventArgs) e).Success)
                {
                    case null:
                        TypedWordTextBox.Background = new SolidColorBrush(Colors.White);
                        break;
                    case true:
                        TypedWordTextBox.Background = new SolidColorBrush(Colors.GreenYellow);
                        break;
                    case false:
                        TypedWordTextBox.Background = new SolidColorBrush(Colors.Tomato);
                        break;
                }
            };
        }
    }
}

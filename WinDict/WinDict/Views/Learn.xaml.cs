using System.Collections.ObjectModel;
using System.Windows.Controls;
using Stachowski.WinDict.ViewModels;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.Views
{
    /// <summary>
    /// Interaction logic for Learn.xaml
    /// </summary>
    public partial class Learn : UserControl
    {

        public string Theme
        {
            get { return ((LearnTestViewModel)DataContext).Theme; }
            set { ((LearnTestViewModel)DataContext).Theme = value; }
        }

        public LanguageViewModel PrimaryLanguage
        {
            get { return ((LearnTestViewModel) DataContext).PrimaryLanguage; }
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

        public Learn()
        {
            InitializeComponent();
        }
    }
}

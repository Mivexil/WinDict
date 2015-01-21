using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinDict.ViewModels;
using WinDict.ViewModels.ObjectsViewModels;

namespace WinDict.Views
{
    /// <summary>
    /// Interaction logic for FlashCard.xaml
    /// </summary>
    public partial class FlashCard : UserControl
    {

        public bool Flippable
        {
            get { return ((FlashCardViewModel) DataContext).Flippable; }
            set { ((FlashCardViewModel) DataContext).Flippable = value; }
        }

        public bool Flipped
        {
            get { return ((FlashCardViewModel)DataContext).Flipped; }
            set { ((FlashCardViewModel)DataContext).Flipped = value; }
        }

        public LanguageViewModel PrimaryLanguage
        {
            get { return ((FlashCardViewModel)DataContext).PrimaryLanguage; }
            set { ((FlashCardViewModel)DataContext).PrimaryLanguage = value; }
        }

        public LanguageViewModel SecondaryLanguage
        {
            get { return ((FlashCardViewModel)DataContext).SecondaryLanguage; }
            set { ((FlashCardViewModel)DataContext).SecondaryLanguage = value; }
        }

        public WordViewModel Word
        {
            get { return ((FlashCardViewModel)DataContext).Word; }
            set { ((FlashCardViewModel)DataContext).Word = value; }
        }

        public FlashCard()
        {
            InitializeComponent();
        }
    }
}

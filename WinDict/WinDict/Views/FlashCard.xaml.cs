using System.Windows;
using System.Windows.Controls;
using Stachowski.WinDict.ViewModels;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.Views
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

        public static readonly DependencyProperty PrimaryLanguageProperty =
            DependencyProperty.Register("PrimaryLanguage", typeof (LanguageViewModel), typeof (FlashCard),
                new PropertyMetadata(OnPrimaryLanguageChanged));

        private static void OnPrimaryLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FlashCardViewModel)(((FlashCard)d).DataContext)).PrimaryLanguage = e.NewValue as LanguageViewModel;
        }

        public LanguageViewModel PrimaryLanguage
        {
            get { return (LanguageViewModel)GetValue(PrimaryLanguageProperty); }
            set { SetValue(PrimaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty SecondaryLanguageProperty =
            DependencyProperty.Register("SecondaryLanguage", typeof(LanguageViewModel), typeof(FlashCard),
                new PropertyMetadata(OnSecondaryLanguageChanged));

        private static void OnSecondaryLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FlashCardViewModel)(((FlashCard)d).DataContext)).SecondaryLanguage = e.NewValue as LanguageViewModel;
        }

        public LanguageViewModel SecondaryLanguage
        {
            get { return (LanguageViewModel)GetValue(SecondaryLanguageProperty); }
            set { SetValue(SecondaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty WordProperty =
            DependencyProperty.Register("Word", typeof (WordViewModel), typeof (FlashCard),
                new PropertyMetadata(OnWordChanged));

        private static void OnWordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FlashCardViewModel) (((FlashCard) d).DataContext)).Word = e.NewValue as WordViewModel;
            ((FlashCardViewModel)(((FlashCard)d).DataContext)).Flipped = false;
            ((FlashCardViewModel)(((FlashCard)d).DataContext)).WordPicture = null;        
        }

        public WordViewModel Word
        {
            get { return (WordViewModel)GetValue(WordProperty); }
            set { SetValue(WordProperty, value); }
        }

        public FlashCard()
        {
            InitializeComponent();
        }
    }
}

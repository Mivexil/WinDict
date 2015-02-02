using System.Drawing;
using System.Windows;
using System.Windows.Input;
using Stachowski.WinDict.ViewModels.General;
using Stachowski.WinDict.ViewModels.ObjectsViewModels;

namespace Stachowski.WinDict.ViewModels
{
    public class FlashCardViewModel : DependencyObject
    {

        public FlashCardViewModel()
        {
            _flipClickCommand = new RelayCommand(x =>
            {
                if (Flippable) Flip();
            });
        }

        public static readonly DependencyProperty FlippableProperty = DependencyProperty.Register("Flippable",
            typeof (bool), typeof (FlashCardViewModel));

        public bool Flippable
        {
            get { return (bool) GetValue(FlippableProperty); }
            set { SetValue(FlippableProperty, value); }
        }

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register("Word",
            typeof (WordViewModel), typeof (FlashCardViewModel), new FrameworkPropertyMetadata(OnWordChanged) {BindsTwoWayByDefault = true});

        private static void OnWordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FlashCardViewModel)d).Flipped = false;
        }

        public WordViewModel Word
        {
            get { return (WordViewModel) GetValue(WordProperty); }
            set { SetValue(WordProperty, value); }
        }

        public static readonly DependencyProperty WordPictureProperty =
            DependencyProperty.Register("WordPicture", typeof (Bitmap), typeof (FlashCardViewModel));

        public Bitmap WordPicture
        {
            get { return (Bitmap) GetValue(WordPictureProperty); }
            set { SetValue(WordPictureProperty, value); }
        }

        public static readonly DependencyProperty PrimaryLanguageProperty =
            DependencyProperty.Register("PrimaryLanguage", typeof (LanguageViewModel), typeof (FlashCardViewModel));

        public LanguageViewModel PrimaryLanguage
        {
            get { return (LanguageViewModel) GetValue(PrimaryLanguageProperty); }
            set { SetValue(PrimaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty SecondaryLanguageProperty =
            DependencyProperty.Register("SecondaryLanguage", typeof(LanguageViewModel), typeof(FlashCardViewModel));

        public LanguageViewModel SecondaryLanguage
        {
            get { return (LanguageViewModel)GetValue(SecondaryLanguageProperty); }
            set { SetValue(SecondaryLanguageProperty, value); }
        }

        public static readonly DependencyProperty FlippedProperty =
            DependencyProperty.Register("Flipped", typeof (bool), typeof (FlashCardViewModel));

        public bool Flipped
        {
            get { return (bool) GetValue(FlippedProperty); }
            set { SetValue(FlippedProperty, value); }
        }

        private ICommand _flipClickCommand;
        public ICommand FlipClick
        {
            get { return _flipClickCommand; }
        }

        public void Flip()
        {
            Flipped = !Flipped;
            WordPicture = Flipped ? Word.Picture : null;
        }
    }
}

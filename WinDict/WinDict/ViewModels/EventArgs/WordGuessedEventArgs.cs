using System.Windows;

namespace Stachowski.WinDict.ViewModels.EventArgs
{
    public class WordGuessedEventArgs : RoutedEventArgs
    {
        public bool? Success { get; set; }

        public WordGuessedEventArgs(RoutedEvent e, bool? success) : base(e)
        {
            Success = success;
        }

    }
}

using System.Windows;

namespace Stachowski.WinDict.ViewModels.EventArgs
{
    public class CancellableEventArgs : RoutedEventArgs
    {
        public bool Cancel { get; set; }

        public CancellableEventArgs(RoutedEvent e) : base(e)
        {
            
        }
    }
}

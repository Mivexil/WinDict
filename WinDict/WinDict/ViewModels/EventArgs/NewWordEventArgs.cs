using System.Windows;

namespace Stachowski.WinDict.ViewModels.EventArgs
{
    public class NewWordEventArgs : CancellableEventArgs
    {
        public NewWordEventArgs(RoutedEvent e) : base(e)
        {
        }

        public string CancelledField { get; set; }
    }
}

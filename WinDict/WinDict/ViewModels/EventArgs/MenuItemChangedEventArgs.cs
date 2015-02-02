using System.Windows;

namespace Stachowski.WinDict.ViewModels.EventArgs
{
    public class MenuItemChangedEventArgs : RoutedEventArgs
    {
        public MenuItem Item { get; set; }

        public MenuItemChangedEventArgs(RoutedEvent ev, MenuItem item) : base(ev)
        {
            Item = item;
        }

    }
}

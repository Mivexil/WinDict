using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinDict.ViewModels.EventArgs
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

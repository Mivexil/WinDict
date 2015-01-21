using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinDict.ViewModels.EventArgs
{
    public class CancellableEventArgs : RoutedEventArgs
    {
        public bool Cancel { get; set; }

        public CancellableEventArgs(RoutedEvent e) : base(e)
        {
            
        }
    }
}

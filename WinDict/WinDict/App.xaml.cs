using System.Windows;

namespace Stachowski.WinDict
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AssemblyBinder.AssemblyBinder.CheckAndSeedDatabase();
        }
    }
}

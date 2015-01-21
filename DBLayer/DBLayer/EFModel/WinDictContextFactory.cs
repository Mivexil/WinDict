using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stachowski.WinDict.DBLayer.EFModel
{
    internal static class WinDictContextFactory
    {
        public static Lazy<WinDictContext> Context = new Lazy<WinDictContext>(() => new WinDictContext());
    }
}

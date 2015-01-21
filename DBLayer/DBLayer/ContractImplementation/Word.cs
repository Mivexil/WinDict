using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.DBLayer.ContractImplementation
{
    public class Word : IWord
    {
        internal int EntityID { get; set; }
        public IDictionary<ILanguage, string> Definitions { get; set; }
        public string Theme { get; set; }
        public Bitmap Picture { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.DBLayer.ContractImplementation
{
    public class WordStatistics : IWordStatistics
    {
        internal int EntityID { get; set; }
        public int Successes { get; set; }
        public int Tries { get; set; }
        public IUser User { get; set; }
        public IWord Word { get; set; }
        public ILanguage FromLanguage { get; set; }
        public ILanguage ToLanguage { get; set; }
    }
}

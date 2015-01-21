using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stachowski.WinDict.Interfaces
{
    public interface IWordStatistics : IEntity
    {
        IWord Word { get; set; }
        IUser User { get; set; }
        int Tries { get; set; }
        int Successes { get; set; }
        ILanguage FromLanguage { get; set; }
        ILanguage ToLanguage { get; set; }
    }
}

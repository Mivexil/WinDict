using System.Collections.Generic;
using System.Drawing;

namespace Stachowski.WinDict.Interfaces
{
    public interface IWord : IEntity
    {
        string Theme { get; set; }
        Bitmap Picture { get; set; }
        IDictionary<ILanguage, string> Definitions { get; set; }
    }
}
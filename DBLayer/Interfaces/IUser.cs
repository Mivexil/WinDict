using System.Collections.Generic;

namespace Stachowski.WinDict.Interfaces
{
    public interface IUser : IEntity
    {
        string Nick { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.DBLayer.ContractImplementation
{
    public class User : IUser
    {
        internal int EntityID { get; set; }
        public string Nick { get; set; }
    }
}

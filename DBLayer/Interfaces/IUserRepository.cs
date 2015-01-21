using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stachowski.WinDict.Interfaces
{
    public interface IUserRepository
    {
        IUser GetByNick(string nick);
        IEnumerable<IUser> GetAllUsers();
        void AddUser(string nick);
        void DeleteUser(IUser user);
        void DeleteUser(string nick);
        void RenameUser(IUser user, string newNick);
    }
}
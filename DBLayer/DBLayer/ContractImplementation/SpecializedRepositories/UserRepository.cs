using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stachowski.WinDict.DBLayer.EFModel;
using Stachowski.WinDict.Interfaces;

namespace Stachowski.WinDict.DBLayer.ContractImplementation.SpecializedRepositories
{
    public class UserRepository : IUserRepository
    {
        private WinDictContext _ctx = WinDictContextFactory.Context.Value;

        internal IUser FromEntity(EFModel.User entity)
        {
            return new User { EntityID = entity.ID, Nick = entity.Nick };
        }

        internal EFModel.User ToNewEntity(IUser user)
        {
            return new EFModel.User { Nick = user.Nick };
        }

        internal EFModel.User FindEntity(IUser user)
        {
            var concrete = user as User;
            if (concrete == null) throw new ArgumentException("Invalid language.");
            return _ctx.Users.Find(concrete.EntityID);
        }
        
        public IUser GetByNick(string nick)
        {
            var user = _ctx.Users.FirstOrDefault(x => x.Nick == nick);
            if (user == null) return null;
            return FromEntity(user);
        }

        public IEnumerable<IUser> GetAllUsers()
        {
            return _ctx.Users.ToList().Select(FromEntity);
        }

        public void AddUser(string nick)
        {
            if (String.IsNullOrEmpty(nick)) throw new ArgumentException("Nickname cannot be empty");
            if (_ctx.Users.Any(x => x.Nick == nick)) throw new ArgumentException("User already exists");
            _ctx.Users.Add(new EFModel.User {Nick = nick});
            _ctx.SaveChanges();
        }

        public void DeleteUser(IUser user)
        {
            var userEntity = FindEntity(user);
            if (userEntity != null)
            {
                _ctx.Statistics.RemoveRange(_ctx.Statistics.Where(x => x.User.ID == userEntity.ID));
                _ctx.Users.Remove(userEntity);
            }
            _ctx.SaveChanges();
        }

        public void DeleteUser(string nick)
        {
            var userEntity = _ctx.Users.FirstOrDefault(x => x.Nick == nick);
            if (userEntity != null) _ctx.Users.Remove(userEntity);
            _ctx.SaveChanges();
        }

        public void RenameUser(IUser user, string newNick)
        {
            if (newNick == null) throw new ArgumentNullException("newNick");
            var userEntity = FindEntity(user);
            if (userEntity == null) throw new ArgumentException("Invalid user.");
            userEntity.Nick = newNick;
            _ctx.SaveChanges();
        }
    }
}

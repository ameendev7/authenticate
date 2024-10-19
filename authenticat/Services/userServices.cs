using authenticat.Models;

namespace authenticat.Services
{
    public interface IUser
    {
        void Register(user user);
        user Login(Login user);
        List<user> GetUsers();
    }
    public class UserServices : IUser
    {
        List<user> users = new List<user>()
        {
            new user(){Id=20 , UserName="admin" , password="23456", Role="admin"}
        };
        public void Register(user user)
        {
            user.Id = users.Count + 1;
            users.Add(user);
        }

        public user Login(Login user)
        {
            var us = users.FirstOrDefault(x => x.UserName == user.UserName && x.password == user.Password);
            return us;
        }
        public List<user> GetUsers()
        {
            return users;
        }
    }
}
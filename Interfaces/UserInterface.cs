using hw1.Models;
using System;
using Microsoft.IdentityModel.Tokens;


namespace hw1.Interfaces
{
    public interface IUser{
        public SecurityToken login(User user);
        public  SecurityToken GenerateBadge( User user);
        public List<User> GetAll();
        public User Get(string password);
        public bool Update(string password, User newUser);
        public bool Delete(string password);


    }
}
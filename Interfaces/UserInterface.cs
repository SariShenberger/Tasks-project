using tasks.Models;
using Microsoft.IdentityModel.Tokens;


namespace tasks.Interfaces;

    public interface IUser{
        public SecurityToken login(User user);
        public List<User> GetAll();
        public User Get(string password);
        public void Add(User user);
        public bool Update(string password, User newUser);
        public bool Delete(string password);


    }

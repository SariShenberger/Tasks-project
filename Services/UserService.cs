using System;
using System.Security.Claims;
using hw1.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using hw1.Interfaces;

namespace hw1.Services
{
    public class UserService : IUser
    {

        public List<User> users = new List<User>();
        private IWebHostEnvironment webHost;
        private string filePath;


        public UserService(IWebHostEnvironment webHost)
        {
           this.webHost = webHost;
           this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "Users.json");
           using (var jsonFile = File.OpenText("Data/Users.json"))
            {
                this.users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        private void saveToFile()
                {
                    File.WriteAllText(filePath, JsonSerializer.Serialize(users));
                }

        public SecurityToken login(User user)
        {
            var dt = DateTime.Now;
            if (user.UserName != "Sari"
            || user.Password != $"S{dt.Year}#{dt.Day}!"
            || user.Admin == false)
            {
                User findUser = users.Where(u => u.Password == user.Password).FirstOrDefault();
                if (findUser != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("type", "User"),
                        new Claim("password", user.Password),
                        // new Claim("ClearanceLevel", user.ClearanceLevel.ToString()),
                    };

                    var token = TaskTokenService.GetToken(claims);
                    return token;
                }
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim("type", "Admin"),
                    new Claim("password", user.Password),
                };
                var token = TaskTokenService.GetToken(claims);
                return token;
            }
            return null;
        }
        public SecurityToken GenerateBadge(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("type", "User"),
                new Claim("password", user.Password),
                // new Claim("ClearanceLevel", user.ClearanceLevel.ToString()),
            };
            var token = TaskTokenService.GetToken(claims);
            return token;
        }

         public List<User> GetAll()
        {
            return users;
        }


        public User Get(string password)
        {
            return GetAll().FirstOrDefault(i => i.Password == password);
        }
        public void Add(User user)
        {
            user.Password = users.Max(i => i.Password) + 1;
            users.Add(user);
            saveToFile();
        }
        public bool Update(string password, User newUser)
        {
            if (newUser.Password != password)
                return false;
            var user = users.FirstOrDefault(i => i.Password == newUser.Password);
            if (user != null)
            {
                user.UserName = newUser.UserName;
                saveToFile();
                return true;
            }
            return false;
        }
        public bool Delete(string password)
        {
            var user = users.FirstOrDefault(i => i.Password == password);
            if (user == null)
                return false;
            users.Remove(user);
            saveToFile();
            return true;
        }


    }

}

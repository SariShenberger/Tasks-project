using System.Security.Claims;
using tasks.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using tasks.Interfaces;

namespace tasks.Services
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
            if (user.Admin == false)
            {
                User findUser = users.Where(u => u.Password == user.Password).FirstOrDefault();
                if (findUser != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("type", "User"),
                        new Claim("password", user.Password),
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

        public List<User> GetAll()
        {
            return users;
        }

        public User Get(string password)
        {
            return GetAll().FirstOrDefault(i => i.Password == password);
        }
        public bool Add(User user)
        {
            if (user.UserName != null && user.Password != null)
            {
                User find = users.FirstOrDefault((u) => u.UserName == user.UserName && u.Password == user.Password);
                if (find != null)
                {


                    users.Add(user);
                    saveToFile();
                }
                return true;
            }return false;
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

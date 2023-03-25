using hw1.Models;
using hw1.Interfaces;
using System.Text.Json;



namespace hw1.Services
{
    public class TaskService : IConnect
    {
        public List<Item> tasks = new List<Item>();
        private IWebHostEnvironment webHost;
        private string filePath;


        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "task.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<Item>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }

        public List<Item> GetAll(string password)
        {
            return tasks.Where(t => t.UserPassword == password).ToList();
        }


        public Item Get(string password, int id)
        {
            return GetAll(password).FirstOrDefault(i => i.Id == id);
        }
        public void Add(Item item)
        {
            item.Id = tasks.Max(i => i.Id) + 1;
            tasks.Add(item);
            saveToFile();
        }
        public bool Update(int id, Item newItem)
        {
            if (newItem.Id != id)
                return false;
            var item = tasks.FirstOrDefault(i => i.Id == newItem.Id);
            if (item != null)
            {
                item.Name = newItem.Name;
                item.DoneOrNot = newItem.DoneOrNot;
                saveToFile();
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var item = tasks.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return false;
            tasks.Remove(item);
            saveToFile();
            return true;
        }


    }
}


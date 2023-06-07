using tasks.Models;

namespace tasks.Interfaces
{
    public interface IConnect{
        public List<Item> GetAll(string password);
        public Item Get( int id);
        public void Add(Item item);
        public bool Update(int id, Item newItem);
        public bool Delete(int id);
    }
}
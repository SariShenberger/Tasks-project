using hw1.Models;
using System;

namespace hw1.Interfaces
{
    public interface IConnect{
        public List<Item> GetAll();
        public Item Get(int id);
        public void Add(Item item);
        public bool Update(int id, Item newItem);
        public bool Delete(int id);
    }
}
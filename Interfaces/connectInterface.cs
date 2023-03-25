using hw1.Models;
using System;

namespace hw1.Interfaces
{
    public interface IConnect{
        public List<Item> GetAll(string password);
        public Item Get(string password , int id);
        public void Add(Item item);
        public bool Update(int id, Item newItem);
        public bool Delete(int id);
    }
}
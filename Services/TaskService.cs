using hw1.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using hw1.Interfaces;


namespace hw1.Services
{
public   class TaskService : IConnect{
    private   List<Item> items=new List<Item>{
        new Item{Id=1, Name="hw1", DoneOrNot=true},
        new Item{Id=2, Name="hw2", DoneOrNot=false},
        new Item{Id=3, Name="hw3", DoneOrNot=true}
    };

    public List<Item> GetAll()=>items;

    public Item Get(int id){
        return items.FirstOrDefault(i=>i.Id==id);
    }
    public void Add(Item item){
        item.Id=items.Max(i=>i.Id)+1;
        items.Add(item);
    }
    public bool Update(int id, Item newItem){
        if(newItem.Id != id)
            return false;
        var item=items.FirstOrDefault(i=>i.Id==newItem.Id);
        item.Name=newItem.Name;
        item.DoneOrNot=newItem.DoneOrNot;
        return true;
    }
    public bool Delete(int id){
        var item=items.FirstOrDefault(i=>i.Id==id);
        if(item==null)
            return false;
        items.Remove(item);
        return true;
    }
}}
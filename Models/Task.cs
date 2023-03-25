using System;
using Microsoft.IdentityModel.Tokens;

namespace hw1.Models
{
    public class Item
    {
        public int Id {get;set;}
        public string Name { get; set; }="";
        public bool DoneOrNot {get;set;} 
        public string UserPassword {get;set;}="";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQtoObjects
{
    class Pet
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        private string _lastName = "dog";

        public Pet(string name, int age, int id)
        {
            Name = name;
            Age = age;
            Id = id;
        }
    }
}

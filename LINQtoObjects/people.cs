using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQtoObjects
{
    class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        private string _lastName = "family";

        public People(string name, int age, int id)
        {
            Name = name;
            Age = age;
            Id = id;
        }
    }
}

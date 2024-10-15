using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    internal class Person
    {
        public Person(int id)
        {
            this.id = id;
        }
        public int id { get; set; }
        public Person Prev { get; set; }
        public int ShoppingTime { get; set; }
        public int timeInQueue { get; set; }
        public int idQueue { get; set; }
        public int Estimation { get; set; }
    }
}

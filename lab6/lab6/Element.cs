using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    internal class Element
    {
        public Element(int id)
        {
            this.id = id;
        }
        public int id { get; set; }
        public Element Prev { get; set; }
        public int time_shop { get; set; }
        public int time_cash { get; set; }
        public int idQueue { get; set; }
        public int Estimation { get; set; }
    }
}


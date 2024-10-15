using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    internal class Queue
    {
        public int count = 0, id, time_pop=0;
        
        Element first;
        Element last;
        public void Push(Element Element)
        {
            if (Element != null)
            {
                Element.idQueue = id;
                Element oldElement = last;
                last = Element;

                if (count == 0)
                {
                    first = last;
                }
                else
                {
                    oldElement.Prev = last;
                }
                count++;
            }
        }
        public Element Pop()
        {

            if (count == 0)
                return null;
            Element popd = first;
            first = first.Prev;
            count--;
            return popd;
        }

        public void CurrentTime() //+1
        {
            Element current = first;
            while (current != null)
            {
                current.time_cash++;
                current = current.Prev;
            }
        }

        public List<Element> getListOfPeople()
        {
            Element current = first;
            List<Element> list = new List<Element>();
            while (current != null)
            {
                list.Add(current);
                current = current.Prev;
            }
            return list;
        }
    }
}

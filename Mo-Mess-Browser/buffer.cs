using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo_Mess_Browser
{
    public class buffer<T> : System.Collections.Generic.List<T>
    {
        private int size;
        public bool dataInBuffer = false;
        public buffer( int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException("Der Buffer muss mindestens ein Element enthalten können");
            this.size = size;
        }
        public void add(T value)
        {
            if(this.Count >= size)
            {
                this.RemoveAt(0);
                this.Add(value);
            }else
            {
                this.Add(value);
            }
            dataInBuffer =  true;
            
        }
        public T read()
        {
            T value = this[0];
            this.RemoveAt(0);
            dataInBuffer = (this.Count > 0) ? true : false;
            return value;
            
        }
    }
}

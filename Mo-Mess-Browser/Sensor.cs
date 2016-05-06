using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Mo_Mess
{
   public class SensorProvider
    {
        public enum type { Volt = 0, Amp  = 1, temp = 2, Generic = 3 }
        public string name;
        public IPAddress address;
        public int id;
        public type Type;
       
        public SensorProvider(String Name, String Address, int id, type t)
        {
            this.Type = t;
            this.name = Name;
            this.address = IPAddress.Parse(Address);
            this.id = id;
        }

        public byte[] createByteArrayToSend()
        {
            byte[] a = Encoding.ASCII.GetBytes(this.name);
            byte[] b = Encoding.ASCII.GetBytes(id.ToString());
            byte c = Convert.ToByte(Type);
            byte[] g = new byte[201];
            for(int i = 0; i<a.Length && i<100; i++)
            {
                g[i] = a[i];
            }
            for(int i = 0; i<b.Length &&i<100; i++)
            {
                g[i + 100] = b[i];
            }
            g[200] = c;
            


            return g;
        }

        public static SensorProvider SensorFromByteArray(byte[] input, String ip)
        {
            byte[] a  =new byte[100];
            Array.Copy(input, a, 100);
            byte[] b = new byte[100];
            Array.Copy(input, 100, b, 0, 100);
            byte c = input[200];
            type t = (type)c;

            SensorProvider s = new SensorProvider(Encoding.ASCII.GetString(a).Replace("\0", ""),ip,int.Parse(Encoding.ASCII.GetString(b)),t);

            return s;
        }

    }
}

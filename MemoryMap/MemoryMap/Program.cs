using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMap
{
    class Program
    {
        static void Main(string[] args)
        {

            var intlist = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                intlist.Add(i);
            }

            var x = new MemoryManager();

            var y = x.WriteRead(intlist);


            var indata = new SerializableData() { Name = "Name", Value = 1, Data = new SerializableData.zData() { InnerName = "InnerName", InnerValue = 2 } };

            var outdata = x.WriteRead(indata);

            

        }
    }
}

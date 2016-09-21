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
            var x = new MemoryMapper("test");

            var intlist = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                intlist.Add(i);
            }
            byte[] inarray = ByteConverter.ToByteArray(intlist);
            long size = inarray.LongLength; //Marshal.SizeOf(inarray);
            x.Write(0, inarray, 0);

            byte[] outarray = x.Read(0, 0, size);
            var outcontent = ByteConverter.FromByteArray<List<int>>(outarray);
        }
    }
}

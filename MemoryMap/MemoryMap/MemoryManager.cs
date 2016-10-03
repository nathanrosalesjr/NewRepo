using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMap
{
    public class MemoryManager
    {
        MemoryMapper _memoryMapper;

        public MemoryManager()
        {
            Init();
        }

        public void Init()
        {
            _memoryMapper = new MemoryMapper("test");
        }

        public T WriteRead<T>(T o)
        {
            byte[] inarray = ByteConverter.ToByteArray(o);
            long size = inarray.LongLength;
            _memoryMapper.Write(0, inarray, 0);

            byte[] outarray = _memoryMapper.Read(0, 0, size);
            T outobject = ByteConverter.FromByteArray<T>(outarray);
            return outobject;
        }
    }
}

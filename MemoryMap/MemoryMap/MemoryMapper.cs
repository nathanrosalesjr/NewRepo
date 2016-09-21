using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMap
{
    public class MemoryMapper : IDisposable
    {
        bool _disposed = false;
        string _name;
        MemoryMappedFile[] handles;
        public MemoryMapper(string name)
        {
            _name = name;
            Init();
        }
        public void Init()
        {
            handles = new MemoryMappedFile[1];
            handles[0] = (MemoryMappedFile.CreateOrOpen(_name, 10485760));
        }

        public byte[] Read(int index, long offset, long size)
        {
            var buffer = new byte[size];
            using (var view = handles[index].CreateViewAccessor(offset, size, MemoryMappedFileAccess.Read))
            {
                view.ReadArray<byte>(0, buffer, 0, (int)size);
            }
            return buffer;
        }

        public void Write(int index, byte[] buffer, long offset)
        {
            using (var view = handles[index].CreateViewAccessor(offset, buffer.LongLength, MemoryMappedFileAccess.Write))
            {
                view.WriteArray<byte>(0, buffer, 0, buffer.Length);
            }
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            
            if (disposing)
            {
            }

            foreach(var handle in handles)
            {
                handle.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MemoryMapper()
        {
            Dispose(false);
        }


        
    }
}

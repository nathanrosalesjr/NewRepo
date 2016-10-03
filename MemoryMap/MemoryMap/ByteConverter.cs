using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMap
{
    public class ByteConverter
    {
        public static byte[] ToByteArray(object obj)
        {
            if (obj == null)
                return null;

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();

            formatter.Serialize(stream, obj);

            return stream.ToArray();
        }

        public static T FromByteArray<T>(byte[] buffer)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            stream.Write(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);
            T obj = (T)formatter.Deserialize(stream);

            return obj;
        }
    }
}

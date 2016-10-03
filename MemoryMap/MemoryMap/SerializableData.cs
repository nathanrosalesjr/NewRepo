using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMap
{
    [Serializable]
    public class SerializableData
    {
        public string Name;
        public int Value;
        public zData Data;

        [Serializable]
        public struct zData
        {
            public string InnerName;
            public int InnerValue;
        }
    }
}

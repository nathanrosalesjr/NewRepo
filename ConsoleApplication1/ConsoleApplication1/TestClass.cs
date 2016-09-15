using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class TestClass
    {
        private static int num = 0;
        public static int Num => num != 0 ? num : (num = Foo());
        private static int Foo()
        {
            return 5;
        }
    }
}

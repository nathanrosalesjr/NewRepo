using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class ExternalClass
    {
        private static Stopwatch sw = new Stopwatch();
        private static Task t = new Task(() => { while (true) { ExternalClass.i++; } });
        private static int i = 0;
        public ExternalClass()
        {
            TestPlugin.ExternalClassCount++;
        }

        public void Tick()
        {
            //if (t.Status != TaskStatus.Running)
            //{
            //    t.Start();
            //}
            //if (sw.Elapsed.TotalSeconds > 10)
            //{
            //    sw.Restart();
            //}
        }


    }
}

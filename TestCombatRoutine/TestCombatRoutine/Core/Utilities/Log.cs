using Styx.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Core.Utilities
{
    public class Log
    {
        private static string lastCombatMsg;
        public static void CombatLog(string message, params object[] args)
        {
            if (message == lastCombatMsg)
                return;
            Logging.Write(Colors.OrangeRed, $"[Bacon] {string.Format(message, args)}");
            lastCombatMsg = message;
        }
        public static void DiagnosticLog(string message, params object[] args)
        {
            if (message != null)
            {
                Logging.WriteDiagnostic(Colors.Firebrick, $"{string.Format(message, args)}");
            }
        }
    }
}

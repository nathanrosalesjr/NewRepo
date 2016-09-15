using Styx.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BrewceLi.Core.Utilities
{
    class Log
    {
        #region [Method] - Combat Log
        public static string lastCombatMSG;

        public static void combatLog(string Message, params object[] args)
        {
            if (Message == lastCombatMSG)
                return;
            Logging.Write(Colors.MediumSpringGreen, "[Brewce Li] {0}", String.Format(Message, args));
            lastCombatMSG = Message;
        }
        #endregion

        #region [Method] - Diagnostic Log
        public static void diagnosticLog(string Message, params object[] args)
        {
            if (Message == null)
                return;
            Logging.WriteDiagnostic(Colors.MediumSpringGreen, "{0}", String.Format(Message, args));
        }
        #endregion
    }
}

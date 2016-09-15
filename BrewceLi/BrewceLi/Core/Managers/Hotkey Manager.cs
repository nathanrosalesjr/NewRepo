using Styx.Common;
using Styx.WoWInternals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace BrewceLi.Core.Managers
{
    class Hotkey_Manager
    {
        public static bool aoeOn { get; set; }
        public static bool emergencyOn { get; set; }
        public static bool manualOn { get; set; }
        public static bool keysRegistered { get; set; }

        #region [Method] - Hotkey Registration
        public static void registerHotKeys()
        {
            if (keysRegistered)
                return;
            HotkeysManager.Register("aoeOn", Keys.Q, ModifierKeys.Alt, ret =>
            {
                aoeOn = !aoeOn;
                Lua.DoString(aoeOn ? @"print('AoE Mode: \124cFF15E61C Enabled!')" : @"print('AoE Mode: \124cFFE61515 Disabled!')");
            });
            HotkeysManager.Register("emergencyOn", Keys.E, ModifierKeys.Alt, ret =>
            {
                emergencyOn = !emergencyOn;
                Lua.DoString(emergencyOn ? @"print('Emergency Healing: \124cFF15E61C Enabled!')" : @"print('Emergency Healing: \124cFFE61515 Disabled!')");
            });
            HotkeysManager.Register("manualOn", Keys.S, ModifierKeys.Alt, ret =>
            {
                manualOn = !manualOn;
                Lua.DoString(manualOn ? @"print('Manual Mode: \124cFF15E61C Enabled!')" : @"print('Manual Mode: \124cFFE61515 Disabled!')");
            });
            keysRegistered = true;
            Lua.DoString(@"print('Hotkeys: \124cFF15E61C Registered!')");
            Logging.Write(Colors.Red, "Hotkeys: Registered!");
        }
        #endregion

        #region [Method] - Hotkey Removal
        public static void removeHotKeys()
        {
            if (!keysRegistered)
                return;
            HotkeysManager.Unregister("aoeOn");
            HotkeysManager.Unregister("emergencyOn");
            HotkeysManager.Unregister("manualOn");
            aoeOn = false;
            emergencyOn = false;
            manualOn = false;
            keysRegistered = false;
            Lua.DoString(@"print('Hotkeys: \124cFFE61515 Removed!')");
            Logging.Write(Colors.Red, "Hotkeys: Removed!");
        }
        #endregion
    }
}
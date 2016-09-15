using Styx.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCombatRoutine.Core.Managers
{
    public class HotKeyManager
    {
        public static bool AoeOn { get; set; }
        public static bool CooldownsOn { get; set; }
        public static bool ManualOn { get; set; }
        public static bool KeysRegistered { get; set; }

        public static void RegisterHotKeys()
        {
            if (KeysRegistered)
                return;
            HotkeysManager.Register("aoeOn", Keys.Q, ModifierKeys.Alt, ret =>
            {
                AoeOn = !AoeOn;
                //Lua.DoString(aoeOn ? @"print('AoE Mode: \124cFF15E61C Enabled!')" : @"print('AoE Mode: \124cFFE61515 Disabled!')");
            });
            HotkeysManager.Register("cooldownsOn", Keys.E, ModifierKeys.Alt, ret =>
            {
                CooldownsOn = !CooldownsOn;
                //Lua.DoString(cooldownsOn ? @"print('Cooldowns: \124cFF15E61C Enabled!')" : @"print('Cooldowns: \124cFFE61515 Disabled!')");
            });
            HotkeysManager.Register("manualOn", Keys.S, ModifierKeys.Alt, ret =>
            {
                ManualOn = !ManualOn;
                //Lua.DoString(manualOn ? @"print('Manual Mode: \124cFF15E61C Enabled!')" : @"print('Manual Mode: \124cFFE61515 Disabled!')");
            });
            KeysRegistered = true;
            //Lua.DoString(@"print('Hotkeys: \124cFF15E61C Registered!')");
            Logging.Write(LogLevel.Diagnostic, "Hotkeys: Registered!");
        }
        public static void RemoveHotKeys()
        {
            if (!KeysRegistered)
                return;
            HotkeysManager.Unregister("aoeOn");
            HotkeysManager.Unregister("cooldownsOn");
            HotkeysManager.Unregister("manualOn");
            AoeOn = false;
            CooldownsOn = false;
            ManualOn = false;
            KeysRegistered = false;
            //Lua.DoString(@"print('Hotkeys: \124cFFE61515 Removed!')");
            Logging.Write(LogLevel.Diagnostic, "Hotkeys: Removed!");
        }

        
    }
}

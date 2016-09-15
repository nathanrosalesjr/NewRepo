//created by alltrueist

using Styx;
using Styx.Common;
using Styx.CommonBot.Routines;
using Styx.TreeSharp;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

#region [Method] - Class Redundancy
using HKM = BrewceLi.Core.Managers.Hotkey_Manager;
using R = BrewceLi.Rotation.Rotation;
using U = BrewceLi.Core.Unit;
#endregion

namespace BrewceLi
{
    public class Main : CombatRoutine
    {
        private static readonly Version version = new Version(1, 1, 0);
        public override string Name { get { return "Brewce Li v" + version; } }
        public override WoWClass Class { get { return WoWClass.Monk; } }
        private static LocalPlayer Me { get { return StyxWoW.Me; } }

        #region [Method] - Implementations
        private Composite _preCombatBehavior, _combatBuffBehavior, _HealBehavior, _combatBehavior;
        public override Composite PreCombatBuffBehavior { get { return _preCombatBehavior ?? (_preCombatBehavior = R.preCombatBuffing()); } }
        public override Composite HealBehavior { get { return _HealBehavior ?? (_HealBehavior = R.HealBehavior()); } }
        public override Composite CombatBuffBehavior { get { return _combatBuffBehavior ?? (_combatBuffBehavior = R.combatBuffing()); } }
        public override Composite CombatBehavior { get { return _combatBehavior ?? (_combatBehavior = R.rotationSelector()); } }

        #region [Method] - Overrides
        public override void Initialize()
        {
            Logging.Write(Colors.MediumSpringGreen, "Hello {0}!", Me.Name);
            Logging.Write(Colors.White, "");
            Logging.Write(Colors.MediumSpringGreen, "For optimal performance, please use Enyo");
            Logging.Write(Colors.White, "");
            Logging.Write(Colors.MediumSpringGreen, "Current Version:");
            Logging.Write(Colors.MediumSpringGreen, "-- Brewce Li v" + version + " --");
            Logging.Write(Colors.MediumSpringGreen, "-- by alltrueist --");
            Logging.Write(Colors.MediumSpringGreen, "-- a Fistweaving CombatRoutine --");
            Logging.Write(Colors.MediumSpringGreen, "-- press Alt + E to toggle Emergency Healing Mode --");
            Logging.Write(Colors.MediumSpringGreen, "-- Use Crackling Jade Lightning instead of Jab --");
            HKM.registerHotKeys();
        }
        public override bool WantButton { get { return true; } }
        public override void OnButtonPress() { Logging.Write(Colors.MediumSpringGreen, "Coming Soon!"); }
        public override void ShutDown() { HKM.removeHotKeys(); }
        #endregion

        #region [Method] - Pulse
        public override void Pulse()
        {
            if (!StyxWoW.IsInWorld || Me == null || !Me.IsValid || !Me.IsAlive)
                return;
            if (!Me.Combat)
                return;
            U.Cache();
            U.enemyAnnex(20);
        }
        #endregion
        #endregion
    }
}

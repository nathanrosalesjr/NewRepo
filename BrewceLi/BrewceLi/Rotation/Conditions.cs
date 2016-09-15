using Styx;
using Styx.TreeSharp;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region [Method] - Class Redundancies
using Action = Styx.TreeSharp.Action;
using S = BrewceLi.Core.Spell;
using C = BrewceLi.Rotation.Conditions;
using HkM = BrewceLi.Core.Managers.Hotkey_Manager;
using SB = BrewceLi.Core.Helpers.Spell_Book;
using TM = BrewceLi.Core.Managers.Talent_Manager;
using U = BrewceLi.Core.Unit;
#endregion

namespace BrewceLi.Rotation
{
    class Conditions
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit currentTarget { get { return StyxWoW.Me.CurrentTarget; } }

        #region [Method] - Auto Attack
        private static Composite autoAttack()
        {
            return new Action(ret =>
            {
                if (!Me.IsAutoAttacking && U.isUnitValid(currentTarget, 5))
                    Lua.DoString("StartAttack()");
                return RunStatus.Failure;
            });
        }
        #endregion
    }
}

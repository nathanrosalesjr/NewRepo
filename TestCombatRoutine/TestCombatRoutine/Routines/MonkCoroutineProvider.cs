using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.CommonBot.Routines;
using Buddy.Coroutines;
using Styx.WoWInternals;
using Styx.CommonBot;
using Styx;
using Styx.WoWInternals.WoWObjects;
using TestCombatRoutine.Core.Helpers;
using TestCombatRoutine.Core.Spell;

namespace TestCombatRoutine.Routines
{
    public class MonkCoroutineProvider : ICombatCoroutineProvider
    {
        private static LocalPlayer Me => StyxWoW.Me;
        
        public async Task<bool> GetCombatCoroutine()
        {
            //if (Me.IsCasting || Me.IsChanneling || SpellManager.GlobalCooldown)
            //    return true;

            //if (await Core.Spell.HiveSpellManager.CastSpell(SpellBook.TigerPalm))
            //    return true;

            return false;
        }
    }
}

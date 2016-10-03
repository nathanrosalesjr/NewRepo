using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.WoWInternals;
using Styx.CommonBot;
using Styx.WoWInternals.WoWObjects;

namespace TestCombatRoutine.Core.Spell
{
    public class HiveSpellManager : ISpellManager
    {

        async public Task<bool> CastSpell(WoWSpell spell)
        {
            if (!SpellManager.CanCast(spell))
                return false;

            if (!SpellManager.Cast(spell))
                return false;

            return true;
        }

        async public Task<bool> CastSpell(WoWSpell spell, WoWUnit unit)
        {
            if (!SpellManager.CanCast(spell))
                return false;

            if (!SpellManager.Cast(spell, unit))
                return false;
            
            return true;
        }
    }
}

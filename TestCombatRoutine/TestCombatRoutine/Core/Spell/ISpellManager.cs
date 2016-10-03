using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Core.Spell
{
    public interface ISpellManager
    {
        Task<bool> CastSpell(WoWSpell spell);

        Task<bool> CastSpell(WoWSpell spell, WoWUnit unit);
    }
}

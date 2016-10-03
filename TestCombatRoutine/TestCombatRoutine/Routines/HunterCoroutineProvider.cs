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
    public class HunterCoroutineProvider : BaseCoroutineProvider
    {
        private static HunterCoroutineProvider instance;

        private HunterCoroutineProvider() { }

        public static HunterCoroutineProvider Instance => instance ?? (instance = new HunterCoroutineProvider());

        private static LocalPlayer Me => StyxWoW.Me;
        private static WoWUnit Target => Me.CurrentTarget;
        private double _gcd = 1.8;
        private ISpellManager _spellManager => new HiveSpellManager();

        public override async Task<bool> GetCombatCoroutine()
        {
            if (Me.GetPowerPercent(WoWPowerType.Rage) > 70)
            {
                if (await _spellManager.CastSpell(SpellBook.Maul))
                    return true;
            }

            if (Me.IsCasting || Me.IsChanneling || SpellManager.GlobalCooldown)
                return true;

            if (await AuraTimeLessThan(Target, "Thrash", _gcd))
            {
                if (await _spellManager.CastSpell(SpellBook.Thrash))
                    return true;
            }

            if (await _spellManager.CastSpell(SpellBook.Mangle))
                return true;

            if (Target.GetAuraByName("Thrash")?.StackCount < 3)
            {
                if (await _spellManager.CastSpell(SpellBook.Thrash))
                    return true;
            }


            if (SpellBook.Mangle.CooldownTimeLeft.TotalSeconds > _gcd)
            {
                if (await _spellManager.CastSpell(SpellBook.Moonfire))
                    return true;
            }

            return false;
        }
        

        
    }
}

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
using System.Diagnostics;
using TestCombatRoutine.Core.Managers;
using Styx.Common;
using System.Threading;

namespace TestCombatRoutine.Routines
{
    public class PriestCoroutineProvider : BaseCoroutineProvider, IPulseManager
    {
        private static PriestCoroutineProvider instance;

        private PriestCoroutineProvider() { sw.Start(); ContextManager.Complian("started"); }

        public static PriestCoroutineProvider Instance => instance ?? (instance = new PriestCoroutineProvider());
        private static LocalPlayer Me => StyxWoW.Me;
        private static WoWUnit Target => Me.CurrentTarget;
        private double _gcd = 1.8;
        private uint latency = 0;
        private Stopwatch sw = new Stopwatch();
        private ISpellManager _spellManager => new HiveSpellManager();

        public override async Task<bool> GetCombatCoroutine()
        {

            if (Me.IsCasting || Me.IsChanneling || SpellManager.GlobalCooldown)
                return true;

            //Keep shield on tank
            //Penance on CD - dps or heals
            //Plea for someone < 80
            //SW:P mobs > 80 hp
            //Smite

            await Heal(HealingManager.PreferredTargets);
            await Heal(HealingManager.Targets);


            //if (await _spellManager.CastSpell(SpellBook.PowerWordShield))
            //    return true;

            //if (Me.HealthPercent < 70)
            //{
            //    if (await _spellManager.CastSpell(SpellBook.Penance))
            //        return true;


            //    if (await _spellManager.CastSpell(SpellBook.Plea))
            //        return true;

            //}

            return false;
        }

        private async Task<bool> Dps()
        {
            return true;
        }

        private async Task<bool> Heal(List<WoWPlayer> targets)
        {
            WoWUnit healTarget;

            if (SpellBook.PowerWordShield.CooldownTimeLeft.TotalMilliseconds < latency)
            {
                healTarget = targets.Where(p => p.HealthPercent < 60).FirstOrDefault();
                if (healTarget != null)
                    if (await _spellManager.CastSpell(SpellBook.PowerWordShield, healTarget))
                        return true;
            }

            if (SpellBook.Penance.CooldownTimeLeft.TotalMilliseconds < latency)
            {
                healTarget = targets.Where(p => p.HealthPercent < 35).FirstOrDefault();
                if (healTarget != null)
                    if (await _spellManager.CastSpell(SpellBook.Penance, healTarget))
                        return true;
            }

            healTarget = targets.Where(p => p.HealthPercent < 85).FirstOrDefault();
            if (healTarget != null)
                if (await _spellManager.CastSpell(SpellBook.Plea, healTarget))
                    return true;


            return false;
        }



        private int healingPriority;


        private enum HealStates
        {
            EmergencyHealing,
            MovingAndHealing,
            DpsAndHeal
        }

        bool doExtra = false;
        int pulseCount = 0;

        public void Pulse()
        {
            pulseCount++;

            SetHealState();

            if (sw.Elapsed.TotalMilliseconds > 999)
            {
                Logging.Write($"{pulseCount} Pulses last second");
                pulseCount = 0;
                Logging.Write($"{ContextManager.CacheUpdateCount} updates last second");
                ContextManager.CacheUpdateCount = 0;

                int i = 0;
                if (i == 0)
                {
                    //var unit = Me.CurrentTarget;
                    //if (!ContextManager.IsObserving(unit.Guid))
                    //    ContextManager.AddToObservedUnits(unit);
                }
                sw.Restart();
            }

            if (doExtra) { Thread.Sleep(500); }
            //var bars = Styx.CommonBot.Bars.ActionBar.Active;
            //latency = StyxWoW.WoWClient.Latency;
            //HealingManager.UpdateTargets();
            //var x = HealingManager.Targets;
            //var y = HealingManager.PreferredTargets;
        }

        private async Task<bool> SetHealState()
        {
            return true;
        }
    }
}

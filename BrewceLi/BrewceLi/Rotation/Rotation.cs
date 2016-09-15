using CommonBehaviors.Actions;
using Styx.TreeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx;
using Styx.WoWInternals.WoWObjects;

#region [Method] - Class Redundancies
using S = BrewceLi.Core.Spell;
using C = BrewceLi.Rotation.Conditions;
using HkM = BrewceLi.Core.Managers.Hotkey_Manager;
using SB = BrewceLi.Core.Helpers.Spell_Book;
using TM = BrewceLi.Core.Managers.Talent_Manager;
using U = BrewceLi.Core.Unit;
using Styx.WoWInternals;
#endregion

namespace BrewceLi.Rotation
{
    class Rotation
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit currentTarget { get { return StyxWoW.Me.CurrentTarget; } }

        #region [Method] - Combat Buffing
        public static Composite combatBuffing()
        {
            return new PrioritySelector(
                new Decorator(ret => HkM.manualOn || !Me.IsAlive || !Me.GotTarget, new ActionAlwaysSucceed()),
                S.dropCast(SB.spellJadeSerpentStatue, nearTarget => currentTarget, ret => U.isUnitValid(currentTarget, 20) && U.MyStatue == null),
                S.dropCast(SB.spellJadeSerpentStatue, nearTarget => Me, ret => U.MyStatue == null),
                S.Buff(SB.spellManaTea, ret => U.needManaTea),
                S.Buff(SB.spellManaTeaGlyph, ret => U.glyphedManaTea),
                S.Buff(SB.spellChiBrew, ret => U.Chi < 3 && U.ManaTea <= 18),
                S.Buff(SB.spellThunderFocusTea, ret => U.VitalMists == 5),
                S.Cast(SB.spellXuen, ret => Me.CurrentTarget.IsBoss)
                );


        } 
        #endregion

        #region [Method] - Heal Behavior
        public static Composite HealBehavior()
        {
            return new PrioritySelector(
                new Decorator(ret => HkM.manualOn || !Me.IsAlive || !Me.GotTarget, new ActionAlwaysSucceed()),
                S.Buff(SB.spellSurgingMist, ret => U.VitalMists == 5),
                //S.blindCast(SB.spellDetonateChi, ret => Me.GroupInfo.IsInParty),
                S.Buff(SB.spellDiffuseMagic, ret => Me.HealthPercent <= 35),
                S.Buff(SB.spellDampenHarm, ret => Me.HealthPercent <= 35),
                S.Buff(SB.spellFortifyingBrew, ret => Me.HealthPercent <= 50),
                S.Buff(SB.spellLifeCocoon, ret => Me.HealthPercent <= 70)
                );
        }
        #endregion

        #region [Method] - Precombat Buffing
        public static Composite preCombatBuffing()
        {
            return new PrioritySelector(
                new Decorator(ret => HkM.manualOn || !Me.IsAlive || Me.IsCasting || Me.IsChanneling || Me.Mounted || Me.OnTaxi, new ActionAlwaysSucceed()),
                S.Buff(SB.spellLegacyoftheEmperor, ret => !U.auraExists(Me, 1126) && !U.auraExists(Me, 115921) && !U.auraExists(Me, 20217) && !U.auraExists(Me, 90363) && !U.auraExists(Me, 117666)),
                S.dropCast(SB.spellJadeSerpentStatue, nearTarget => currentTarget, ret => U.isUnitValid(currentTarget, 20) && U.MyStatue == null),
                S.Buff(SB.spellStanceoftheSpiritedCrane, ret => !U.auraExists(Me, SB.auraStanceoftheSpiritedCrane))
                );
        } 
        #endregion

        #region [Method] - Rotation Selector
        public static Composite rotationSelector()
            {
                return new PrioritySelector(
                    new Decorator(ret => HkM.manualOn || !Me.IsAlive || !Me.GotTarget, new ActionAlwaysSucceed()),
                    new Switch<bool>(ret => HkM.emergencyOn,
                        new SwitchArgument<bool>(true,
                            new PrioritySelector(
                                S.Cast(SB.spellTouchofDeath, ret => Me.CurrentTarget.CurrentHealth <= Me.CurrentHealth || (Me.CurrentTarget.IsBoss && Me.CurrentTarget.HealthPercent <= 10)),
                                S.Cast(SB.spellCracklingJadeLightning, ret => U.auraExists(Me, SB.auraLucidity) && U.Chi <= 1),
                                S.Cast(SB.spellChiWave),
                                S.Cast(SB.spellRSK, ret => U.auraTimeLeft(Me.CurrentTarget, SB.auraRSK) <= 5000),
                                S.Cast(SB.spellTigerPalm, ret => U.auraTimeLeft(Me, SB.auraTigerPower) <= 6000),
                                S.Cast(SB.spellBlackoutKick, ret => U.auraTimeLeft(Me, SB.auraTigerPower) > 6000 && U.auraTimeLeft(Me.CurrentTarget, SB.auraRSK) > 5000),
                                S.blindCast(SB.spellRushingJadeWind, ret => U.Chi <= 4 && (U.activeEnemies(Me.Location, 8).Count() >= 3 || HkM.aoeOn)),
                                S.blindCast(SB.spellSpinningCraneKick, ret => U.Chi <= 4 && (U.activeEnemies(Me.Location, 8).Count() >= 3 || HkM.aoeOn)),
                                S.Cast(SB.spellExpelHarm, ret => U.Chi <= 4),
                                S.Cast(SB.spellCracklingJadeLightning, ret => U.Chi <= 1)
                                )),
                        new SwitchArgument<bool>(false,
                            new PrioritySelector(
                                S.Cast(SB.spellTouchofDeath, ret => Me.CurrentTarget.CurrentHealth <= Me.CurrentHealth || (Me.CurrentTarget.IsBoss && Me.CurrentTarget.HealthPercent <= 10)),
                                S.Cast(SB.spellCracklingJadeLightning, ret => U.auraExists(Me, SB.auraLucidity) && U.Chi <= 1),
                                S.Cast(SB.spellChiWave),
                                S.Cast(SB.spellRSK, ret => U.auraTimeLeft(Me.CurrentTarget, SB.auraRSK) <= 5000),
                                S.Cast(SB.spellTigerPalm, ret => U.auraTimeLeft(Me, SB.auraTigerPower) <= 6000),
                                S.Cast(SB.spellBlackoutKick, ret => U.auraTimeLeft(Me, SB.auraTigerPower) > 6000 && U.auraTimeLeft(Me.CurrentTarget, SB.auraRSK) > 5000),
                                S.blindCast(SB.spellRushingJadeWind, ret => U.Chi <= 4 && (U.activeEnemies(Me.Location, 8).Count() >= 3 || HkM.aoeOn)),
                                S.blindCast(SB.spellSpinningCraneKick, ret => U.Chi <= 4 && (U.activeEnemies(Me.Location, 8).Count() >= 3 || HkM.aoeOn)),
                                S.Cast(SB.spellExpelHarm, ret => U.Chi <= 4),
                                S.Cast(SB.spellJab, ret => U.Chi <= 4)
                                ))));
            } 
        #endregion

    }

}

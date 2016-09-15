using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region [Method] - Class Redundancy
using L = BrewceLi.Core.Utilities.Log;
using SB = BrewceLi.Core.Helpers.Spell_Book;
using Styx.Common;
#endregion

namespace BrewceLi.Core
{
    static class Unit
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit currentTarget { get { return StyxWoW.Me.CurrentTarget; } }

        #region [Method] - Active Enemies
        public static IEnumerable<WoWUnit> activeEnemies(WoWPoint fromLocation, double Range)
        {
            var Hostile = enemyCount;
            return Hostile != null ? Hostile.Where(x => x.Location.DistanceSqr(fromLocation) < Range * Range) : null;
        }

        private static List<WoWUnit> enemyCount { get; set; }

        public static void enemyAnnex(double Range)
        {
            enemyCount.Clear();
            foreach (var u in surroundingEnemies())
            {
                if (u == null || !u.IsValid)
                    continue;
                if (!u.IsAlive || u.DistanceSqr > Range * Range)
                    continue;
                if (!u.Attackable || !u.CanSelect)
                    continue;
                if (u.IsFriendly)
                    continue;
                if (u.IsNonCombatPet || u.IsCritter)
                    continue;
                enemyCount.Add(u);
            }
        }

        private static IEnumerable<WoWUnit> surroundingEnemies() { return ObjectManager.GetObjectsOfTypeFast<WoWUnit>(); }

        static Unit() { enemyCount = new List<WoWUnit>(); }
        #endregion

        #region [Method] - Aura Detection
        public static bool auraExists(this WoWUnit Unit, int auraID, bool isMyAura = false)
        {
            try
            {
                if (Unit == null || !Unit.IsValid)
                    return false;
                WoWAura Aura = isMyAura ? Unit.GetAllAuras().FirstOrDefault(A => A.SpellId == auraID && A.CreatorGuid == Me.Guid) : Unit.GetAllAuras().FirstOrDefault(A => A.SpellId == auraID);
                return Aura != null;
            }
            catch (Exception xException)
            {
                L.diagnosticLog("Exception in auraExists(): ", xException);
                return false;
            }
        }

        public static uint auraStacks(this WoWUnit Unit, int auraID, bool isMyAura = false)
        {
            try
            {
                if (Unit == null || !Unit.IsValid)
                    return 0;
                WoWAura Aura = isMyAura ? Unit.GetAllAuras().FirstOrDefault(A => A.SpellId == auraID && A.CreatorGuid == Me.Guid) : Unit.GetAllAuras().FirstOrDefault(A => A.SpellId == auraID);
                return Aura != null ? Aura.StackCount : 0;
            }
            catch (Exception xException)
            {
                L.diagnosticLog("Exception in auraStacks(): ", xException);
                return 0;
            }
        }

        public static double auraTimeLeft(this WoWUnit Unit, int auraID, bool isMyAura = false)
        {
            try
            {
                if (Unit == null || !Unit.IsValid)
                    return 0;
                WoWAura Aura = isMyAura ? Unit.GetAllAuras().FirstOrDefault(A => A.SpellId == auraID && A.CreatorGuid == Me.Guid) : Unit.GetAllAuras().FirstOrDefault(A => A.SpellId == auraID);
                return Aura != null ? Aura.TimeLeft.TotalMilliseconds : 0;
            }
            catch (Exception xException)
            {
                L.diagnosticLog("Exception in auraTimeLeft(): ", xException);
                return 9999;
            }
        }
        #endregion

        #region [Method] - Cache
        public static bool Lucidity;
        public static bool CranesZeal;
        public static bool TigerPower;
        public static bool MuscleMemory;
        public static bool RenewingMist;
        public static bool StanceoftheSpiritedCrane;
        public static bool LegacyoftheEmperor;
        public static bool RisingSunKick;

        public static uint VitalMists;
        public static uint ManaTea;

        public static uint Chi;


        public static bool needManaTea;
        public static bool glyphedManaTea;

        #region [Method] - Consolidated Universal Buffs
        public static bool noMasteryBuff;
        public static bool MegaHasteBuff;
        public static bool noStatsBuff;
        public static bool noStaminaBuff;
        public static bool noAPBuff;
        public static bool noSPBuff;
        public static bool noMHasteBuff;
        public static bool noSHasteBuff;
        public static bool noCritBuff;
        #endregion

        //public static bool auraOnMe;
        //public static bool auraOnTarget;
        //public static uint auraStackOnMe;
        //public static uint auraStackOnTarget;
        //public static uint mainResource;

        public static void Cache()
        {
            Lucidity = auraExists(Me, SB.auraLucidity);
            CranesZeal = auraExists(Me, SB.auraCranesZeal);
            TigerPower = auraExists(Me, SB.auraTigerPower);
            StanceoftheSpiritedCrane = auraExists(Me, SB.auraStanceoftheSpiritedCrane);
            LegacyoftheEmperor = auraExists(Me, SB.auraLegacyoftheEmperor);
            RisingSunKick = auraExists(Me.CurrentTarget, SB.auraRSK);
            VitalMists = auraStacks(Me, SB.auraVitalMists);
            ManaTea = auraStacks(Me, SB.auraManaTea);
            Chi = Me.CurrentChi;
            glyphedManaTea = ManaTea >= 2 && Me.ManaPercent <= 90;
            needManaTea = (ManaTea >= 4 && Me.ManaPercent <= 80) || (ManaTea >= 9 && Me.ManaPercent <= 60) || 
                            (ManaTea >= 14 && Me.ManaPercent <= 40) || (ManaTea >= 19 && Me.ManaPercent <= 20);

            #region [Method] - Consolidated Universal Buffs
            noMasteryBuff = (!auraExists(Me, 19740) && !auraExists(Me, 116956) && !auraExists(Me, 93435) && !auraExists(Me, 128997) && !auraExists(Me, 155522));
            MegaHasteBuff = (auraExists(Me, 80353) || auraExists(Me, 2825) || auraExists(Me, 32182) || auraExists(Me, 90355) || auraExists(Me, 146555));
            noStatsBuff = (!auraExists(Me, 1126) && !auraExists(Me, 115921) && !auraExists(Me, 20217) && !auraExists(Me, 90363) && !auraExists(Me, 117666));
            noStaminaBuff = (!auraExists(Me, 469) && !auraExists(Me, 21562) && !auraExists(Me, 90364) && !auraExists(Me, 109773));
            noAPBuff = (!auraExists(Me, 57330) && !auraExists(Me, 19506) && !auraExists(Me, 6673));
            noSPBuff = (!auraExists(Me, 1459) && !auraExists(Me, 61316) && !auraExists(Me, 109773));
            noMHasteBuff = (!auraExists(Me, 55610) && !auraExists(Me, 113742) && !auraExists(Me, 128432) && !auraExists(Me, 128433));
            noSHasteBuff = (!auraExists(Me, 24907) && !auraExists(Me, 49868) && !auraExists(Me, 135678));
            noCritBuff = (!auraExists(Me, 1459) && !auraExists(Me, 17007) && !auraExists(Me, 24604) && !auraExists(Me, 61316) && !auraExists(Me, 90309) && !auraExists(Me, 116781));
            #endregion
            
            //auraOnMe = auraExists(Me, SB.auraName);
            //auraOnTarget = auraExists(currentTarget, SB.auraName, true);
            //auraStackOnMe = auraStacks(Me, SB.auraName);
            //auraStackOnTarget = auraStacks(currentTarget, SB.auraName, true);
            //mainResource = playerPower();
        } 
        #endregion

        #region [Method] - Unit Evaluation
        public static bool isUnitValid(this WoWUnit Unit, double Range)
        {
            return Unit != null && Unit.IsValid && Unit.IsAlive && Unit.Attackable && Unit.DistanceSqr <= Range * Range;
        }
        #endregion

        #region [Method] - Jade Serpent Statue
        public static WoWUnit MyStatue
        {
            get
            {
                WoWUnit statue = ObjectManager.GetObjectsOfTypeFast<WoWUnit>().Where(unit => unit.Entry == 60849 && unit.Distance <= 20 && unit.CreatedByUnitGuid == Me.Guid).OrderBy(i => i.Distance).FirstOrDefault();
                if (statue == null) { L.combatLog("I need my statue"); }
                return statue;
            }
        }
        #endregion
    }
}

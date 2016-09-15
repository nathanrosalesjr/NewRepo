using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using Styx.WoWInternals.WoWObjects;
using Styx;
using Styx.WoWInternals;

using L = TestCombatRoutine.Core.Utilities.Log;
using SB = TestCombatRoutine.Core.Helpers.SpellBook;

namespace TestCombatRoutine.Core
{
    internal static class Unit
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit CurrentTarget { get { return StyxWoW.Me.CurrentTarget; } }

        public static bool IsUnitValid(this WoWUnit Unit, double Range)
        {
            return Unit != null && Unit.IsValid && Unit.IsAlive && Unit.Attackable && Unit.DistanceSqr <= Range * Range;
        }


        public static bool auraOnMe;
        public static bool auraOnTarget;
        public static uint auraStackOnMe;
        public static uint auraStackOnTarget;
        public static uint mainResource;
        public static void Cache()
        {
            auraOnMe = auraExists(Me, SB.auraName);
            auraOnTarget = auraExists(CurrentTarget, SB.auraName, true);
            auraStackOnMe = auraStacks(Me, SB.auraName);
            auraStackOnTarget = auraStacks(CurrentTarget, SB.auraName, true);
            mainResource = Me.CurrentEnergy;
        }

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
                L.DiagnosticLog("Exception in auraExists(): ", xException);
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
                L.DiagnosticLog("Exception in auraStacks(): ", xException);
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
                L.DiagnosticLog("Exception in auraTimeLeft(): ", xException);
                return 9999;
            }
        }

        //public static IEnumerable<WoWUnit> activeEnemies(WoWPoint fromLocation, double Range)
        //{
        //    var Hostile = enemyCount;
        //    return Hostile != null ? Hostile.Where(x => x.Location.DistanceSquared(fromLocation) < Range * Range) : null;
        //}

        private static List<WoWUnit> enemyCount { get; set; }

        public static void EnemyAnnex(double Range)
        {
            enemyCount.Clear();
            foreach (var u in surroundingEnemies<WoWUnit>())
            {
                if (u == null || !u.IsValid)
                    continue;
                if (!u.IsAlive || u.DistanceSqr > Range * Range)
                    continue;
                if (!u.Attackable || !u.CanSelect)
                    continue;
                if (u.IsFriendly)
                    continue;
                if (u.IsNonCombatPet && u.IsCritter)
                    continue;
                enemyCount.Add(u);
            }
        }

        private static IEnumerable<T> surroundingEnemies<T>() where T : WoWObject { return ObjectManager.GetObjectsOfTypeFast<T>(); }

        static Unit() { enemyCount = new List<WoWUnit>(); }
    }
}

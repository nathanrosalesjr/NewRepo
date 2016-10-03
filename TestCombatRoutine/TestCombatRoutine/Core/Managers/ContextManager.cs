using Styx;
using Styx.Common;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCombatRoutine.Model;

namespace TestCombatRoutine.Core.Managers
{
    public class ContextManager
    {
        private static LocalPlayer Me => StyxWoW.Me;
        private static LocalPlayer oldMe;

        private static ConcurrentDictionary<WoWGuid, WoWUnit> ObservedUnits = new ConcurrentDictionary<WoWGuid, WoWUnit>();
        private static ConcurrentDictionary<WoWGuid, UnitModel> ObservedUnitCache = new ConcurrentDictionary<WoWGuid, UnitModel>();
        private static ConcurrentDictionary<WoWGuid, Task<bool>> Observers = new ConcurrentDictionary<WoWGuid, Task<bool>>();

        //private static List<Task<bool>> Oberservers = new List<Task<bool>>();
        private static Task<bool> Master;
        private static CancellationTokenSource TokenSource = new CancellationTokenSource();



        static List<uint> mySpells;
        private static Task<bool> updateContext;
        static bool doWork = true;
        static bool doWork2 = true;
        static int CacheCalls = 0;

        static ContextManager()
        {
            var token = TokenSource.Token;
            Master = Task.Factory.StartNew<bool>(() => Watch(token), token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            updateContext = Task.Factory.StartNew(UpdateContext);
        }

        private static bool Watch(CancellationToken token)
        {
            return true;
        }

        public static void AddToObservedUnits(WoWUnit unit)
        {
            var unitModel = new UnitModel(unit);
            while (ObservedUnits.TryAdd(unit.Guid, unit) == false) { }
            while (ObservedUnitCache.TryAdd(unit.Guid, unitModel) == false) { }
        }

        public static void AddToMySpells(uint id)
        {
            mySpells.Add(id);
        }

        public static async Task<bool> UpdateObservedUnits()
        {
            Parallel.ForEach<KeyValuePair<WoWGuid, UnitModel>>(ObservedUnitCache.Where(u => u.Value.NeedsUpdate),
                entry =>
                {
                    var unit = ObservedUnits[entry.Key];
                    var unitModel = new UnitModel(unit);
                    ObservedUnitCache[entry.Key] = unitModel;
                });

            return true;
        }

        public static async Task<bool> ObserveUnit(WoWGuid guid)
        {
            var unit = ObservedUnits[guid];
            var cachedUnit = ObservedUnitCache[guid];
            if (!cachedUnit.Equals(unit))
            {
                var unitModel = new UnitModel(unit);
                ObservedUnitCache[guid] = unitModel;
            }
            return true;
        }

        private static bool UpdateContext()
        {
            while (oldMe == null)
            {
                oldMe = Me;
                Cache();
            }
            while (true)
            {
                if (doWork)
                {
                    Compare();
                }
                if (doWork2)
                {
                    //Thread.Sleep(500);
                }
            }
            return true;
        }

        public static void Complian(string s)
        {
            Logging.Write(s);
            Cache();
        }

        private static void Cache()
        {
            CacheCalls++;
            BuffsCount = Me.Buffs.Count;
            CastingSpell = Me.CastingSpell;
            CurrentTargetGuid = Me.CurrentTargetGuid;
            DebuffsCount = Me.Debuffs.Count;
            HealthPercent = Me.HealthPercent;
            IsCasting = Me.IsCasting;
            IsMoving = Me.IsMoving;
            X = Me.X;
            Y = Me.Y;
            ManaPercent = Me.ManaPercent;
        }
        static int BuffsCount;
        static WoWSpell CastingSpell;
        static WoWGuid CurrentTargetGuid;
        static int DebuffsCount;
        static double HealthPercent;
        static bool IsCasting;
        static bool IsMoving;
        static float X;
        static float Y;
        static double ManaPercent;


        private static void Compare()
        {
            if (BuffsCount != Me.Buffs.Count)
            {
                Complian("buffs");
            }
            if (CastingSpell != Me.CastingSpell)
            {
                Complian("spell");
            }
            if (CurrentTargetGuid != Me.CurrentTargetGuid)
            {
                Complian("target");
            }
            if (DebuffsCount != Me.Debuffs.Count)
            {
                Complian("debuffs");
            }
            if (HealthPercent != Me.HealthPercent)
            {
                Complian("health");
            }
            if (IsCasting != Me.IsCasting)
            {
                Complian("casting");
            }
            if (IsMoving != Me.IsMoving)
            {
                Complian("moving");
            }
            if (X != Me.X)
            {
                Complian("x");
            }
            if (Y != Me.Y)
            {
                Complian("y");
            }
            if (ManaPercent != Me.ManaPercent)
            {
                Complian("mana");
            }
        }

        public static async Task<List<uint>> GetMySpells()
        {
            return mySpells;
        }

    }
}

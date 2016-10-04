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
        public interface IUnitOfWork
        {
            object[] Parameters { set; }

            bool DoWork();
        }

        public class NewUnitWork : IUnitOfWork
        {
            private object[] parameters;
            public object[] Parameters { set { parameters = value; } }

            public bool DoWork()
            {
                var unit = (WoWUnit)parameters[0];
                var unitGuid = unit.Guid;
                if (Observers.ContainsKey(unitGuid))
                {
                    return false;
                }
                var unitModel = new UnitModel(unit);
                while (ObservedUnits.TryAdd(unitGuid, unit) == false) { }
                while (ObservedUnitCache.TryAdd(unitGuid, unitModel) == false) { }

                var task = Task.Factory.StartNew<bool>(() => ObserveUnit(unitGuid, TokenSource.Token), TokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                while (Observers.TryAdd(unitGuid, task) == false) { }
                Logging.Write($"Added {unit.SafeName} to observed units.");
                return true;
            }
        }


        private static LocalPlayer Me => StyxWoW.Me;
        private static LocalPlayer oldMe;

        private static ConcurrentDictionary<WoWGuid, WoWUnit> ObservedUnits = new ConcurrentDictionary<WoWGuid, WoWUnit>();
        private static ConcurrentDictionary<WoWGuid, UnitModel> ObservedUnitCache = new ConcurrentDictionary<WoWGuid, UnitModel>();
        private static ConcurrentDictionary<WoWGuid, Task<bool>> Observers = new ConcurrentDictionary<WoWGuid, Task<bool>>();
        public static bool IsObserving(WoWGuid guid) { return Observers.ContainsKey(guid); }
        public static int CacheUpdateCount = 0;

        //private static List<Task<bool>> Oberservers = new List<Task<bool>>();
        private static Task<bool> Master;
        private static Task<bool> Runner;
        private static BlockingCollection<IUnitOfWork> JobQueue = new BlockingCollection<IUnitOfWork>();
        private static CancellationTokenSource TokenSource = new CancellationTokenSource();



        static List<uint> mySpells;
        private static Task<bool> updateContext;
        static bool doWork = true;
        static bool doWork2 = true;
        static int CacheCalls = 0;

        static ContextManager()
        {
            var token = TokenSource.Token;
            Master = Task.Factory.StartNew<bool>(() => Orchestrate(token), token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            Runner = Task.Factory.StartNew<bool>(() => Consume(token), token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            AddToObservedUnits(Me.CurrentTarget);
            AddToObservedUnits(Me);
            //updateContext = Task.Factory.StartNew(UpdateContext);
        }

        private static bool Orchestrate(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var entry in Observers.Where(o => (o.Value.Status == TaskStatus.Faulted) || (o.Value.Status == TaskStatus.RanToCompletion && o.Value.Result == false)))
                {
                    var key = entry.Key;
                    var task = entry.Value;
                    Logging.Write($"{ObservedUnitCache[key].SafeName} failed");
                    Observers[key] = Task.Factory.StartNew<bool>(() => ObserveUnit(key, token), token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                    Logging.Write($"Started new task for {ObservedUnitCache[key].SafeName}");
                }
                if (Master.Exception != null)
                {
                    Logging.Write($"Master failed {Master.Exception.ToString()}");
                    Master = Task.Factory.StartNew<bool>(() => Orchestrate(token), token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                    Logging.Write($"New master task");
                }
            }
            return true;
        }

        private static bool Consume(CancellationToken token)
        {
            try
            {
                foreach (var job in JobQueue.GetConsumingEnumerable())
                {
                    job.DoWork();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void AddToObservedUnits(WoWUnit unit)
        {
            var parameters = new WoWUnit[] { unit };
            var work = new NewUnitWork() { Parameters = parameters };
            JobQueue.Add(work);
        }

        public static void AddToMySpells(uint id)
        {
            mySpells.Add(id);
        }

        public static bool UpdateObservedUnits()
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

        public static bool ObserveUnit(WoWGuid guid, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var unit = ObservedUnits[guid];
                    var cachedUnit = ObservedUnitCache[guid];
                    if (!cachedUnit.Equals(unit))
                    {
                        var unitModel = new UnitModel(unit);
                        ObservedUnitCache[guid] = unitModel;
                        CacheUpdateCount++;
                    }
                }
            }
            catch(Exception ex)
            {
                Logging.Write($"{ObservedUnitCache[guid]} failed: {ex.ToString()}");
                return false;
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
            //Cache();
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

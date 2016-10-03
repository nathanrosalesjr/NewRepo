using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx;
using Styx.CommonBot.Routines;
using Styx.WoWInternals.WoWObjects;
using Styx.TreeSharp;
using Styx.Common;
using System.Drawing;

using HKM = TestCombatRoutine.Core.Managers.HotKeyManager;
using R = TestCombatRoutine.Rotation.Rotation;
using U = TestCombatRoutine.Core.Unit;
using CommonBehaviors.Actions;
using TestCombatRoutine.Routines;

namespace TestCombatRoutine
{
    public partial class Main : CombatRoutine
    {
        private static readonly Version version = new Version(1, 0, 0, 0);

        public override WoWClass Class => StyxWoW.Me.Class;
        public override string Name => "Test Monk v" + version;
        public override CapabilityFlags SupportedCapabilities => CapabilityFlags.All;

        private static LocalPlayer Me => StyxWoW.Me;


        private Composite _preCombatBehavior, _combatBuffBehavior, _combatBehavior;
        private ActionRunCoroutine _combatCoroutine;



        private ICoroutineProvider _coroutineProvider;
        //public override Composite CombatBehavior { get { return _combatBehavior; } }
        //public override Composite CombatBuffBehavior { get { return _combatBuffsBehavior; } }
        //public override Composite HealBehavior { get { return _healBehavior; } }
        //public override Composite PreCombatBuffBehavior { get { return _preCombatBuffsBehavior; } }
        //public override Composite PullBehavior { get { return _pullBehavior; } }
        //public override Composite PullBuffBehavior { get { return _pullBuffsBehavior; } }
        //public override Composite RestBehavior { get { return _restBehavior; } }
        //public override Composite DeathBehavior { get { return _deathBehavior; } }

        public override Composite PreCombatBuffBehavior => _preCombatBehavior ?? (_preCombatBehavior = R.PreCombatBuffing());
        public override Composite CombatBuffBehavior => /*new ActionRunCoroutine(obj => CombatCoroutine());*/_combatBuffBehavior ?? (_combatBuffBehavior = R.CombatBuffing());
        public override Composite CombatBehavior => _combatCoroutine ?? (_combatCoroutine = new ActionRunCoroutine(obj => _coroutineProvider.GetCombatCoroutine())); //_combatBehavior ?? (_combatBehavior = R.RotationSelector());

        public static async Task<bool> CombatCoroutine() { return false; }


        //public override void Pulse()
        //{
        //    if (!StyxWoW.IsInWorld || Me == null || !Me.IsValid || !Me.IsAlive)
        //        return;
        //    if (!Me.Combat)
        //        return;
        //    U.Cache();
        //    U.EnemyAnnex(30);
        //}







        #region [Method] - Hidden Overrides
        public override void Initialize()
        {
            _coroutineProvider = new CoroutineProvider();

            //Logging.Write(Color.OrangeRed, "Hello {0}!", Me.Name);
            //Logging.Write(Color.White, "");
            //Logging.Write(Color.OrangeRed, "For optimal performance please use: Enyo");
            //Logging.Write(Color.White, "");
            //Logging.Write(Color.OrangeRed, "Current Version:");
            //Logging.Write(Color.OrangeRed, "-- Frost 1H v" + version + " --");
            //Logging.Write(Color.OrangeRed, "-- by Dagradt --");
            //Logging.Write(Color.OrangeRed, "-- A Death Knight CoumbatRoutine --");
            HKM.RegisterHotKeys();
        }
        public override bool WantButton { get { return true; } }
        public override void OnButtonPress() { /*Logging.Write(Color.OrangeRed, "Coming soon!");*/ }
        public override void ShutDown() { HKM.RemoveHotKeys(); }
        #endregion
    }


}

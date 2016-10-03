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
using Styx.WoWInternals;
using System.Diagnostics;
using Styx.CommonBot;
using TestCombatRoutine.Core.Managers;
using TestCombatRoutine.Routines;

namespace TestCombatRoutine
{
    public partial class Main : CombatRoutine
    {
        public override void Pulse()
        {
            _coroutineProvider.Pulse();

            if (!StyxWoW.IsInWorld || Me == null || !Me.IsValid || !Me.IsAlive)
                return;
            if (!Me.Combat)
                return;
            //U.Cache();
            //U.EnemyAnnex(30);
        }
    }


}

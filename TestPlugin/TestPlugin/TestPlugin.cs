using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.Common.Helpers;
using Styx.CommonBot;
using Styx.CommonBot.AreaManagement;
using Styx.CommonBot.Frames;
using Styx.CommonBot.Inventory;
using Styx.CommonBot.Profiles;
using Styx.CommonBot.POI;
using Styx.CommonBot.Routines;
using Styx.Helpers;
using Styx.Pathing;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.Misc;
using Styx.WoWInternals.World;
using Styx.WoWInternals.WoWObjects;
using Bots.Quest;
using Styx.TreeSharp;
using Sequence = Styx.TreeSharp.Sequence;
using Action = Styx.TreeSharp.Action;
using CommonBehaviors.Actions;
using Levelbot.Actions.Combat;

namespace TestPlugin
{
    public class TestPlugin : HBPlugin
    {
        public static LocalPlayer Me => StyxWoW.Me;
        public static int TestInt = 0;
        public static int ExternalClassCount = 0;
        public uint partySize = 0;
        public string currentProfile;
        public bool waiting;
        public WoWUnit lastQuestGiver = null;

        public override string Name => "TestPlugin";
        public override string Author => "TestName";
        public override Version Version => new Version(1, 0, 0, 0);
        public override bool WantButton { get { return false; } }

        private ExternalClass _ext = new ExternalClass();

        public void Initialize()
        {
            Logging.Write(Colors.Green, Name + "Loaded");
            partySize = Me.GroupInfo.PartySize;
        }

        public override void Pulse()
        {
            try
            {            
                _ext.Tick();
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }
        }

      

    }
}
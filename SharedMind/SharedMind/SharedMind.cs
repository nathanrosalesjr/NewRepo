
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx;
using Styx.Plugins;
using Styx.CommonBot.POI;
using Styx.WoWInternals.WoWObjects;
using System.Numerics;
using Styx.Common;

namespace SharedMind
{
    public class SharedMind : HBPlugin
    {
        public override string Author => "nathan";

        public override string Name => "SharedMind";

        public override Version Version => new Version(0, 0, 1, 0);

        private LocalPlayer Me => StyxWoW.Me;
        private BotPoi _lastPoi;
        private float _distanceSquared;
        private TreeHooks _instance;

        public override void Pulse()
        {
            if (_lastPoi != BotPoi.Current)
            {
                _lastPoi = BotPoi.Current;
                _distanceSquared = Vector3.Distance(Me.Location, _lastPoi.Location);
                _instance = TreeHooks.Instance;
            }
        }
        
    }
}

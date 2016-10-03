using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Core.Helpers
{
    public static class Extensions
    {
        public static TimeSpan GetAuraTimeLeft(this WoWUnit onUnit, string auraName, bool fromMyAura = true)
        {
            if (onUnit == null)
                return TimeSpan.Zero;

            WoWAura wantedAura = 
                onUnit.GetAllAuras().Where(a => a != null && a.Name == auraName && a.TimeLeft > TimeSpan.Zero && (!fromMyAura || a.CreatorGuid == StyxWoW.Me.Guid)).FirstOrDefault();

            return wantedAura != null ? wantedAura.TimeLeft : TimeSpan.Zero;
        }

    }
}

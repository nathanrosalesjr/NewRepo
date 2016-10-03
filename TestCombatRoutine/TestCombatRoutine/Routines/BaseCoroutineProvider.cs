using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Routines
{
    public abstract class BaseCoroutineProvider : ICombatCoroutineProvider
    {
        public virtual Task<bool> GetCombatCoroutine()
        {
            throw new NotImplementedException();
        }


        protected async Task<bool> AuraTimeLessThan(WoWUnit unit, string auraName, double seconds)
        {
            var aura = unit.GetAuraByName(auraName);
            if (aura == null)
                return true;
            if (aura.TimeLeft.TotalSeconds < seconds)
                return true;


            return false;
        }
    }
}

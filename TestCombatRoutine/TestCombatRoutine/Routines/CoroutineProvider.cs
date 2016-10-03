using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.CommonBot.Routines;
using Styx;

namespace TestCombatRoutine.Routines
{
    public class CoroutineProvider : ICoroutineProvider
    {
        private ICombatCoroutineProvider _combatCoroutineProvider;
        private IPulseManager _pulseManager;

        public CoroutineProvider()
        {
            if (StyxWoW.Me.Class == WoWClass.Druid)
                _combatCoroutineProvider = DruidCoroutineProvider.Instance;
            if (StyxWoW.Me.Class == WoWClass.Priest)
            {
                _combatCoroutineProvider = PriestCoroutineProvider.Instance;
                _pulseManager = PriestCoroutineProvider.Instance;
            }
            if (StyxWoW.Me.Class == WoWClass.Hunter)
            {
                _combatCoroutineProvider = HunterCoroutineProvider.Instance;
            }
        }

        public Task<bool> GetCombatBuffCoroutine()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetCombatCoroutine()
        {
            return await _combatCoroutineProvider.GetCombatCoroutine();
        }

        public Task<bool> GetDeathCoroutine()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetHealCoroutine()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPreCombatCoroutine()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPullBuffCoroutine()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPullCoroutine()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetRestCoroutine()
        {
            throw new NotImplementedException();
        }

        public void Pulse()
        {
            _pulseManager?.Pulse();
        }
    }
}

using Styx.CommonBot.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Routines
{
    public interface ICoroutineProvider
    {
        Task<bool> GetCombatBuffCoroutine();
        Task<bool> GetCombatCoroutine();
        Task<bool> GetHealCoroutine();
        Task<bool> GetPreCombatCoroutine();
        Task<bool> GetPullCoroutine();
        Task<bool> GetPullBuffCoroutine();
        Task<bool> GetRestCoroutine();
        Task<bool> GetDeathCoroutine();
        void Pulse();
        

    }
}

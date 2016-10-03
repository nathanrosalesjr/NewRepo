using Styx.CommonBot.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Routines
{
    public interface ICombatCoroutineProvider
    {
        Task<bool> GetCombatCoroutine();
    }
}

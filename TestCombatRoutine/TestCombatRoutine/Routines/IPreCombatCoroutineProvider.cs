using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Routines
{
    public interface IPreCombatCoroutineProvider
    {
        Task<bool> GetPreCombatCoroutine();
    }
}

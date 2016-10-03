using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCombatRoutine.Core.Managers;

namespace TestCombatRoutine.Core
{
    public class Logic
    {
        private List<uint> spellReady;
        //private Task
        public Logic()
        {
            spellReady = new List<uint>();
            spellReady.Sort();
        }

        public async void PopulateList()
        {
            spellReady = new List<uint>();
            spellReady.AddRange(await ContextManager.GetMySpells());
        }
        public async Task<bool> CanCastSpell(uint id)
        {
            if (spellReady.BinarySearch(id) > 0)
                return true;
            return false;
        }
    }
}

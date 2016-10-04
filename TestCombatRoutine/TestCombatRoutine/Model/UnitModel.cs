using Styx.Common;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Model
{
    public class UnitModel
    {
        public bool NeedsUpdate;
        int BuffsCount;
        int CastingSpell;
        int CurrentTargetGuid;
        int DebuffsCount;
        double HealthPercent;
        bool IsCasting;
        bool IsInMyPartyOrRaid;
        bool IsMoving;
        double ManaPercent;
        public string SafeName;
        float X;
        float Y;
        public UnitModel() { }
        public UnitModel(WoWUnit unit)
        {
            Init(unit);
        }
        public UnitModel(WoWPlayer player)
        {
            Init(player);
        }
        
        private void Init(WoWPlayer player)
        {
            IsInMyPartyOrRaid = player.IsInMyPartyOrRaid;
            Init((WoWUnit)player);
        }
        private void Init(WoWUnit unit)
        {
            BuffsCount = unit.Buffs.Count;
            BuffsCount = unit.Buffs.Count;
            CurrentTargetGuid = unit.CurrentTargetGuid.Entry;
            DebuffsCount = unit.Debuffs.Count;
            HealthPercent = unit.HealthPercent;
            IsCasting = unit.IsCasting;
            IsMoving = unit.IsMoving;
            ManaPercent = unit.ManaPercent;
            SafeName = unit.SafeName;
            X = unit.X;
            Y = unit.Y;
            NeedsUpdate = false;
        }

        public override bool Equals(object obj)
        {
            return Equals((WoWUnit)(obj));
            if (obj.GetType().IsSubclassOf(typeof(WoWUnit)))
            {
                return Equals((WoWUnit)(obj));
            }
            return base.Equals(obj);
        }

        private bool Equals(WoWUnit unit)
        {
            if (!unit.IsValid)
                return false;
            var message = $"Unit: {unit.SafeName}\n";
            var neq = false;
            if (BuffsCount != unit.Buffs.Count)
            {
                message += "Buffs\n";
                neq = true;
            }
            if (CurrentTargetGuid != unit.CurrentTargetGuid.Entry)
            {
                message += "Target\n";
                neq = true;
            }
            if (DebuffsCount != unit.Debuffs.Count)
            {
                message += "Debuffs\n";
                neq = true;
            }
            if (HealthPercent != unit.HealthPercent)
            {
                message += "Health\n";
                neq = true;
            }
            if (IsCasting != unit.IsCasting)
            {
                message += "Casting\n";
                neq = true;
            }
            if (IsMoving != unit.IsMoving)
            {
                message += "Moving\n";
                neq = true;
            }
            if (X != unit.X)
            {
                message += "X\n";
                neq = true;
            }
            if (Y != unit.Y)
            {
                message += "Y\n";
                neq = true;
            }
            if (ManaPercent != unit.ManaPercent)
            {
                message += "Mana\n";
                neq = true;
            }
            if (neq)
            {
                //Logging.Write(message);
                return false;
            }
            return true;
        }

    }
}

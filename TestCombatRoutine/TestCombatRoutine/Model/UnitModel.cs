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
            X = unit.X;
            Y = unit.Y;
            ManaPercent = unit.ManaPercent;
            NeedsUpdate = false;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(WoWUnit))
            {
                return Equals((WoWUnit)(obj));
            }
            return base.Equals(obj);
        }

        private bool Equals(WoWUnit unit)
        {
            if (BuffsCount != unit.Buffs.Count)
            {
                return false;
            }
            if (CurrentTargetGuid != unit.CurrentTargetGuid.Entry)
            {
                return false;
            }
            if (DebuffsCount != unit.Debuffs.Count)
            {
                return false;
            }
            if (HealthPercent != unit.HealthPercent)
            {
                return false;
            }
            if (IsCasting != unit.IsCasting)
            {
                return false;
            }
            if (IsMoving != unit.IsMoving)
            {
                return false;
            }
            if (X != unit.X)
            {
                return false;
            }
            if (Y != unit.Y)
            {
                return false;
            }
            if (ManaPercent != unit.ManaPercent)
            {
                return false;
            }
            return true;
        }

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
        float X;
        float Y;
    }
}

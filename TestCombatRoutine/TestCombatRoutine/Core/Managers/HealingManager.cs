using Styx;
using Styx.CommonBot;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Core.Managers
{
    public class HealingManager
    {
        public static List<WoWPlayer> Targets;
        public static List<WoWPlayer> PreferredTargets;
        public static List<WoWUnit> PreferredPets;
        public static List<WoWPlayer> Tanks;
        private static List<string> PreferredNames = new List<string>()
        {
            "Wornou",
            "Tionerect",
            "Tkal"
        };

        public static void UpdateTargets()
        {
            Targets = ObjectManager.GetObjectsOfTypeFast<WoWPlayer>().Where(p => p.IsInMyPartyOrRaid && !p.Attackable).ToList();
            PreferredTargets = Targets.Where(p => PreferredNames.Contains(p.Name)).ToList();
            PreferredPets = PreferredTargets.SelectMany(p => p.Minions.Where(m => m.PlayerControlled == true)).ToList();
            Tanks = Targets.Where(p => GetMainTankGuids().Contains(p.Guid)).ToList();
        }

        public static HashSet<WoWGuid> GetMainTankGuids()
        {
            var infos = StyxWoW.Me.GroupInfo.RaidMembers;

            return new HashSet<WoWGuid>(
                from pi in infos
                where (pi.Role & WoWPartyMember.GroupRole.Tank) != 0
                select pi.Guid);
        }
    }
    //public class HealingManager : HealTargeting
    //{
    //    public new static HealingManager Instance;
    //    static LocalPlayer Me => StyxWoW.Me;

    //    static HealingManager()
    //    {
    //        HealTargeting.Instance = Instance = new HealingManager();
    //    }

    //    protected override List<WoWObject> GetInitialObjectList()
    //    {
    //        // Targeting requires a list of WoWObjects - so it's not bound to any specific type of object. Just casting it down to WoWObject will work fine.
    //        // return ObjectManager.ObjectList.Where(o => o is WoWPlayer).ToList();
    //        List<WoWObject> heallist;

    //        //if (Me.GroupInfo.IsInRaid || Me.GroupInfo.IsInParty)
    //        //{
    //        //    if (!SingularSettings.Instance.IncludeCompanionssAsHealTargets)
    //        //        heallist = ObjectManager.ObjectList
    //        //            .Where(o => o is WoWPlayer && o.ToPlayer().IsInMyRaid)
    //        //            .ToList();
    //        //    else
    //        //        heallist = ObjectManager.ObjectList
    //        //            .Where(o => (o is WoWPlayer && o.ToPlayer().IsInMyRaid) || (o is WoWUnit && o.ToUnit().SummonedByUnitGuid == Me.Guid && !o.ToUnit().IsPet))
    //        //            .ToList();
    //        //}

    //        //if (Me.ZoneId == 6852)
    //            HashSet<WoWGuid> raidmember = new HashSet<WoWGuid>(Me.GroupInfo.RaidMemberGuids);
    //                heallist = ObjectManager.ObjectList
    //                    .Where(o => raidmember.Contains(o.Guid) || (o is WoWUnit && o.ToUnit().SummonedByUnitGuid == Me.Guid && !o.ToUnit().IsPet))
    //                    .ToList();

    //        return heallist;
    //    }

    //    public override void Pulse()
    //    {
    //        base.Pulse();
    //    }
    //}


}

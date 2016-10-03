//using CommonBehaviors.Actions;
//using Styx;
//using Styx.CommonBot;
//using Styx.TreeSharp;
//using Styx.WoWInternals;
//using Styx.WoWInternals.WoWObjects;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using L = TestCombatRoutine.Core.Utilities.Log;
//using SB = TestCombatRoutine.Core.Helpers.SpellBook;

//namespace TestCombatRoutine.Core
//{
//    internal static class Spell
//    {
//        public delegate WoWUnit unitSelectionDelegate(object Context);
//        public delegate T Selection<T>(object Context);
//        public static int lastSpellCast;
//        private static LocalPlayer Me { get { return StyxWoW.Me; } }
//        private static WoWUnit currentTarget { get { return StyxWoW.Me.CurrentTarget; } }


//        private static Composite castMethod(this int Spell, unitSelectionDelegate unitSelection, Selection<bool> reqs = null, bool gcdCast = false)
//        {
//            return new Decorator(ret => ((reqs != null && reqs(ret)) || (reqs == null)) && unitSelection != null && unitSelection(ret) != null && SpellManager.CanCast(Spell, unitSelection(ret)),
//                new Styx.TreeSharp.Action(ret =>
//                {
//                    if (SpellManager.Cast(Spell, unitSelection(ret)))
//                    {
//                        lastSpellCast = Spell;
//                        L.CombatLog("Casting: " + WoWSpell.FromId(Spell).Name + " on " + unitSelection(ret).SafeName);
//                        if (!gcdCast)
//                            return RunStatus.Success;
//                    }
//                    return RunStatus.Failure;
//                }));
//            { }
//        }

//        private static Composite castMethod(this int Spell, Selection<bool> reqs = null, bool gcdCast = false)
//        {
//            return new Decorator(ret => ((reqs != null && reqs(ret)) || (reqs == null)) && SpellManager.CanCast(Spell),
//                new Styx.TreeSharp.Action(ret =>
//                {
//                    if (SpellManager.Cast(Spell))
//                    {
//                        lastSpellCast = Spell;
//                        L.CombatLog("Casting: " + WoWSpell.FromId(Spell).Name);
//                        if (!gcdCast)
//                            return RunStatus.Success;
//                    }
//                    return RunStatus.Failure;
//                }));
//        }

//        //private static Composite dropMethod(this int Spell, unitSelectionDelegate unitSelection, Selection<bool> reqs = null, bool waitForSpell = false)
//        //{
//        //    return new Decorator(ret => unitSelection != null && reqs(ret) && SpellManager.CanCast(Spell) && (Me.Location.Distance(unitSelection(ret).Location) <= WoWSpell.FromId(Spell).MaxRange || WoWSpell.FromId(Spell).MaxRange == 0),
//        //        new Sequence(
//        //            new Styx.TreeSharp.Action(ret => SpellManager.Cast(Spell)),
//        //            new DecoratorContinue(ctx => waitForSpell,
//        //                new WaitContinue(1, ret => Me.CurrentPendingCursorSpell != null && Me.CurrentPendingCursorSpell.Id == Spell, new ActionAlwaysSucceed())),
//        //                new Styx.TreeSharp.Action(ret =>
//        //                {
//        //                    SpellManager.ClickRemoteLocation(unitSelection(ret).Location);
//        //                    lastSpellCast = Spell;
//        //                    L.combatLog("Casting: " + Spell);
//        //                })));
//        //}

//        //public static Composite dropCast(this int Spell, unitSelectionDelegate unitSelection, Selection<bool> reqs = null)
//        //{
//        //    return dropMethod(Spell, unitSelection, reqs);
//        //}

//        public static Composite blindCast(this int Spell, Selection<bool> reqs = null, bool gcdCast = false)
//        {
//            return castMethod(Spell, reqs, gcdCast);
//        }

//        public static Composite Buff(this int Spell, Selection<bool> reqs = null, bool gcdCast = false)
//        {
//            return castMethod(Spell, ret => Me, reqs, gcdCast);
//        }

//        public static Composite Cast(this int Spell, Selection<bool> reqs = null, bool gcdCast = false)
//        {
//            return castMethod(Spell, ret => currentTarget, reqs, gcdCast);
//        }

//        public static double cooldownTimeLeft(this int Spell)
//        {
//            try
//            {
//                SpellFindResults Results;
//                return SpellManager.FindSpell(Spell, out Results) ? (Results.Override != null ? Results.Override.CooldownTimeLeft.TotalMilliseconds : Results.Original.CooldownTimeLeft.TotalMilliseconds) : 9999;
//            }
//            catch (Exception xException)
//            {
//                L.DiagnosticLog("Exception in cooldownTimeLeft(): ", xException);
//                return 9999;
//            }
//        }

//    }
//}

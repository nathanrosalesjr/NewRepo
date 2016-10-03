using Styx.CommonBot;
using Styx.WoWInternals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCombatRoutine.Core.Helpers
{
    public class SpellBook
    {
        private static WoWSpell _growl;
        private static WoWSpell _mangle;
        private static WoWSpell _maul;
        private static WoWSpell _moonFire;
        private static WoWSpell _tigerPalm;
        private static WoWSpell _thrash;
        public static WoWSpell Growl => _growl ?? (_growl = SpellManager.Spells["Growl"]);
        public static WoWSpell Mangle => _mangle ?? (_mangle = SpellManager.Spells["Mangle"]);
        public static WoWSpell Maul => _maul ?? (_maul = SpellManager.Spells["Maul"]);
        public static WoWSpell Moonfire => _moonFire ?? (_moonFire = SpellManager.Spells["Moonfire"]);
        public static WoWSpell TigerPalm => _tigerPalm ?? (_tigerPalm = SpellManager.Spells["Tiger Palm"]);
        public static WoWSpell Thrash => _thrash ?? (_thrash = SpellManager.Spells["Thrash"]);

        private static WoWSpell _penance;
        public static WoWSpell Penance => _penance ?? (_penance = SpellManager.Spells["Penance"]);
        private static WoWSpell _plea;
        public static WoWSpell Plea => _plea ?? (_plea = SpellManager.Spells["Plea"]);
        private static WoWSpell _powerWordShield;
        public static WoWSpell PowerWordShield => _powerWordShield ?? (_powerWordShield = SpellManager.Spells["Power Word: Shield"]);
        private static WoWSpell _shadowMend;
        public static WoWSpell ShadowMend => _shadowMend ?? (_shadowMend = SpellManager.Spells["Shadow Mend"]);
        private static WoWSpell _killCommand;
        public static WoWSpell KillCommand => _killCommand ?? (_killCommand = SpellManager.Spells["Kill Command"]);
        private static WoWSpell _direFrenzy;
        public static WoWSpell DireFrenzy => _direFrenzy ?? (_direFrenzy = SpellManager.Spells["Dire Frenzy"]);
        private static WoWSpell _multiShot;
        public static WoWSpell MultiShot => _multiShot ?? (_multiShot = SpellManager.Spells["Multi-Shot"]);
        private static WoWSpell _cobraShot;
        public static WoWSpell CobraShot => _cobraShot ?? (_cobraShot = SpellManager.Spells["Cobra Shot"]);


        public static int auraName = 0;
        public enum Spells
        {
            BloodFury = 20572,
            TigerPalm = 100780,
        };
    }
}

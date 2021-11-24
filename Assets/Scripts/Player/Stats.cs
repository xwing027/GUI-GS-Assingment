using System;
using System.Collections;
using System.Collections.Generic;

public class Stats : Attributes
{
    #region Structs
    [Serializable]
    public struct StatBlock //this is what each stat (strength, dex, etc) will need to have
    {
        public string name;
        public int value;
        public int tempValue; //this is used for any buffs that temporarily increase stats
        public int tempCustomValue;
    }
    #endregion

    #region Variables
    public StatBlock[] characterStats = new StatBlock[6]; //each creatue has 6 stats
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public CharacterRace characterRace = CharacterRace.Human;
    #endregion
}
public enum CharacterClass
{
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Monk,
    Paladin,
    Ranger,
    Rogue,
    Sorcerer,
    Warlock,
    Wizard,
}

public enum CharacterRace
{
    Dragonborn,
    Dwarf,
    Elf,
    Gnome,
    HalfElf,
    Halfling,
    HalfOrc,
    Human,
    Tiefling,
}

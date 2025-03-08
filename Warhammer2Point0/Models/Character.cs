namespace WarhammerFightSimulator.Models;


public enum CharacterSkill {
    Unik
};

public enum CharacterAbility {
    Bijatyka = 0,
    // BlyskawicznePrzeladowanie,
    BlyskawicznyBlok = 1,
    // BronNaturalna,
    Oburecznosc = 2,
    SilnyCios = 3,
    StrzalPrecyzyjny = 4,
    StrzalPrzebijajacy = 5,
};

public class Character {
    public Guid Guid { get; set; }
    public string Name { get; set; } = "";
    public CharacterTeam Team { get; set; }

    //stats
    public int WW { get; set; }
    public int Zr { get; set; }
    public int A { get; set; }
    public int Zyw { get; set; }
    public int S { get; set; }
    public int Wt { get; set; }
    public int US { get; set; }

    public Armour Armour { get; set; } = new Armour{};
    public Hands Hands { get; set; } = new Hands{};
    //public List<IEquipmentItem> EquipmentItems { get; set; } = [];
    public List<CharacterAbility> Abilities { get; set; } = [];

    public Dictionary<CharacterSkill, int> Skills { get; set; } = [];
    public int CurrentZyw { get; set; }
    public int AttacksCount { get; set; }

    public bool IsParring { get; set; } = false;
    public bool IsDodging { get; set; } = true;

}

public enum CharacterTeam{
    TeamA = 0,
    TeamB = 1
}

public class Hands{
    public IEquipmentItem? RightHand { get; set; } = null;
    public IEquipmentItem? LeftHand { get; set; } = null;
}

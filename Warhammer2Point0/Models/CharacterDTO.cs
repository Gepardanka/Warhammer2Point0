using WarhammerFightSimulator.Models;

public class CharacterDTO {
    public required string Name { get; set; }
    public CharacterTeam Team { get; set; }
    public int MeleeSkill { get; set; }
    public int Dexterity { get; set; }
    public int NumberOfAttacks { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Resistance { get; set; }
    public int RangeSkill { get; set; }
    public required Armour Armour { get; set; }
    public WeaponDTO? RightHand { get; set; }
    public WeaponDTO? LeftHand { get; set; }
    public List<CharacterAbility> Abilities { get; set; } = [];
    public int? DodgeValue { get; set; }
}

public class WeaponDTO {
    public int Modifier { get; set; }
    public required List<WeaponTrait> WeaponTraits { get; set; }
}
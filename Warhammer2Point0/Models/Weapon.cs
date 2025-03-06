namespace WarhammerFightSimulator.Models;

public class Weapon : IEquipmentItem {
    public string WeaponName { get; set; } = "";
    public int Modifier { get; set; }
    public virtual EquipmentItemType EquipmentItemType { get;}
    public List<WeaponTrait> WeaponTraits { get; set; } = [];
}

public class RangedWeapon : Weapon
{
    public override EquipmentItemType EquipmentItemType => EquipmentItemType.RangedWeapon;
    public int LoadingProgress { get; set; }
    public int LoadingTime { get; set; }
}

public class MeleeWeapon : Weapon {
    public override EquipmentItemType EquipmentItemType => EquipmentItemType.MeleeWeapon;
}
public class ThrownWeapon : Weapon{
    public int LoadingProgress { get; set; }
    public int LoadingTime { get; set; }
}
public enum WeaponTrait{
    Ciezki = 0,
    Druzgoczacy = 1,
    Parujacy = 2,
    Powolny = 3,
    PrzebijajacyZbroje = 4,
    Szybki = 5,
    Unieruchamiajacy = 6,
    Wywazony = 7
}
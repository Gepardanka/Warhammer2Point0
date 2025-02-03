namespace WarhammerFightSimulator.Models;

public class Weapon : IEquipmentItem {
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
    Ciezki,
    Druzgoczacy,
    Parujacy,
    Powolny,
    PrzebijajacyZbroje,
    Szybki,
    Unieruchamiajacy,
    Wywazony,
    Tarcza
}
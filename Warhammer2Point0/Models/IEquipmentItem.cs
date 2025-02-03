namespace WarhammerFightSimulator.Models;

public interface IEquipmentItem{
    public EquipmentItemType EquipmentItemType { get;}
}

public enum EquipmentItemType {
    MeleeWeapon = 1,
    RangedWeapon = 2,
    ThrownWeapon = 3,
    Ammunition = 4,
}
public class Ammunition : IEquipmentItem{
    public EquipmentItemType EquipmentItemType { get;} = EquipmentItemType.Ammunition;
    public RangedWeapon WeaponType { get; set; } = new RangedWeapon{};
    public int Count { get; set; }
}


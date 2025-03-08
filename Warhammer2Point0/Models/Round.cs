namespace WarhammerFightSimulator.Models;
public class Round{
    public string AttackingWeaponName { get; set; } = "";
    public HitSuccessMissReason HitSuccessFailReason { get; set; }
    public Guid DefendingCharID { get; set; }
    public int DefendingCharCurrentHP { get; set; }
}

public enum HitSuccessMissReason{
    Hit = 0,
    Miss = 1,
    Parry = 2,
    Dodge = 3
}
using WarhammerFightSimulator.Models;
namespace WarhammerFightSimulator.Services;
public abstract class Attack(Character attacking, Character defending, IDiceRolls diceRolls, int wwMod)
{
    protected readonly Character _attacking = attacking;
    protected readonly Character _defending = defending;
    protected readonly IDiceRolls _diceRolls = diceRolls;
    protected int _wwMod = wwMod;

    public abstract Round MakeAttack(IEquipmentItem? attackingHand);
    protected int HitPlace(Armour armour, int roll)
    {
        if (roll == 100) { return armour.LeftLeg; }

        string strRoll = roll.ToString().PadLeft(2, '0');
        string reversed = new(strRoll.Reverse().ToArray());
        int location = int.Parse(reversed);

        if (location < 16) { return armour.Head; }
        if (location < 36) { return armour.RightArm; }
        if (location < 56) { return armour.LeftArm; }
        if (location < 81) { return armour.Body; }
        if (location < 91) { return armour.RightLeg; }
        if (location < 100) { return armour.LeftLeg; }
        throw new Exception("you absolute buffon");
    }
    protected bool CanDodge()
    {
        return _defending.Skills.ContainsKey(CharacterSkill.Unik) && _defending.IsDodging;
    }
    protected bool Dodge(Weapon? attackingWeapon, Round round)
    {
        bool value = TotalDodgeValue(attackingWeapon) >= _diceRolls.D100();
        _defending.IsDodging = false;
        if (value) { round.HitSuccessFailReason = HitSuccessMissReason.Dodge; }
        return value;

    }
    protected int? TotalDodgeValue(Weapon? attackingWeapon)
    {
        if (!CanDodge()) { return null; }
        int dodgeValue = _defending.Skills[CharacterSkill.Unik];
        int zrMod = StatsModifications.PowolnySzybki(attackingWeapon);
        return _defending.Zr + dodgeValue + zrMod;
    }
    protected bool CanParry()
    {
        if (_defending.Hands.RightHand != null)
        {
            return _defending.IsParring && _defending.Hands.RightHand.EquipmentItemType == EquipmentItemType.MeleeWeapon;
        }
        if (_defending.Hands.LeftHand != null)
        {
            return _defending.IsParring && _defending.Hands.LeftHand.EquipmentItemType == EquipmentItemType.MeleeWeapon;
        }
        return false;
    }
    protected bool Parry(Weapon? attackingWeapon, Round round)
    {
        bool value = _diceRolls.D100() <= TotalParryValue(attackingWeapon);
        _defending.IsParring = false;
        if (value) { round.HitSuccessFailReason = HitSuccessMissReason.Parry; }
        return value;
    }
    protected int? TotalParryValue(Weapon? attackingWeapon)
    {
        if (!CanParry()) { return null; }
        int wwMod = 0;
        if (_defending.Hands.RightHand != null)
        {
            wwMod = StatsModifications.Parujacy((MeleeWeapon)_defending.Hands.RightHand);
        }
        if (wwMod == 0)
        {
            if (_defending.Hands.LeftHand != null)
            {
                wwMod = StatsModifications.Parujacy((MeleeWeapon)_defending.Hands.LeftHand);
            }
        }
        wwMod += StatsModifications.PowolnySzybki(attackingWeapon);
        return _defending.WW + wwMod;
    }
    protected bool DidDodgeParry(Weapon? attackingWeapon, Round round)
    {
        int? parryValue = TotalParryValue(attackingWeapon);
        int? dodgeValue = TotalDodgeValue(attackingWeapon);

        if (dodgeValue.HasValue && parryValue.HasValue)
        {
            if (dodgeValue > parryValue) { return Dodge(attackingWeapon, round); }
            return Parry(attackingWeapon, round);
        }
        if (dodgeValue.HasValue) { return Dodge(attackingWeapon, round); }
        if (parryValue.HasValue) { return Parry(attackingWeapon, round); }
        return false;
    }
}
public class UnarmedAttack(Character attacking, Character defending, IDiceRolls diceRolls, int wwMod) : Attack(attacking, defending, diceRolls, wwMod)
{
    public override Round MakeAttack(IEquipmentItem? hand)
    {
        Round round = new Round();
        round.AttackingWeaponName = WeaponName.Unarmed;
        round.DefendingCharID = _defending.Guid;
        round.DefendingCharCurrentHP = _defending.CurrentZyw;

        var mod = StatsModifications.Bijatyka(_attacking);
        int damage = mod.DamageMod;
        _wwMod += mod.WWMod;
        int roll = _diceRolls.D100();

        if (_attacking.WW + _wwMod < roll)
        {
            round.HitSuccessFailReason = HitSuccessMissReason.Miss;
            return round;
        }

        bool dodgeOrParry = DidDodgeParry(null, round);
        if (!dodgeOrParry)
        {
            damage += _attacking.S - 4;
            TakeAttack(roll, damage + _diceRolls.D10(1), round);
            round.HitSuccessFailReason = HitSuccessMissReason.Hit;
        }
        return round;
    }
    private void TakeAttack(int d100Roll, int damage, Round round)
    {
        int armour = HitPlace(_defending.Armour, d100Roll) * 2;
        if (damage - _defending.Wt - armour > 0)
        {
            _defending.CurrentZyw = _defending.CurrentZyw + _defending.Wt + armour - damage;
            round.DefendingCharCurrentHP = _defending.CurrentZyw;
        }
    }
}
public class MeleeAttack(Character attacking, Character defending, IDiceRolls diceRolls, int wwMod) : Attack(attacking, defending, diceRolls, wwMod)
{
    public override Round MakeAttack(IEquipmentItem? attackingHand)
    {
        Round round = new Round();
        MeleeWeapon attackingWeapon = (MeleeWeapon)attackingHand!;
        round.AttackingWeaponName = attackingWeapon.WeaponName;
        round.DefendingCharCurrentHP = _defending.CurrentZyw;
        round.DefendingCharID = _defending.Guid;
        int roll = _diceRolls.D100();
        if (_attacking.WW + _wwMod < roll) { 
            round.HitSuccessFailReason = HitSuccessMissReason.Miss; 
            return round; }

        bool dodgeOrParry = DidDodgeParry(attackingWeapon, round);

        if (dodgeOrParry) { return round; }

        int d10Roll = StatsModifications.Druzgoczacy(attackingWeapon, _diceRolls);
        int damage = _attacking.S
            + attackingWeapon.Modifier
            + StatsModifications.SilnyCios(_attacking);
        TakeAttack(roll, damage + d10Roll, attackingWeapon, round);
        round.HitSuccessFailReason = HitSuccessMissReason.Hit;
        return round;
    }
    private void TakeAttack(int d100Roll, int damage, Weapon attackingWeapon, Round round)
    {
        int armour = HitPlace(_defending.Armour, d100Roll);
        int armourMod = 0;
        if (armour > 0)
        {
            armourMod = StatsModifications.PrzebijajacyZbroje(attackingWeapon);
        }
        int healthTaken = damage - _defending.Wt - armour + armourMod;
        if (healthTaken > 0)
        {
            _defending.CurrentZyw -= healthTaken;
            round.DefendingCharCurrentHP = _defending.CurrentZyw;
        }
    }
}
public class RangedAttack(Character attacking, Character defending, IDiceRolls diceRolls) : Attack(attacking, defending, diceRolls, 0)
{
    public override Round MakeAttack(IEquipmentItem? attackingHand)
    {
        Round round = new Round();
        RangedWeapon attackingWeapon = (RangedWeapon)attackingHand!;
        int roll = _diceRolls.D100();
        if (_attacking.US >= roll)
        {
            int damage = attackingWeapon.Modifier + _diceRolls.D10(1);
            TakeAttack(roll, damage);
        }
        return round;
    }
    private void TakeAttack(int d100Roll, int damage)
    {
        int armour = HitPlace(_defending.Armour, d100Roll);
        int healthTaken = damage - _defending.Wt - armour;
        if (healthTaken > 0)
        {
            _defending.CurrentZyw -= healthTaken;
        }
    }

}
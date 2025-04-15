using WarhammerFightSimulator.Models;
namespace WarhammerFightSimulator.Services;

public static class StatsModifications
{
    public static int GroupMod(List<Character> group, CharacterTeam attackingTeam)
    {
        return Math.Min((group.Count(x => x.Team == attackingTeam) - 1) * 10, 20);
    }
    public static BijatykaMod Bijatyka(Character attacking)
    {
        if (attacking.Abilities.Contains(CharacterAbility.Bijatyka))
        {
            return new BijatykaMod(10, 1);
        }
        return new BijatykaMod(0, 0);
    }
    public static int Druzgoczacy(Weapon attackingWeapon, IDiceRolls diceRolls)
    {
        if (attackingWeapon.WeaponTraits.Contains(WeaponTrait.Druzgoczacy))
        {
            return diceRolls.HighestD10(2);
        }
        return diceRolls.D10(1);
    }
    public static int SilnyCios(Character attacking)
    {
        if (attacking.Abilities.Contains(CharacterAbility.SilnyCios))
        {
            return 1;
        }
        return 0;
    }
    public static int PowolnySzybki(Weapon? attackingWeapon)
    {
        if (attackingWeapon == null) { return 0; }
        if (attackingWeapon.WeaponTraits.Contains(WeaponTrait.Powolny))
        {
            return 10;
        }
        if (attackingWeapon.WeaponTraits.Contains(WeaponTrait.Szybki))
        {
            return -10;
        }
        return 0;
    }
    public static int Parujacy(Weapon attackingWeapon)
    {
        if (attackingWeapon.WeaponTraits.Contains(WeaponTrait.Parujacy)) { return 10; }
        else { return 0; }
    }
    public static int PrzebijajacyZbroje(Weapon attackingWeapon)
    {
        if (attackingWeapon.WeaponTraits.Contains(WeaponTrait.PrzebijajacyZbroje))
        {
            return 1;
        }
        return 0;
    }
    public static int LeftHandMod(Weapon weapon, Character character)
    {
        if (weapon.WeaponTraits.Contains(WeaponTrait.Wywazony)
            || character.Abilities.Contains(CharacterAbility.Oburecznosc))
        {
            return 0;
        }
        return -20;
    }
    public static void BlyskawicznyBlok(Character attacking, CharacterStatus characterStatus)
    {
        if (attacking.Abilities.Contains(CharacterAbility.BlyskawicznyBlok)
            && attacking.A >= 3)
        {
            characterStatus.IsParring = true;
            characterStatus.AttacksCount --;
        }
    }
}
public record BijatykaMod(int WWMod, int DamageMod);
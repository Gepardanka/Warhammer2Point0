using WarhammerFightSimulator.Models;

namespace WarhammerFightSimulator.Services;
public interface IAttackSetUp{
    public void ChooseAttack(Character attacking, Character defending, int wwMod);
}

public class AttackSetUp : IAttackSetUp{
    private readonly IDiceRolls _diceRolls;
    public AttackSetUp(IDiceRolls diceRolls){
        _diceRolls = diceRolls;
    }
    public void ChooseAttack(Character attacking, Character defending, int wwMod)
    {
        if (attacking.Hands.RightHand == null && attacking.Hands.LeftHand == null)
        {
            UnarmedLogic(attacking, defending, wwMod);
        }
        else if (attacking.Hands.RightHand != null){
            if(attacking.Hands.RightHand.EquipmentItemType == EquipmentItemType.MeleeWeapon){
                MeleeLogic(attacking, defending, wwMod, (MeleeWeapon)attacking.Hands.RightHand);
            }
        }
        else if(attacking.Hands.LeftHand!= null){            
            if(attacking.Hands.LeftHand.EquipmentItemType == EquipmentItemType.MeleeWeapon){
                wwMod += 
                    StatsModifications.LeftHandMod((MeleeWeapon)attacking.Hands.LeftHand, attacking);                
                MeleeLogic(attacking, defending, wwMod, (MeleeWeapon)attacking.Hands.LeftHand);
            }
        }
    }
    private void MeleeLogic(Character attacking, Character defending, int wwMod, MeleeWeapon attackingWeapon){
        attacking.IsDodging = true;
        attacking.AttacksCount = attacking.A;

        if (attacking.A > 1)
        {
            StatsModifications.BlyskawicznyBlok(attacking);
            DoAttack(new MeleeAttack(attacking, defending, _diceRolls, wwMod), attackingWeapon, attacking.AttacksCount);
            
        }
        else
        {
            DoAttack(new MeleeAttack(attacking, defending, _diceRolls, wwMod), attackingWeapon, attacking.AttacksCount);
            attacking.IsParring = true;
        }
    }

    private void UnarmedLogic(Character attacking, Character defending, int wwMod){
        attacking.IsDodging = true;
        DoAttack(new UnarmedAttack(attacking, defending, _diceRolls, wwMod),
            null, attacking.A);
    }

    private void DoAttack(Attack attackType, Weapon? attackingWeapon, int attacks){
        for(int i = 0; i < attacks; i++){
            attackType.MakeAttack(attackingWeapon);
        }
    }
}
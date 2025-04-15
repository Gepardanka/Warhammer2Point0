using WarhammerFightSimulator.Models;

namespace WarhammerFightSimulator.Services;
public interface IAttackSetUp{
    public void ChooseAttack();
}

public class AttackSetUp(IDiceRolls diceRolls, RoundHistory roundHistory, Dictionary<Guid, CharacterStatus> statuses, Character attacking, Character defending) : IAttackSetUp{
    private readonly IDiceRolls _diceRolls = diceRolls;
    private readonly RoundHistory _roundHistory = roundHistory;
    readonly Dictionary<Guid, CharacterStatus> _statuses = statuses;
    readonly Character _attacking = attacking;
    readonly Character _defending = defending;

    public void ChooseAttack()
    {
        if (_attacking.Hands.RightHand == null && _attacking.Hands.LeftHand == null)
        {
            UnarmedLogic();
        }
        else if (_attacking.Hands.RightHand != null){
            if(_attacking.Hands.RightHand.EquipmentItemType == EquipmentItemType.MeleeWeapon){
                MeleeLogic((MeleeWeapon)_attacking.Hands.RightHand);
            }
        }
        else if(_attacking.Hands.LeftHand!= null){            
            if(_attacking.Hands.LeftHand.EquipmentItemType == EquipmentItemType.MeleeWeapon){
                _statuses[_attacking.Guid].AttackMod += 
                    StatsModifications.LeftHandMod((MeleeWeapon)_attacking.Hands.LeftHand, _attacking);                
                MeleeLogic((MeleeWeapon)_attacking.Hands.LeftHand);
            }
        }
    }
    private void MeleeLogic(MeleeWeapon _attackingWeapon){
        
        _statuses[_attacking.Guid].IsDodging = true;
        _statuses[_attacking.Guid].AttacksCount = _attacking.A;

        if (_attacking.A > 1)
        {
            StatsModifications.BlyskawicznyBlok(_attacking, _statuses[_attacking.Guid]);
            DoAttack(new MeleeAttack(
                _attacking, _defending, _diceRolls, _statuses), _attackingWeapon);
            
        }
        else
        {
            DoAttack(new MeleeAttack(_attacking, _defending, _diceRolls, _statuses), _attackingWeapon);
            _statuses[_attacking.Guid].IsParring = true;
        }
    }

    private void UnarmedLogic(){
        _statuses[_attacking.Guid].IsDodging = true;
        _statuses[_attacking.Guid].AttacksCount = _attacking.A;
        DoAttack(new UnarmedAttack(_attacking, _defending, _diceRolls, _statuses),
            null);
    }

    private void DoAttack(Attack attackType, Weapon? _attackingWeapon){
        for(int i = 0; i < _statuses[_attacking.Guid].AttacksCount; i++){
            _roundHistory.Rounds.Add(attackType.MakeAttack(_attackingWeapon));
        }
    }
}
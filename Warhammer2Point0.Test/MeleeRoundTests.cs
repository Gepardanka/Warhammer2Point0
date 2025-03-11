using WarhammerFightSimulator.Services;
using WarhammerFightSimulator.Models;
namespace WarhammerFightSimulator.Tests;

public class MeleeRoundTests{
    [Fact]
    public void FailHitRollRightHand(){
        Character attacking = new(){
            WW = 10,
            Hands = new Hands{
                RightHand = new MeleeWeapon{
                    WeaponName = WeaponName.Sword
        }}};
        Character defending = new(){CurrentZyw = 1, Guid = Guid.NewGuid()};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [90]};
        MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        Round expectedRound = new Round{
            AttackingWeaponName = WeaponName.Sword,
            HitSuccessFailReason = HitSuccessMissReason.Miss,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, meleeAttack.MakeAttack(attacking.Hands.RightHand));
    }

    [Fact]
    public void SucceedAttackRightHand(){
        Character attacking = new(){
            WW = 10,
            Hands = new Hands{
                RightHand = new MeleeWeapon{
                    WeaponName = WeaponName.Sword,
                    Modifier = 0
        }}};
        Character defending = new(){CurrentZyw = 1, Guid = Guid.NewGuid()};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1]};
        MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        Round expectedRound = new Round{
            AttackingWeaponName = WeaponName.Sword,
            HitSuccessFailReason = HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, meleeAttack.MakeAttack(attacking.Hands.RightHand));    
    }
}
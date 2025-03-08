using WarhammerFightSimulator.Services;
using WarhammerFightSimulator.Models;
namespace WarhammerFightSimulator.Tests;

public class MeleeRoundTests{
    [Fact]
    public void FailHitRollRightHand(){
        Round round = new Round{};
        Character attacking = new(){
            WW = 10,
            Hands = new Hands{
                RightHand = new MeleeWeapon{
                    WeaponName = "Sword"
        }}};
        Character defending = new(){CurrentZyw = 1};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [90]};
        MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand, round);

        Round expectedRound = new Round{
            AttackingWeaponName = "Sword",
            HitSuccessFailReason = HitSuccessMissReason.Miss,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, round);
    }

    [Fact]
    public void SucceedAttackRightHand(){
        Round round = new Round{};
        Character attacking = new(){
            WW = 10,
            Hands = new Hands{
                RightHand = new MeleeWeapon{
                    Modifier = 0
        }}};
        Character defending = new(){CurrentZyw = 1};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1]};
        MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand, round);

        Round expectedRound = new Round{
            AttackingWeaponName = "",
            HitSuccessFailReason = HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, round);    
    }
}
using WarhammerFightSimulator.Services;

namespace WarhammerFightSimulator.Tests;

public class RoundTests{
    [Fact]
    public void UnarmedRoundMiss(){
        Models.Round round = new Models.Round{};
        Models.Character defending = new(){CurrentZyw = 1};
        Models.Character attacking = new() {WW = 40};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [90]};
        Attack attack = new UnarmedAttack(attacking, defending, fakeDiceRolls, 0);

        attack.MakeAttack(null,round);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = "Unarmed",
            HitSuccessFailReason = Models.HitSuccessMissReason.Miss,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, round);
    }

    [Fact]
    public void UnarmedRoundHit(){
        Models.Round round = new Models.Round{};
        Models.Character defending = new(){CurrentZyw = 1, Wt = 1};
        Models.Character attacking = new(){WW = 40, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [30], IntsD10 = [2]};
        Attack attack = new UnarmedAttack(attacking, defending, fakeDiceRolls, 0);

        attack.MakeAttack(null,round);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = "Unarmed",
            HitSuccessFailReason = Models.HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, round);
    }

    [Fact]
    public void UnarmedRoundParry(){
        Models.Round round = new Models.Round{};
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0}},
            IsParring = true
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null, round);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = "Unarmed",
            HitSuccessFailReason = Models.HitSuccessMissReason.Parry,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, round);
    }

    [Fact]
     public void UnarmedRuondFailedParry(){
        Models.Round round = new Models.Round{};
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0}},
            IsParring = true
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null,round);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = "Unarmed",
            HitSuccessFailReason = Models.HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, round);
    }

    [Fact]
    public void UnarmedDodge(){
        Models.Round round = new Models.Round{};
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Zr = 90,
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null, round);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = "Unarmed",
            HitSuccessFailReason = Models.HitSuccessMissReason.Dodge,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, round);    
    }

    [Fact]
    public void UnarmedFailedDodge(){
        Models.Round round = new Models.Round{};
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Zr = 90,
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null, round);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = "Unarmed",
            HitSuccessFailReason = Models.HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, round); 
    }
}
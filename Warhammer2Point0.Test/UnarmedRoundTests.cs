using WarhammerFightSimulator.Services;

namespace WarhammerFightSimulator.Tests;

public class RoundTests{
    [Fact]
    public void UnarmedRoundMiss(){
        Models.Character defending = new(){CurrentZyw = 1, Guid = Guid.NewGuid()};
        Models.Character attacking = new() {WW = 40};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [90]};
        Attack attack = new UnarmedAttack(attacking, defending, fakeDiceRolls, 0);


        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = Models.WeaponName.Unarmed,
            HitSuccessFailReason = Models.HitSuccessMissReason.Miss,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, attack.MakeAttack(null));
    }

    [Fact]
    public void UnarmedRoundHit(){
        Models.Character defending = new(){CurrentZyw = 1, Wt = 1, Guid = Guid.NewGuid()};
        Models.Character attacking = new(){WW = 40, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [30], IntsD10 = [2]};
        Attack attack = new UnarmedAttack(attacking, defending, fakeDiceRolls, 0);


        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = Models.WeaponName.Unarmed,
            HitSuccessFailReason = Models.HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, attack.MakeAttack(null));
    }

    [Fact]
    public void UnarmedRoundParry(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0}},
            IsParring = true,
            Guid = Guid.NewGuid(),
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = Models.WeaponName.Unarmed,
            HitSuccessFailReason = Models.HitSuccessMissReason.Parry,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, unarmedAttack.MakeAttack(null));
    }

    [Fact]
     public void UnarmedRuondFailedParry(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0}},
            Guid = Guid.NewGuid(),
            IsParring = true
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = Models.WeaponName.Unarmed,
            HitSuccessFailReason = Models.HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, unarmedAttack.MakeAttack(null));
    }

    [Fact]
    public void UnarmedDodge(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Zr = 90,
            Guid = Guid.NewGuid(),
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = Models.WeaponName.Unarmed,
            HitSuccessFailReason = Models.HitSuccessMissReason.Dodge,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 1
        };

        Assert.Equivalent(expectedRound, unarmedAttack.MakeAttack(null));    
    }

    [Fact]
    public void UnarmedFailedDodge(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Zr = 90,
            Guid = Guid.NewGuid(),
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);

        Models.Round expectedRound = new Models.Round{
            AttackingWeaponName = Models.WeaponName.Unarmed,
            HitSuccessFailReason = Models.HitSuccessMissReason.Hit,
            DefendingCharID = defending.Guid,
            DefendingCharCurrentHP = 0
        };

        Assert.Equivalent(expectedRound, unarmedAttack.MakeAttack(null)); 
    }
}
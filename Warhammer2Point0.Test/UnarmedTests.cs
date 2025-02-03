using WarhammerFightSimulator.Models;
using WarhammerFightSimulator.Services;

namespace WarhammerFightSimulator.Tests;
public class UnarmedTests{
    [Fact]
    public void UnarmedAttackWWTest(){
        Models.Character defending = new(){CurrentZyw = 1};
        Models.Character attacking = new() {WW = 40};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [90]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);
    }
    [Fact]
    public void UnarmedAttackSuccess(){
        Models.Character defending = new(){CurrentZyw = 1, Wt = 1};
        Models.Character attacking = new(){WW = 40, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [30], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, defending.CurrentZyw);
    }
    [Fact]
    public void UnarmedAttackSuccessNoHealthTaken(){
        Models.Character defending = new(){CurrentZyw = 1, Wt = 2};
        Models.Character attacking = new(){WW = 40, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [30], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);        
    }
    [Fact]
    public void UnarmedHeadHitPlace(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Armour = new Models.Armour{Head = 2}
            };
        Models.Character attacking = new(){WW = 50, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [1], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);         
    }
    [Fact]
    public void UnarmedBodyHitPlace(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Armour = new Models.Armour{Body = 2}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [65], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);         
    }
    [Fact]
    public void UnarmedDoubleArmour(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Armour = new Models.Armour{Head = 1}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10], IntsD10 = [3]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);          
    }
    [Fact]
    public void UnarmedDodge(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Zr = 90,
            Skills = new Dictionary<Models.CharacterSkill, int>{{CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);
        Assert.False(defending.IsDodging);         
    }
    [Fact]
    public void UnarmedFailedDodge(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            Zr = 90,
            Skills = new Dictionary<Models.CharacterSkill, int>{{CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, defending.CurrentZyw);
        Assert.False(defending.IsDodging);          
    }

    [Fact]
    public void UnarmedParry(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new MeleeWeapon{
                    Modifier = 0}},
            IsParring = true
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);
        Assert.False(defending.IsParring);
    }
    [Fact]
     public void UnarmedFailedParry(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new MeleeWeapon{
                    Modifier = 0}},
            IsParring = true
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, defending.CurrentZyw);
        Assert.False(defending.IsParring);
    }
    [Fact]
    public void UnarmedBijatyka(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1};
        Models.Character attacking = new(){
            WW = 50, 
            S = 4,
            Abilities = new List<CharacterAbility>{CharacterAbility.Bijatyka}};

        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [40], IntsD10 = [1]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 0);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, defending.CurrentZyw);        
    }
    [Fact]
    public void UnarmedGroupBonus(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1};
        Models.Character attacking = new(){
            WW = 50, 
            S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [60], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 10);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, defending.CurrentZyw);         
    }
    [Fact]
    public void UnarmedGroupBonusFail(){
        Models.Character defending = new(){
            CurrentZyw = 1, 
            Wt = 1};
        Models.Character attacking = new(){
            WW = 40, 
            S = 4};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [60], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, 10);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, defending.CurrentZyw);         
    }
}
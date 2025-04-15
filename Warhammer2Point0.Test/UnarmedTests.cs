using WarhammerFightSimulator.Models;
using WarhammerFightSimulator.Services;

namespace WarhammerFightSimulator.Tests;
public class UnarmedTests{
    [Fact]
    public void UnarmedAttackWWTest(){
        Models.Character defending = new(){Guid = Guid.NewGuid()};
        Models.Character attacking = new() {WW = 40};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [90]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);
    }
    [Fact]
    public void UnarmedAttackSuccess(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(), 
            Wt = 1};
        Models.Character attacking = new(){WW = 40, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [30], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, statuses[defending.Guid].CurrentZyw);
    }
    [Fact]
    public void UnarmedAttackSuccessNoHealthTaken(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(), 
            Wt = 2};
        Models.Character attacking = new(){WW = 40, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [30], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);        
    }
    [Fact]
    public void UnarmedHeadHitPlace(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1,
            Armour = new Models.Armour{Head = 2}
            };
        Models.Character attacking = new(){WW = 50, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [1], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);         
    }
    [Fact]
    public void UnarmedBodyHitPlace(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1,
            Armour = new Models.Armour{Body = 2}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [65], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);         
    }
    [Fact]
    public void UnarmedDoubleArmour(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1,
            Armour = new Models.Armour{Head = 1}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10], IntsD10 = [3]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);          
    }
    [Fact]
    public void UnarmedDodge(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1,
            Zr = 90,
            Skills = new Dictionary<Models.CharacterSkill, int>{{CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);
        Assert.False(statuses[defending.Guid].IsDodging);         
    }
    [Fact]
    public void UnarmedFailedDodge(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1,
            Zr = 90,
            Skills = new Dictionary<Models.CharacterSkill, int>{{CharacterSkill.Unik, 0}}
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, statuses[defending.Guid].CurrentZyw);
        Assert.False(statuses[defending.Guid].IsDodging);          
    }

    [Fact]
    public void UnarmedParry(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new MeleeWeapon{
                    Modifier = 0}},
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1, IsParring = true}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 10], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);
        Assert.False(statuses[defending.Guid].IsParring);
    }
    [Fact]
     public void UnarmedFailedParry(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1,
            WW = 90,
            Hands = new Models.Hands{
                RightHand = new MeleeWeapon{
                    Modifier = 0}},
            };
        Models.Character attacking = new(){WW = 90, S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1, IsParring = true}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [10, 91], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, statuses[defending.Guid].CurrentZyw);
        Assert.False(statuses[defending.Guid].IsParring);
    }
    [Fact]
    public void UnarmedBijatyka(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1};
        Models.Character attacking = new(){
            WW = 50, 
            S = 4,
            Abilities = new List<CharacterAbility>{CharacterAbility.Bijatyka}};

        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [40], IntsD10 = [1]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, statuses[defending.Guid].CurrentZyw);        
    }
    [Fact]
    public void UnarmedGroupBonus(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1};
        Models.Character attacking = new(){
            WW = 50, 
            S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{AttackMod = 10}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [60], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(0, statuses[defending.Guid].CurrentZyw);         
    }
    [Fact]
    public void UnarmedGroupBonusFail(){
        Models.Character defending = new(){
            Guid = Guid.NewGuid(),
            Wt = 1};
        Models.Character attacking = new(){
            WW = 40, 
            S = 4};
        Dictionary<Guid, Models.CharacterStatus> statuses = new(){
            {attacking.Guid, new Models.CharacterStatus{AttackMod = 10}},
            {defending.Guid, new Models.CharacterStatus{CurrentZyw = 1}},
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [60], IntsD10 = [2]};
        UnarmedAttack unarmedAttack = new(attacking, defending, fakeDiceRolls, statuses);
        unarmedAttack.MakeAttack(null);
        Assert.Equal(1, statuses[defending.Guid].CurrentZyw);         
    }
}
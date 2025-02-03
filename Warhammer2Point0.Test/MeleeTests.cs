namespace WarhammerFightSimulator.Tests;

public class MeleeTests{
    [Fact]
    public void FailHitRollRightHand(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
        }}};
        Models.Character defending = new(){CurrentZyw = 1};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [90]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw); 
    }
    [Fact]
    public void SucceedAttackRightHand(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0
        }}};
        Models.Character defending = new(){CurrentZyw = 1};
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(0, defending.CurrentZyw);         
    }
    [Fact]
    public void ChooseDodgeOverParry(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            WW = 10,
            Zr = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0}},
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 10}},
            IsParring = true
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9, 15], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);          
    }
    [Fact]
    public void ChooseParryOverDodge(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            WW = 20,
            Zr = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0}},
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 0}},
            IsParring = true
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9, 15], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);  
    }
    [Fact]
    public void SucceedHitFailDamage(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            Wt = 1,
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);  
    }
    [Fact]
    public void ParryWithShield(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            WW = 20,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0},
                LeftHand = new Models.MeleeWeapon{
                    Modifier = -2,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.Parujacy}
                }},
            IsParring = true
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9, 30], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);          
    }
    [Fact]
    public void ParryWolny(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.Powolny}
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            WW = 20,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0
                }},
            IsParring = true
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9, 30], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);             
    }
    [Fact]
    public void ParrySzybki(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.Szybki}
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            WW = 20,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0
                }},
            IsParring = true
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9, 20], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(0, defending.CurrentZyw);            
    }
    [Fact]
    public void DodgeWolny(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.Powolny}
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            Zr = 20,
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 0}}
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9, 30], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);            
    }
    [Fact]
    public void DodgeSzybki(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.Szybki}
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            Zr = 20,
            Skills = new Dictionary<Models.CharacterSkill, int>{{Models.CharacterSkill.Unik, 0}}
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9, 20], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(0, defending.CurrentZyw);           
    }
    [Fact]
    public void DruzgoczacyAttack(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.Druzgoczacy}
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            Wt = 1
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1, 2]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(0, defending.CurrentZyw);        
    }
    [Fact]
    public void SilnyCiosAttack(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0}},
            Abilities = new List<Models.CharacterAbility>{Models.CharacterAbility.SilnyCios}
            };
        Models.Character defending = new(){
            CurrentZyw = 1,
            Wt = 1
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(0, defending.CurrentZyw);    
    }
    [Fact]
    public void PrzebijajacyZbrojeAttack(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.PrzebijajacyZbroje}
        }}};
        Models.Character defending = new(){
            CurrentZyw = 1,
            Armour = new Models.Armour{RightLeg = 1}
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(0, defending.CurrentZyw);        
    }
    [Fact]
    public void PrzebijajacyZbrojeNoArmourAttack(){
        Models.Character attacking = new(){
            WW = 10,
            Hands = new Models.Hands{
                RightHand = new Models.MeleeWeapon{
                    Modifier = 0,
                    WeaponTraits = new List<Models.WeaponTrait>{Models.WeaponTrait.PrzebijajacyZbroje}
        }}};
        Models.Character defending = new(){
            CurrentZyw = 2,
        };
        FakeDiceRolls fakeDiceRolls = new(){IntsD100 = [9], IntsD10 = [1]};
        Services.MeleeAttack meleeAttack = new(attacking, defending, fakeDiceRolls, 0);
        meleeAttack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);         
    }
}
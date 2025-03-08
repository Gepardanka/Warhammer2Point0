namespace WarhammerFightSimulator.Tests;

public class RangedTests{
    [Fact]
    public void MissTest(){
        FakeDiceRolls fakeDiceRolls= new(){IntsD100 = [20]};
        Models.Character attacking = new(){
            US = 10,
            Hands = new Models.Hands{RightHand = new Models.RangedWeapon{Modifier = 3}}
        };
        Models.Character defending = new(){
            CurrentZyw = 1
        };
        Services.RangedAttack attack= new(attacking, defending, fakeDiceRolls);
        attack.MakeAttack(attacking.Hands.RightHand);
        Assert.Equal(1, defending.CurrentZyw);
    }
}
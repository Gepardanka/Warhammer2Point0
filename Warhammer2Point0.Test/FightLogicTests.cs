namespace WarhammerFightSimulator.Tests;

public class FightLogicTests{
    [Fact]
    public void OneHitKillFight(){
        List<Models.Character> fighters = [
            new Models.Character{
                Team = Models.CharacterTeam.TeamA,
                WW = 10,
                A = 1,
                CurrentZyw = 1,
                Hands = new Models.Hands{
                    RightHand = new Models.MeleeWeapon{
                        Modifier = 0
                    }
                }
            },
            new Models.Character{
                Team = Models.CharacterTeam.TeamB,
                CurrentZyw = 1
            }
        ];
        FakeDiceRolls fakeDiceRolls = new() {
            IntsD10 = [2, 1, 1],
            IntsD100 = [9]
        };
        Models.RoundHistory roundHistory = new();
        Services.AttackSetUp attackSetUp = new(fakeDiceRolls, roundHistory);
        Services.FightSimulator fightSimulator = new(fighters, fakeDiceRolls, attackSetUp, roundHistory);
        Models.CharacterTeam winnerTeam = fightSimulator.Fight().WinnerTeam;
        Assert.Equal(Models.CharacterTeam.TeamA, winnerTeam);
    }
    [Fact]
    public void FivePepsFight(){
        List<Models.Character> fighters = new(){
            new Models.Character{
                Team = Models.CharacterTeam.TeamA,
                WW = 90,
                Zr = 90,
                A = 3,
                S = 4,
                Wt = 6,
                CurrentZyw = 16,
                Hands = new Models.Hands{
                    RightHand = new Models.MeleeWeapon{
                        Modifier = 1
                    }
                }
            },
            new Models.Character{
                Team = Models.CharacterTeam.TeamA,
                WW = 60,
                Zr = 60,
                A = 3,
                S = 4,
                Wt = 6,
                CurrentZyw = 16,
                Hands = new Models.Hands{
                    RightHand = new Models.MeleeWeapon{
                        Modifier = 1
                    }
                }
            },
            new Models.Character{
                Team = Models.CharacterTeam.TeamA,
                WW = 60,
                Zr = 60,
                A = 3,
                S = 4,
                Wt = 6,
                CurrentZyw = 16,
                Hands = new Models.Hands{
                    RightHand = new Models.MeleeWeapon{
                        Modifier = 1
                    }
                }
            },
            new Models.Character{
                Team = Models.CharacterTeam.TeamB,
                WW = 35,
                Zr = 35,
                A = 1,
                S = 4,
                Wt = 6,
                CurrentZyw = 15,
                Hands = new Models.Hands{
                    RightHand = new Models.MeleeWeapon{
                        Modifier = 0
                    }
                }
            },
            new Models.Character{
                Team = Models.CharacterTeam.TeamB,
                WW = 35,
                Zr = 35,
                A = 1,
                S = 4,
                Wt = 6,
                CurrentZyw = 15,
                Hands = new Models.Hands{
                    RightHand = new Models.MeleeWeapon{
                        Modifier = 0
                    }
                }
            },
        };
        DiceRolls diceRolls = new();
        Models.RoundHistory roundHistory = new();
        Services.AttackSetUp attackSetUp = new(diceRolls, roundHistory);
        Services.FightSimulator fightSimulator = new(fighters, diceRolls, attackSetUp, roundHistory);

        var wins = new List<Models.CharacterTeam>();
        for(int i = 0; i < 1000; i++){
            wins.Add(fightSimulator.Fight().WinnerTeam);
        }
        var fail = wins.Any(team => team == Models.CharacterTeam.TeamB);
        Assert.False(fail);
    }
}
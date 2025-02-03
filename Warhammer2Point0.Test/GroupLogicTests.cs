namespace WarhammerFightSimulator.Tests;

public class GroupLogicTests
{
    [Fact]
    public void MakeGroups2Characters()
    {
        var characters = new List<Models.Character>{
                new Models.Character{
                    Team = Models.CharacterTeam.TeamA
                },
                new Models.Character{
                    Team = Models.CharacterTeam.TeamB
                }};
        var groups = GroupLogic.MakeGroups(characters);
        Assert.Single(groups);
        Assert.Equal(2, groups[0].Count);
    }
    [Fact]
    public void MakeGroupsMany(){
        var characters = new List<Models.Character>{
                new Models.Character{
                    Team = Models.CharacterTeam.TeamA
                },
                new Models.Character{
                    Team = Models.CharacterTeam.TeamA
                },
                new Models.Character{
                    Team = Models.CharacterTeam.TeamB
                },
                new Models.Character{
                    Team = Models.CharacterTeam.TeamB
                },
                new Models.Character{
                    Team = Models.CharacterTeam.TeamB
                },
                new Models.Character{
                    Team = Models.CharacterTeam.TeamB
                },
                new Models.Character{
                    Team = Models.CharacterTeam.TeamB
                }};
        var groups = GroupLogic.MakeGroups(characters);
        Assert.Equal(2, groups.Count);
        var group1Count = groups.Count(x => {
            return x.Count(y => y.Team == Models.CharacterTeam.TeamA) == 1
                && x.Count(y => y.Team == Models.CharacterTeam.TeamB) == 2;
        });  
        var group2Count = groups.Count(x =>{
            return x.Count(y => y.Team == Models.CharacterTeam.TeamA) == 1
                && x.Count(y => y.Team == Models.CharacterTeam.TeamB) == 3;
        });
        Assert.Equal(1, group1Count);
        Assert.Equal(1, group2Count);
    }
    [Fact]
    public void Initiative2Char(){
        var diceRolls = new FakeDiceRolls{
            IntsD10 = [0, 5]
        };

        var characters = new List<Models.Character>{
                new Models.Character{
                    Zr = 30
                },
                new Models.Character{
                    Zr = 29
                }};
                
        var initiative = GroupLogic.Initiative(characters, diceRolls).ToList();
        Assert.Equal(29, initiative[0].Zr);
    }
    [Fact]
    public void EqualRollInit(){
        var diceRolls = new FakeDiceRolls{
            IntsD10 = [0, 5]
        };
        var characters = new List<Models.Character>{
                new Models.Character{
                    Zr = 30
                },
                new Models.Character{
                    Zr = 25
                }};
        var initiative = GroupLogic.Initiative(characters, diceRolls).ToList();
        Assert.Equal(30, initiative[0].Zr);        
    }
    [Fact]
    public void ReassignCharacterA(){
        Models.Character toReassign = new Models.Character{Team = Models.CharacterTeam.TeamA};
        List<List<Models.Character>> groups = new List<List<Models.Character>>{
            new List<Models.Character>{
                new Models.Character{Team = Models.CharacterTeam.TeamA},
                new Models.Character{Team = Models.CharacterTeam.TeamB}
            }
        };
        GroupLogic.ReassignToGroups(toReassign, groups);
        var groupCount = groups.Count(x => {
            return x.Count(y => y.Team == Models.CharacterTeam.TeamA) == 2
                && x.Count(y => y.Team == Models.CharacterTeam.TeamB) == 1;
        });
        Assert.Equal(1, groupCount);
        Assert.Single(groups);
    }
    [Fact]
    public void ReassignCharacterNewGroup(){
        Models.Character toReassign = new Models.Character{Team = Models.CharacterTeam.TeamA};
        List<List<Models.Character>> groups = new List<List<Models.Character>>{
            new List<Models.Character>{
                new Models.Character{Team = Models.CharacterTeam.TeamA},
                new Models.Character{Team = Models.CharacterTeam.TeamB},
                new Models.Character{Team = Models.CharacterTeam.TeamB}
            }
        };
        GroupLogic.ReassignToGroups(toReassign, groups);
        var groupCount = groups.Count(x => {
            return x.Count(y => y.Team == Models.CharacterTeam.TeamA) == 1
                && x.Count(y => y.Team == Models.CharacterTeam.TeamB) == 1;
        });
        Assert.Equal(2, groupCount);
        Assert.Equal(2, groups.Count);
    }
    [Fact]
    public void ReassignJoinRightChar(){
        Models.Character toReassign = new Models.Character{Team = Models.CharacterTeam.TeamA};
        List<List<Models.Character>> groups = new List<List<Models.Character>>{
            new List<Models.Character>{
                new Models.Character{Team = Models.CharacterTeam.TeamA},
                new Models.Character{Team = Models.CharacterTeam.TeamA},
                new Models.Character{Team = Models.CharacterTeam.TeamB}
            },
            new List<Models.Character>{
                new Models.Character{Team = Models.CharacterTeam.TeamA},
                new Models.Character{Team = Models.CharacterTeam.TeamB}
            }
        };
        GroupLogic.ReassignToGroups(toReassign, groups);
        Assert.Equal(2, groups.Count);
        Assert.Equal(3, groups[1].Count);
    }
    [Fact]
    public void ReassignStealRightChar(){
        Models.Character toReassign = new Models.Character{Team = Models.CharacterTeam.TeamA};
        List<List<Models.Character>> groups = new List<List<Models.Character>>{
            new List<Models.Character>{
                new Models.Character{Team = Models.CharacterTeam.TeamA},
                new Models.Character{Team = Models.CharacterTeam.TeamB},
                new Models.Character{Team = Models.CharacterTeam.TeamB}
            },
            new List<Models.Character>{
                new Models.Character{Team = Models.CharacterTeam.TeamA},
                new Models.Character{Team = Models.CharacterTeam.TeamB},
                new Models.Character{Team = Models.CharacterTeam.TeamB},
                new Models.Character{Team = Models.CharacterTeam.TeamB}
            }
        };
        GroupLogic.ReassignToGroups(toReassign, groups);
        Assert.Equal(3, groups.Count);
        Assert.Equal(3, groups[1].Count);  
    }
}

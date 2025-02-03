using System.Diagnostics;
using WarhammerFightSimulator.Models;
namespace WarhammerFightSimulator.Services;
public class FightSimulator
{
    readonly List<Character> _characters;
    readonly IDiceRolls _diceRolls;
    readonly IAttackSetUp _attackSetUp;
    public FightSimulator(List<Character> characters, IDiceRolls diceRolls, IAttackSetUp attackSetUp)
    {
        _characters = characters;
        _diceRolls = diceRolls;
        _attackSetUp = attackSetUp;
    }
    public CharacterTeam Fight()
    {
        List<Character> inBattle = GroupLogic.Initiative(_characters, _diceRolls).ToList();
        List<List<Character>> groups = GroupLogic.MakeGroups(inBattle);
        int stop = 10000;
        for (int i = 0; i < stop; i++)
        {
            for (int j = 0; j < inBattle.Count; j++)
            {
                CharacterTeam? winner = CheckForWinner(inBattle);
                if(winner.HasValue){return (CharacterTeam)winner; }

                Character attacking = inBattle[j];
                List<Character> group = groups.First(x => x.Contains(attacking));
                if(attacking.CurrentZyw <= 0){
                    if(!group.Any(x => x.CurrentZyw > 0)){
                        groups.Remove(group);
                    }
                    continue;
                }
                
                if(!group.Any(x => x.Team != attacking.Team
                    && x.CurrentZyw > 0)){
                   GroupLogic.ReassignToGroups(attacking, groups.Where(x => x.Any(y => y.CurrentZyw > 0)).ToList());
                   continue;
                }
                Character defending = group.Where(x => x.CurrentZyw > 0).First(x => x.Team != attacking.Team);
                
                _attackSetUp.ChooseAttack(attacking, defending, StatsModifications.GroupMod(group, attacking.Team));
            }
        }
        throw new Exception($"Fight didn't finish in {stop} rounds;");
    }
    private CharacterTeam? CheckForWinner(List<Character> inBattle){
        if (!inBattle.Where(x => x.Team == CharacterTeam.TeamA
            && x.CurrentZyw > 0).Any())
        {
            return CharacterTeam.TeamB;
        }
        if (!inBattle.Where(x => x.Team == CharacterTeam.TeamB
            && x.CurrentZyw > 0).Any())
        {
            return CharacterTeam.TeamA;
        }
        return null;
    }
}
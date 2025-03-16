using WarhammerFightSimulator.Models;
namespace WarhammerFightSimulator.Services;
public class FightSimulator
{
    readonly List<Character> _characters;
    readonly IDiceRolls _diceRolls;
    readonly IAttackSetUp _attackSetUp;
    RoundHistory _roundHistory;
    public FightSimulator(List<Character> characters, IDiceRolls diceRolls, IAttackSetUp attackSetUp, RoundHistory roundHistory)
    {
        _roundHistory = roundHistory;
        _characters = characters;
        _diceRolls = diceRolls;
        _attackSetUp = attackSetUp;
    }
    public RoundHistory Fight()
    {
        List<Character> inBattle = GroupLogic.Initiative(_characters, _diceRolls).ToList();
        List<List<Character>> groups = GroupLogic.MakeGroups(inBattle);
        _roundHistory.TeamA = inBattle.Where(x => x.Team == CharacterTeam.TeamA).Select(CharacterDTO.CharacterToDTO).ToList();
        _roundHistory.TeamB = inBattle.Where(x => x.Team == CharacterTeam.TeamB).Select(CharacterDTO.CharacterToDTO).ToList();

        int stop = 1000;
        for (int i = 0; i < stop; i++)
        {
            for (int j = 0; j < inBattle.Count; j++)
            {
                CharacterTeam? winner = CheckForWinner(inBattle);
                if(winner.HasValue){
                    _roundHistory.WinnerTeam = (CharacterTeam)winner;
                    return _roundHistory;
                }

                Character attacking = inBattle[j];
                if(attacking.CurrentZyw <1){continue;}
                List<Character> group = groups.First(x => x.Contains(attacking));

                
                if(!group.Any(x => x.Team != attacking.Team)){
                   GroupLogic.ReassignToGroups(attacking, groups);
                   System.Console.WriteLine("Reassigned group");
                   continue;
                }
                Character defending = group.Where(x => x.CurrentZyw > 0).First(x => x.Team != attacking.Team);

                _attackSetUp.ChooseAttack(attacking, defending, StatsModifications.GroupMod(group, attacking.Team));
                GroupLogic.RemoveCorpse(groups);
            }
            
        }
        System.Console.WriteLine(_roundHistory.Rounds.Count);
        _roundHistory.WinnerTeam = TeamMoreHP(inBattle);
        return _roundHistory;
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

    private CharacterTeam TeamMoreHP(List<Character> characters){
        int aHP = 0;
        int bHP = 0; 
        foreach(var character in characters){
            if(character.Team == CharacterTeam.TeamA && character.CurrentZyw > 0){
                aHP += character.CurrentZyw;
            }
            else if(character.CurrentZyw > 0){
                bHP += character.CurrentZyw;
            }
        }
        if(aHP > bHP){ return CharacterTeam.TeamA;}
        return CharacterTeam.TeamB;
    }
}
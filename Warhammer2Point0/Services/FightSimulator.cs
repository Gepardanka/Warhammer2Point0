using WarhammerFightSimulator.Models;
namespace WarhammerFightSimulator.Services;
public class FightSimulator
{
    readonly List<Character> _characters;
    readonly IDiceRolls _diceRolls;
    readonly RoundHistory _roundHistory;
    readonly Dictionary<Guid, CharacterStatus> _statuses;
    public FightSimulator(List<Character> characters, IDiceRolls diceRolls)
    {
        _roundHistory = new RoundHistory();
        _characters = characters;
        _diceRolls = diceRolls;
        _statuses = characters.ToDictionary(
            key => key.Guid,
            character => new CharacterStatus{
                CurrentZyw = character.Zyw,
                AttacksCount = character.A
            }
        );
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
                if(_statuses[attacking.Guid].CurrentZyw <1){continue;}
                List<Character> group = groups.First(x => x.Contains(attacking));

                
                if(!group.Any(x => x.Team != attacking.Team)){
                   GroupLogic.ReassignToGroups(attacking, groups);
                   System.Console.WriteLine("Reassigned group");
                   continue;
                }
                Character defending = group.Where(x => _statuses[x.Guid].CurrentZyw > 0).First(x => x.Team != attacking.Team);
                _statuses[attacking.Guid].AttackMod = StatsModifications.GroupMod(group, attacking.Team);

                AttackSetUp attackSetUp = new AttackSetUp(_diceRolls, _roundHistory, _statuses, attacking, defending);
                attackSetUp.ChooseAttack();
                GroupLogic.RemoveCorpse(groups, _statuses);
            }
            
        }
        System.Console.WriteLine(_roundHistory.Rounds.Count);
        _roundHistory.WinnerTeam = TeamMoreHP(inBattle);
        return _roundHistory;
    }
    private CharacterTeam? CheckForWinner(List<Character> inBattle){
        if (!inBattle.Where(x => x.Team == CharacterTeam.TeamA
            && _statuses[x.Guid].CurrentZyw > 0).Any())
        {
            return CharacterTeam.TeamB;
        }
        if (!inBattle.Where(x => x.Team == CharacterTeam.TeamB
            && _statuses[x.Guid].CurrentZyw > 0).Any())
        {
            return CharacterTeam.TeamA;
        }
        return null;
    }

    private CharacterTeam TeamMoreHP(List<Character> characters){
        int aHP = 0;
        int bHP = 0; 
        foreach(var character in characters){
            if(character.Team == CharacterTeam.TeamA && _statuses[character.Guid].CurrentZyw > 0){
                aHP += _statuses[character.Guid].CurrentZyw;
            }
            else if(_statuses[character.Guid].CurrentZyw > 0){
                bHP += _statuses[character.Guid].CurrentZyw;
            }
        }
        if(aHP > bHP){ return CharacterTeam.TeamA;}
        return CharacterTeam.TeamB;
    }
}
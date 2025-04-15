using System.Diagnostics;
using WarhammerFightSimulator.Models;
public static class GroupLogic{
    public static IEnumerable<Character> Initiative(List<Character> characters, IDiceRolls diceRolls)
    {
        List<(int, Character)> initOrdered = [];
        foreach (Character character in characters)
        {
            initOrdered.Add((character.Zr + diceRolls.D10(1), character));
        }
        initOrdered = initOrdered.OrderByDescending(x => x.Item1).ThenByDescending(x => x.Item2.Zr).ToList();
        return initOrdered.Select(x => x.Item2);
    }
    public static List<List<Character>> MakeGroups(List<Character> characters)
    {
        List<Character> teamA = characters.Where(x => x.Team == CharacterTeam.TeamA).ToList();
        List<Character> teamB = characters.Where(x => x.Team == CharacterTeam.TeamB).ToList();

        List<List<Character>> groups = teamA.Zip(teamB, (a, b) => new List<Character>{a, b}).ToList();
        int j = 0;

        for(int i = groups.Count; i < teamA.Count; i++){
            groups[j++].Add(teamA[i]);
            j %= groups.Count;
        }
        for(int i = groups.Count; i < teamB.Count; i++){
            groups[j++].Add(teamB[i]);
            j %= groups.Count;
        }
        return groups;
    }


    public static void ReassignToGroups(Character toReassign, List<List<Character>> groups){
        Debug.Assert(groups.Count != 0);
        var originGroup = groups.First(x => x.Contains(toReassign));

        var canReassign = groups.Where(gr => gr.Any(ch => ch.Team != toReassign.Team)).ToList();
        if(canReassign.Count == 0){return;}

        var lonelyGroup = canReassign.FirstOrDefault(gr => gr.Count() == 1);
        //check if can add to lonely enemy
        if(lonelyGroup != null){
            lonelyGroup.Add(toReassign);
        }
        else{
            //check if can steal from a group with the greatest enemy advantage
            var enemyAdvantage = canReassign.MaxBy(gr => gr.Count(ch => ch.Team != toReassign.Team));
            if(enemyAdvantage!.Count(ch => ch.Team != toReassign.Team) > 1){
                var toSteal = enemyAdvantage!.First(ch => ch.Team != toReassign.Team);
                groups.Add([toReassign, toSteal]);
                enemyAdvantage!.Remove(toSteal);
            }
            //if only one enemy in each group, join group with least friends
            else{
                var leastFriends = canReassign.MinBy(gr => gr.Count(ch => ch.Team == toReassign.Team));
                leastFriends!.Add(toReassign);
            }
        }
        originGroup.Remove(toReassign);
        if(originGroup.Count == 0){groups.Remove(originGroup);}
    }

    public static void RemoveCorpse(List<List<Character>> groups, Dictionary<Guid, CharacterStatus> statuses){
        for(int i = 0; i < groups.Count; i++){
            groups[i] = groups[i].Where(ch => statuses[ch.Guid].CurrentZyw > 0).ToList();
        }
    }
}
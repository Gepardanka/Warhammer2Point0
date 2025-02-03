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
    public static void ReassignToGroups(Character toReassign, List<List<Character>> groups)
    {
        Debug.Assert(groups.Count != 0);
        var canReassign = groups.Where(group => {
            return group.Count(ch => ch.Team == toReassign.Team) > 1 || group.Count == 2;
        }).ToList();
        if(canReassign.Count > 0){
            var toAdd = canReassign.MinBy(x => x.Count);
            toAdd!.Add(toReassign);
        }
        else{
            var toSteal = groups.MaxBy(x => x.Count)!;
            Character stollen = toSteal.First(x => x.Team != toReassign.Team);
            toSteal.Remove(stollen);
            groups.Add([toReassign, stollen]);
        }
    }
}
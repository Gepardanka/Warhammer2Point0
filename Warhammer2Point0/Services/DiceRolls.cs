public interface IDiceRolls{
    public int D100();
    public int D10(int dices);
    public int Opposing(int stat);
    public int HighestD10(int dices);
}
public class DiceRolls:IDiceRolls{
    private static readonly Random random = new();
    public int D100(){
        return random.Next(1, 101);
    }
    public int D10(int dices){
        int result = 0;
        for(int i = 0; i < dices; i++){
            result += random.Next(1, 11);
        }
        return result;
    }
    public int Opposing(int stat){
        return stat / D100();
    }
    public int HighestD10(int dices){
        List<int> results = [];
        for(int i = 0; i < dices; i++){
            results.Add(D10(1));
        }
        return results.Max();
    }
}
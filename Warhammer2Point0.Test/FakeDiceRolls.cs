public class FakeDiceRolls : IDiceRolls
{
    public int[] IntsD10 {get; set;} = [];
    public int[] IntsD100 {get; set;} = [];
    public int indexD10 = 0;
    public int indexD100 = 0;
    public int D10(int dices)
    {
        dices = indexD10;
        indexD10++;
        indexD10 %= IntsD10.Length;
        return IntsD10[dices];
    }

    public int D100()
    {
        int temp = indexD100;
        indexD100++;
        indexD100 %= IntsD100.Length;
        return IntsD100[temp];
    }

    public int HighestD10(int dices)
    {
        List<int> results = [];
        for(int i = 0; i < dices; i++){
            results.Add(D10(1));
        }
        return results.Max();
    }

    public int Opposing(int stat)
    {
        throw new NotImplementedException();
    }
}
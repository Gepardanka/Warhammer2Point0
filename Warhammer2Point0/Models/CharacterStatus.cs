namespace WarhammerFightSimulator.Models;
public class CharacterStatus
{
    public int CurrentZyw { get; set; }
    public int AttacksCount { get; set; }

    public bool IsParring { get; set; } = false;
    public bool IsDodging { get; set; } = true;
    public int AttackMod { get; set; } = 0;
}
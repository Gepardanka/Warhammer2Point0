namespace WarhammerFightSimulator.Models;
public class RoundHistory{
    public List<Round> Rounds { get; set; } = [];
    public List<CharacterDTO> TeamA { get; set; } =[];
    public List<CharacterDTO> TeamB { get; set; } =[];
    public CharacterTeam WinnerTeam { get; set; }
}
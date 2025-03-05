using WarhammerFightSimulator.Models;

public class BattleResultDTO{
    public required List<CharacterDTO> BattleCharacters { get; set; }
    public required CharacterTeam WinnerTeam { get; set; }
}


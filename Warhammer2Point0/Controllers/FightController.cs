using Microsoft.AspNetCore.Mvc;
using WarhammerFightSimulator.Models;
using WarhammerFightSimulator.Services;

namespace Warhammer2Point0.Controllers;

[ApiController]
[Route("fight")]
public class FightController: ControllerBase{

    [HttpGet]
    [Route("defaultChar")]
    public IEnumerable<CharacterDTO> GetDefaultChar(){
        return new DefaultCharacters().characters;
    }

    [HttpPost]
    [Route("battleResult")]
    public RoundHistory Battle(IEnumerable<CharacterDTO> charactersDTO){
        DiceRolls diceRolls = new DiceRolls();
        RoundHistory roundHistory = new RoundHistory{};
        AttackSetUp attackSetUp = new AttackSetUp(diceRolls, roundHistory);
        List<Character> characters = charactersDTO.Select(CharacterDTO.DTOToCharacter).ToList();

        FightSimulator fight = new FightSimulator(
            characters,
            diceRolls,
            attackSetUp,
            roundHistory);

        CharacterTeam winnerTeam = fight.Fight().WinnerTeam;
        return roundHistory;
    }
}


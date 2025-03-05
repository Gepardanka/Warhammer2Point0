using Microsoft.AspNetCore.Mvc;
using WarhammerFightSimulator.Models;
using WarhammerFightSimulator.Services;

namespace Warhammer2Point0.Controllers;

[ApiController]
[Route("fight")]
public class FightController: ControllerBase{

    private CharacterDTO CharacterToDTO(Character character){
        MeleeWeapon? RightHandWeapon = (MeleeWeapon?)character.Hands.RightHand;
        MeleeWeapon? LeftHandWeapon = (MeleeWeapon?)character.Hands.LeftHand;
        return new CharacterDTO{
            Name = character.Name,
            Team = character.Team,
            MeleeSkill = character.WW,
            Dexterity = character.Zr,
            NumberOfAttacks = character.A,
            Health = character.Zyw,
            Resistance = character.Wt,
            Strength = character.Wt,
            RangeSkill = character.US,
            Armour = character.Armour,
            RightHand = character.Hands.RightHand == null? null:
             new WeaponDTO{
                Modifier = RightHandWeapon!.Modifier,
                WeaponTraits = RightHandWeapon!.WeaponTraits
            },
            LeftHand = character.Hands.LeftHand == null? null: 
            new WeaponDTO{
                Modifier = LeftHandWeapon!.Modifier,
                WeaponTraits = LeftHandWeapon!.WeaponTraits
            },
            Abilities = character.Abilities,
            DodgeValue = character.Skills.ContainsKey(CharacterSkill.Unik)?
                character.Skills[CharacterSkill.Unik] : null,
        };
    }
    private Character DTOToCharacter(CharacterDTO characterDTO){
        
        return new Character{
            Name = characterDTO.Name,
            Team = characterDTO.Team,
            //stats
            WW = characterDTO.MeleeSkill,
            Zr = characterDTO.Dexterity,
            A = characterDTO.NumberOfAttacks,
            Zyw = characterDTO.Health,
            S = characterDTO.Strength,
            Wt = characterDTO.Resistance,
            US = characterDTO.RangeSkill,
            Armour = characterDTO.Armour, 
            Hands = new Hands{
                RightHand = characterDTO.RightHand == null? null :
                    new MeleeWeapon{
                        Modifier = characterDTO.RightHand.Modifier,
                        WeaponTraits = characterDTO.RightHand.WeaponTraits
                    },
                LeftHand = characterDTO.LeftHand == null? null:
                    new MeleeWeapon{
                        Modifier = characterDTO.LeftHand.Modifier,
                        WeaponTraits = characterDTO.LeftHand.WeaponTraits
                    }
            },
            Abilities  = characterDTO.Abilities,
            Skills  = characterDTO.DodgeValue == null? []:
                new Dictionary<CharacterSkill, int> {{CharacterSkill.Unik, (int)characterDTO.DodgeValue}},
            CurrentZyw = characterDTO.Health 
        };
    } 

    [HttpGet]
    [Route("defaultChar")]
    public IEnumerable<CharacterDTO> GetDefaultChar(){
        var chars = new List<Character>{
            new Character{
                Name = "Test0",
                Team = CharacterTeam.TeamA,
                WW = 50,
                Zr = 30,
                A = 1,
                Zyw = 15,
                S = 4,
                Wt = 3,
                US = 0,
            },
            new Character{
                Name = "Test1",
                Team = CharacterTeam.TeamB,
                WW = 50,
                Zr = 30,
                A = 1,
                Zyw = 15,
                S = 4,
                Wt = 3,
                US = 0,
            }
        };
        return chars.Select(x => CharacterToDTO(x)).ToList();
    }

    [HttpPost]
    [Route("battleResult")]
    public BattleResultDTO Battle(IEnumerable<CharacterDTO> charactersDTO){
        DiceRolls diceRolls = new DiceRolls();
        AttackSetUp attackSetUp = new AttackSetUp(diceRolls);
        List<Character> characters = charactersDTO.Select(x => DTOToCharacter(x)).ToList();

        FightSimulator fight = new FightSimulator(
            characters,
            diceRolls,
            attackSetUp);

        CharacterTeam winnerTeam = fight.Fight();
        return new BattleResultDTO{BattleCharacters = charactersDTO.ToList(), WinnerTeam = winnerTeam};
    }
}


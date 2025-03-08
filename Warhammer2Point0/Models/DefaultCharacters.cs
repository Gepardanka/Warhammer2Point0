namespace WarhammerFightSimulator.Models;
public class DefaultCharacters
{
    public List<CharacterDTO> characters;

    public DefaultCharacters()
    {
        characters = [
            new() {
                Guid = Guid.NewGuid(),
                Name = "Człowiek",
                Team = CharacterTeam.TeamA,
                MeleeSkill = 31,
                Dexterity = 31,
                NumberOfAttacks = 1,
                Health = 12,
                Resistance = 3,
                Strength = 3,
                RangeSkill = 31,
                Armour = new Armour{},
                RightHand =
                new WeaponDTO
                {
                    Modifier = 0,
                    WeaponTraits = []
                },
                LeftHand = null,
                Abilities = [],
                DodgeValue = null
            },
            new() {
                Guid = Guid.NewGuid(),
                Name = "Elf",
                Team = CharacterTeam.TeamA,
                MeleeSkill = 31,
                Dexterity = 41,
                NumberOfAttacks = 1,
                Health = 11,
                Resistance = 3,
                Strength = 3,
                RangeSkill = 41,
                Armour = new Armour{},
                RightHand =
                new WeaponDTO
                {
                    Modifier = 0,
                    WeaponTraits = []
                },
                LeftHand = null,
                Abilities = [],
                DodgeValue = null
            },
            new() {
                Guid = Guid.NewGuid(),
                Name = "Krasnolud",
                Team =  CharacterTeam.TeamB,
                MeleeSkill = 41,
                Dexterity = 21,
                NumberOfAttacks = 1,
                Health = 13,
                Resistance = 4,
                Strength = 3,
                RangeSkill = 31,
                Armour = new Armour{},
                RightHand =
                new WeaponDTO
                {
                    Modifier = 0,
                    WeaponTraits = []
                },
                LeftHand = null,
                Abilities = [],
                DodgeValue = null
            },
            new() {
                Guid = Guid.NewGuid(),
                Name = "Niziołek",
                Team = CharacterTeam.TeamB,
                MeleeSkill = 21,
                Dexterity = 41,
                NumberOfAttacks = 1,
                Health = 2,
                Resistance = 2,
                Strength = 2,
                RangeSkill = 41,
                Armour = new Armour{},
                RightHand =
                new WeaponDTO
                {
                    Modifier = 0,
                    WeaponTraits = []
                },
                LeftHand = null,
                Abilities = [],
                DodgeValue = null
            },

        ];
    }
}
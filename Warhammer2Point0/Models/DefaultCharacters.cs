namespace WarhammerFightSimulator.Models;
public class DefaultCharacters
{
    public List<CharacterDTO> characters;

    public DefaultCharacters()
    {
        characters = [
            new() {
                BigURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Images/warrior.png?",
                SmallURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Icons/warrior_icon.png?",
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
                    WeaponName = WeaponName.Sword,
                    Modifier = 0,
                    WeaponTraits = []
                },
                LeftHand = 
                new WeaponDTO{
                    WeaponName = WeaponName.Shield,
                    Modifier = -2,
                    WeaponTraits = [WeaponTrait.Parujacy]
                },
                Abilities = [],
                DodgeValue = null
            },
            new() {
                BigURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Images/elf.png?",
                SmallURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Icons/elf_icon.png?",
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
                    WeaponName = WeaponName.Sword,
                    Modifier = -1,
                    WeaponTraits = [WeaponTrait.Szybki]
                },
                LeftHand =
                new WeaponDTO{
                    WeaponName = WeaponName.Shield,
                    Modifier = -4,
                    WeaponTraits = [WeaponTrait.Wywazony, WeaponTrait.Parujacy]
                },
                Abilities = [CharacterAbility.BlyskawicznyBlok],
                DodgeValue = 10
            },
            new() {
                BigURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Images/dwarf.png?",
                SmallURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Icons/dwarf_icon.png?",
                Guid = Guid.NewGuid(),
                Name = "Krasnolud",
                Team =  CharacterTeam.TeamA,
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
                    WeaponName = WeaponName.Axe,
                    Modifier = 0,
                    WeaponTraits = [WeaponTrait.Powolny, WeaponTrait.Druzgoczacy]
                },
                LeftHand = null,
                Abilities = [CharacterAbility.SilnyCios],
                DodgeValue = null
            },
            new() {
                BigURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Images/niziolek.png?",
                SmallURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Icons/niziolek_icon.png?",
                Guid = Guid.NewGuid(),
                Name = "Niziołek",
                Team = CharacterTeam.TeamB,
                MeleeSkill = 21,
                Dexterity = 41,
                NumberOfAttacks = 1,
                Health = 12,
                Resistance = 2,
                Strength = 2,
                RangeSkill = 41,
                Armour = new Armour{
                    Head = 1,
                    RightArm = 1,
                    LeftArm = 1,
                    LeftLeg = 1,
                    RightLeg = 1
                },
                RightHand = null,
                LeftHand = null,
                Abilities = [CharacterAbility.Bijatyka],
                DodgeValue = 20
            },
            new() {
                BigURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Images/orc.png?",
                SmallURL = "https://raw.githubusercontent.com/Gepardanka/Warhammer2Point0/refs/heads/master/Assets/Icons/orc_icon.png?",
                Guid = Guid.NewGuid(),
                Name = "Ork",
                Team = CharacterTeam.TeamB,
                MeleeSkill = 35,
                Dexterity = 25,
                NumberOfAttacks = 1,
                Health = 12,
                Resistance = 4,
                Strength = 3,
                RangeSkill = 35,
                Armour = new Armour{
                    Head = 1,
                    RightArm = 1,
                    LeftArm = 1,
                    Body = 3
                },
                RightHand = new WeaponDTO{
                    WeaponName = WeaponName.Axe,
                    Modifier = 0,
                    WeaponTraits = []
                },
                LeftHand = null,
                Abilities = [CharacterAbility.Bijatyka, CharacterAbility.SilnyCios],
                DodgeValue = null
            },
        ];
    }
}
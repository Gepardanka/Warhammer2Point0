using WarhammerFightSimulator.Models;

public class CharacterDTO
{
    public required string Name { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public CharacterTeam Team { get; set; }
    public int MeleeSkill { get; set; }
    public int Dexterity { get; set; }
    public int NumberOfAttacks { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Resistance { get; set; }
    public int RangeSkill { get; set; }
    public required Armour Armour { get; set; }
    public WeaponDTO? RightHand { get; set; }
    public WeaponDTO? LeftHand { get; set; }
    public List<CharacterAbility> Abilities { get; set; } = [];
    public int? DodgeValue { get; set; }

    public static CharacterDTO CharacterToDTO(Character character)
    {
        MeleeWeapon? RightHandWeapon = (MeleeWeapon?)character.Hands.RightHand;
        MeleeWeapon? LeftHandWeapon = (MeleeWeapon?)character.Hands.LeftHand;
        return new CharacterDTO
        {
            Guid = character.Guid,
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
            RightHand = character.Hands.RightHand == null ? null :
             new WeaponDTO
             {
                 WeaponName = RightHandWeapon!.WeaponName,
                 Modifier = RightHandWeapon!.Modifier,
                 WeaponTraits = RightHandWeapon!.WeaponTraits
             },
            LeftHand = character.Hands.LeftHand == null ? null :
            new WeaponDTO
            {
                WeaponName = LeftHandWeapon!.WeaponName,
                Modifier = LeftHandWeapon!.Modifier,
                WeaponTraits = LeftHandWeapon!.WeaponTraits
            },
            Abilities = character.Abilities,
            DodgeValue = character.Skills.ContainsKey(CharacterSkill.Unik) ?
                character.Skills[CharacterSkill.Unik] : null,
        };
    }
    public static Character DTOToCharacter(CharacterDTO characterDTO)
    {

        return new Character
        {
            Guid = characterDTO.Guid == Guid.Empty ? Guid.NewGuid() : characterDTO.Guid,
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
            Hands = new Hands
            {
                RightHand = characterDTO.RightHand == null ? null :
                    new MeleeWeapon
                    {
                        WeaponName = characterDTO.RightHand.WeaponName,
                        Modifier = characterDTO.RightHand.Modifier,
                        WeaponTraits = characterDTO.RightHand.WeaponTraits
                    },
                LeftHand = characterDTO.LeftHand == null ? null :
                    new MeleeWeapon
                    {
                        WeaponName = characterDTO.LeftHand.WeaponName,
                        Modifier = characterDTO.LeftHand.Modifier,
                        WeaponTraits = characterDTO.LeftHand.WeaponTraits
                    }
            },
            Abilities = characterDTO.Abilities,
            Skills = characterDTO.DodgeValue == null ? [] :
                new Dictionary<CharacterSkill, int> { { CharacterSkill.Unik, (int)characterDTO.DodgeValue } },
            CurrentZyw = characterDTO.Health
        };
    }
}

public class WeaponDTO
{
    public string WeaponName { get; set; } = "";
    public int Modifier { get; set; }
    public required List<WeaponTrait> WeaponTraits { get; set; }
}
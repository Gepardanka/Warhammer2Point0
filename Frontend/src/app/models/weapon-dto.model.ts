import { WeaponTrait } from './weapon-trait.model';

export interface WeaponDTO {
  weaponName: string;
  modifier: number;
  weaponTraits: WeaponTrait[];
}

import { HitSuccessMissReason } from './hit-success-miss-reason.model';

export interface Round {
  attackingWeaponName: string;
  hitSuccessFailReason: HitSuccessMissReason;
  defendingCharID: string;
  defendingCharCurrentHP: number;
}

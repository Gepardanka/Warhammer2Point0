import { Injectable, inject, signal } from '@angular/core';
import { ApiService } from './api.service';
import { EMPTY, catchError, tap } from 'rxjs';
import { RoundHistory } from '../models/round-history.model';
import { CharacterDTO } from '../models/character-dto.model';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private readonly apiService = inject(ApiService);

  private _currentRoundHistory = signal<any | null>(null);
  private _defaultCharacters = signal<CharacterDTO[]>([]);

  public get currentRoundHistory() {
    return this._currentRoundHistory.asReadonly();
  }

  public get defaultCharacters() {
    return this._defaultCharacters.asReadonly();
  }

  public fetchDefaultCharacters() {
    this.apiService
      .get<CharacterDTO[]>('fight/defaultChar')
      .pipe(
        tap((result) => {
          console.log('hello2');
          this._defaultCharacters.set(result);
        }),
        catchError(() => {
          console.log('hello');
          return EMPTY;
        })
      )
      .subscribe();
  }

  public fetchBattleHistory(model: CharacterDTO[]): void {
    this.apiService
      .post<RoundHistory>('fight/battleresult', model)
      .pipe(
        tap((result) => {
          this._currentRoundHistory.set(result);
        }),
        catchError(() => {
          return EMPTY;
        })
      )
      .subscribe();
  }
}

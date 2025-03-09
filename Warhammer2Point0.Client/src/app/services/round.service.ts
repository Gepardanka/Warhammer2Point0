import { Injectable, inject, signal } from '@angular/core';
import { ApiService } from './api.service';
import { EMPTY, catchError, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoundService {
  private readonly apiService = inject(ApiService);

  private _currentRound = signal<any | null>(null);

  public get currentRound() {
    return this._currentRound.asReadonly();
  }

  public startGame(model: any): void {
    this.apiService.postGame(model).pipe(
      tap((result) => {
        this._currentRound.set(result);
      }),
      catchError(() => {
        return EMPTY;
      })
    );
  }
}

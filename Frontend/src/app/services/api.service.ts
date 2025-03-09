import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5014/';

  public postBattleStats(model: any): Observable<any> {
    return this.http.post(this.baseUrl + 'fight/battleresult', model);
  }
}

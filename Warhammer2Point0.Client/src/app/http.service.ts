import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private client: HttpClient) { }

  weatherFunction(){
    return this.client.get('http://localhost:5014/WeatherForecast')
  }

  battleFunction(){
    return this.client.post('http://localhost:5014/fight/battleresult', []);
  }
}

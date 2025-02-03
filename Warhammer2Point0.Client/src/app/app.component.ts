import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpService } from './http.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  constructor(private http: HttpService){
    http.weatherFunction().subscribe(x => {console.log(x)})
  }
  title = 'Client';
}
